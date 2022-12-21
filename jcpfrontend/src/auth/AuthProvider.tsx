import { createContext, useState, useContext, useEffect } from "react";

import * as jose from "jose";
import { Navigate, useLocation, useNavigate } from "react-router-dom";
import config from "../config";
import { v4 as uuidv4 } from "uuid";
import Swal from "sweetalert2";
import { JCPError } from "../Utils";

interface AuthContextType {
  user: any;
  token: string | null;
  init: boolean;
  saveToken: (token: string, callback: () => void) => void;
  signOut: (callback: () => void) => void;
  requestv2: (
    url: string,
    req: RequestInit,
    swal_err?: boolean
  ) => Promise<any>;
}

const AuthContext = createContext<AuthContextType>(null!);

export function useAuth() {
  return useContext(AuthContext);
}

export function AuthProvider({ children }: { children: React.ReactNode }) {
  let [user, setUser] = useState<any>(null);
  let [token, setToken] = useState<string>("");
  let [init, setInit] = useState<boolean>(false);

  useEffect(() => {
    const token = localStorage.getItem("token");
    saveToken(token === null ? "" : token, () => {});

    let err_backlog = localStorage.getItem("err_backlog");
    let errs = [];
    if (!err_backlog) return;
    else errs = JSON.parse(err_backlog);

    fetch((config.api_url + "/api/Error").replace(/([^:]\/)\/+/g, "$1"), {
      method: "PUT",
      headers: new Headers({
        "Content-Type": "application/json",
      }),
      body: JSON.stringify(errs),
    }).then((res) => {
      if (res.ok) localStorage.setItem("err_backlog", "");
    });
  }, []);

  let saveToken = (token: string, callback: () => void) => {
    let decoded_user = null;
    try {
      decoded_user = jose.decodeJwt(token);
      if (
        (decoded_user.exp ? decoded_user.exp : 0) <
        Math.floor(new Date().getTime() / 1000)
      )
        throw "Expired";
    } catch (error) {
      decoded_user = null;
    }

    setUser(decoded_user);
    setToken(token);
    setInit(true);
    localStorage.setItem("token", token);
    callback();
  };

  let signOut = (callback: () => void) => {
    setUser({});
    setToken("");
    localStorage.removeItem("token");
    callback();
  };

  let requestv2 = async (
    url: string,
    req: RequestInit,
    swal_err: boolean = true
  ) => {
    const clean = (config.api_url + url)
      .replace(/([^:]\/)\/+/g, "$1")
      .replace(/\/+$/, "");
    console.log(clean);
    let res_d;
    try {
      let res = await fetch(clean, {
        ...req,
        headers: new Headers({
          Authorization: "Bearer " + token,
          ...req.headers,
        }),
      });
      res_d = res;
      if (!res.ok)
        throw new JCPError(
          `Response code error ${res.status} ${res.statusText}`
        );
    } catch (error) {
      // @ts-ignore
      let msg = error.message;
      // @ts-ignore
      let body;
      try {
        body = await res_d?.json();
      } catch (_) {
        body = msg;
      }
      if (swal_err) Swal.fire("Request error!", msg, "error");
      throw new JCPError(msg, body);
    }

    try {
      let json;
      // Check for json response
      const contentType = res_d.headers.get("content-type");
      if (contentType && contentType.indexOf("application/json") !== -1)
        json = (await res_d?.json()) ?? "";
      return json;
    } catch (error) {
      // @ts-ignore
      let msg = error.message;
      if (swal_err) Swal.fire("JSON parse error!", msg, "error");
      throw new JCPError(msg);
    }
  };

  let value = { user, token, saveToken, signOut, init, requestv2 };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function RequireAuth({ children }: { children: JSX.Element }) {
  let auth = useAuth();
  let location = useLocation();

  if (!auth.init) return <></>;

  if (!auth.user || auth.token === "") {
    // Redirect them to the /login page, but save the current location they were
    // trying to go to when they were redirected. This allows us to send them
    // along to that page after they login, which is a nicer user experience
    // than dropping them off on the home page.
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return children;
}
