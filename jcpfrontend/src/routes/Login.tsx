import { FormEvent, useEffect, useState } from "react";
import { Navigate, useLocation, useNavigate, Location } from "react-router-dom";

import image from "../../images/logo1.png";

import hourglass from "../../images/icons/hourglass_empty_black_24dp.svg";

import { useAuth } from "../auth/AuthProvider";
import { SiteDTO } from "../DTO/SiteDTO";

export function Login() {
  const [userId, setUserId] = useState("");
  const [password, setPassword] = useState("");
  const [siteID, setSiteID] = useState("");
  const [sites, setSites] = useState<SiteDTO[] | undefined>(undefined);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const auth = useAuth();

  let navigate = useNavigate();
  let location: Location = useLocation();

  // @ts-ignore
  let from = location.state?.from?.pathname || "/";

  useEffect(() => {
    setLoading(true);
    auth
      .requestv2("/api/AnonSite", {
        method: "GET",
      })
      .then((res) => {
        setSites(res);
      })
      .catch((err) => {
        setError("Could not load sites from server");
      })
      .finally(() => setLoading(false));
  }, []);

  let checkPassword = (e: FormEvent) => {
    setLoading(true);
    auth
      .requestv2("/api/userauth", {
        headers: {
          "Content-Type": "application/json",
        },
        method: "POST",
        body: JSON.stringify({
          username: userId,
          password,
          site_id: siteID,
        }),
      })
      .then(
        (res) => {
          if (res.token !== "") {
            auth.saveToken(res.token, () => navigate(from));
          } else {
            setError("Invalid username or password");
            setLoading(false);
          }
        },
        (err) => {
          setError("Internal server error!" + err);
          setLoading(false);
        }
      );
    e.preventDefault();
  };

  return (
    <div className="bg-slate-500 w-screen h-screen flex justify-center items-center">
      <div className="bg-gray-50 rounded-lg shadow-md h-fit w-3/5 max-w-fit p-2">
        <img
          className="w-full max-w-lg pointer-events-none select none"
          src={image}
        ></img>
        <div className="m-5">
          <form id="loginform" onSubmit={(e) => checkPassword(e)}>
            {error && (
              <div className="outline-red-600 bg-red-300 mb-2 p-2 rounded-md">
                <p id="error" className="text-red-600 select-none">
                  {error}
                </p>
              </div>
            )}
            {loading && (
              <div className="flex justify-center mb-6 animate-spin">
                <img srcSet={hourglass} />
              </div>
            )}
            {!loading && (
              <>
                <select
                  id="site"
                  className="bg-slate-200 rounded-md p-1 w-full mb-3 focus:outline-none focus:ring-1 focus:ring-slate-400"
                  placeholder="User ID"
                  value={siteID}
                  required
                  onChange={(e) => setSiteID(e.target.value)}
                >
                  <option value={""} disabled>
                    --Select--
                  </option>
                  {sites?.map((val, i) => (
                    <option value={val.id} key={i}>
                      {val.description}
                    </option>
                  ))}
                </select>
                <input
                  id="username"
                  className="bg-slate-200 rounded-md p-1 w-full mb-3 focus:outline-none focus:ring-1 focus:ring-slate-400"
                  type="text"
                  placeholder="User ID"
                  value={userId}
                  required
                  onChange={(e) => setUserId(e.target.value)}
                />
                <input
                  id="password"
                  className="bg-slate-200 rounded-md p-1 w-full mb-3 focus:outline-none focus:ring-1 focus:ring-slate-400"
                  type="password"
                  placeholder="Password"
                  value={password}
                  required
                  onChange={(e) => setPassword(e.target.value)}
                />
                <div className="flex justify-end">
                  <input
                    id="submit"
                    className="rounded-md bg-slate-400 hover:bg-slate-500 p-1"
                    type="submit"
                    value="Login"
                    disabled={loading}
                  />
                </div>
              </>
            )}
          </form>
        </div>
      </div>
    </div>
  );
}
