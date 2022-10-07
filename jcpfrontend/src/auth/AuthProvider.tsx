import { createContext, useState, useContext, useEffect } from "react";

import * as jose from "jose";
import { Navigate, useLocation, useNavigate } from "react-router-dom";
import config from "../config";
import { v4 as uuidv4 } from "uuid";

interface AuthContextType {
  user: any;
  token: string | null;
  init: boolean;
  saveToken: (token: string, callback: () => void) => void;
  signOut: (callback: () => void) => void;
  requestv2: (url: string, req: RequestInit) => Promise<any>;
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

  let requestv2 = async (url: string, req: RequestInit) => {
    const clean = (config.api_url + url).replace(/([^:]\/)\/+/g, "$1");
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
        throw (
          `Response code error ${res.status} ${res.statusText} \n` +
          JSON.stringify(res)
        );

      let json = await res.json();
      return json;
    } catch (error) {
      let error_id = uuidv4();

      let error_data = {
        error_id: error_id,
        date: new Date().toISOString(),
        data: JSON.stringify({ error, userid: user?.id ?? "", res_d }),
      };

      let handle_error = (error: any) => {
        let err_backlog = localStorage.getItem("err_backlog");
        let errs = [];
        if (!err_backlog) errs = [];
        else errs = JSON.parse(err_backlog);

        errs = [...errs, error];
        localStorage.setItem("err_backlog", JSON.stringify(errs));
      };

      fetch((config.api_url + "/Error").replace(/([^:]\/)\/+/g, "$1"), {
        method: "POST",
        headers: new Headers({
          "Content-Type": "application/json",
        }),
        body: JSON.stringify(error_data),
      }).then(
        (res) => {
          if (!res.ok) {
            handle_error(error_data);
          }
        },
        () => {
          handle_error(error_data);
        }
      );

      throw JSON.stringify(error) + ` {${error_id}}`;
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
