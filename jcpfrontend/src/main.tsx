import React from "react";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App";
import { Dashboard } from "./routes/Dashboard";
import { Login } from "./routes/Login";

import config from "./config";
import { AuthProvider, RequireAuth } from "./auth/AuthProvider";
import { UserAdmin } from "./routes/UserAdmin";
import { QuoteEdit } from "./routes/QuoteEdit";
import { QuoteEditLine } from "./routes/QuoteEditLines";
import { CheckIn } from "./routes/CheckIn";
import { CommonPropsProvider } from "./provider/CommonProps";
import { QuoteProvider } from "./provider/Quote";
import { Maintain } from "./routes/Maintenance";
import { AdminTable } from "./components/AdminTable";

import jobcodes from "./schemas/jobcodes";
import customer from "./schemas/customer";
import supplier from "./schemas/supplier";
import { MultiAdminTable } from "./components/MultiAdminTable";
import supplier_branches from "./schemas/supplier_branches";
import userschema from "./schemas/userschema";
import techs from "./schemas/techs";
import vehicles from "./schemas/vehicles";

const container = document.getElementById("root");
if (container) {
  const root = createRoot(container);

  root.render(
    <React.StrictMode>
      <BrowserRouter basename={config.base_url}>
        <AuthProvider>
          <Routes>
            <Route path="/" element={<App />}>
              <Route
                index
                element={
                  <RequireAuth>
                    <Dashboard />
                  </RequireAuth>
                }
              />
              <Route
                path="/maintain"
                element={
                  <RequireAuth>
                    <Maintain />
                  </RequireAuth>
                }
              >
                <Route
                  path="/maintain/job-codes"
                  element={<AdminTable key="jc" {...jobcodes} />}
                />
                <Route
                  path="/maintain/customer"
                  element={<AdminTable key="cust" {...customer} />}
                />
                <Route
                  path="/maintain/supplier"
                  element={<AdminTable key="sup" {...supplier} />}
                />
                <Route
                  path="/maintain/supplier-branches"
                  element={<MultiAdminTable key="br" {...supplier_branches} />}
                />
                <Route
                  path="/maintain/userlist"
                  element={<AdminTable key="br" {...userschema} />}
                />
                <Route
                  path="/maintain/techs"
                  element={<AdminTable key="techs" {...techs} />}
                />
                <Route
                  path="/maintain/vehicles"
                  element={<AdminTable key="vehicles" {...vehicles} />}
                />
                <Route
                  path="/maintain/usersupport"
                  element={<AdminTable key="users" {...userschema} />}
                />
              </Route>
              <Route
                path="/useradmin"
                element={
                  <RequireAuth>
                    <UserAdmin />
                  </RequireAuth>
                }
              />
              <Route
                path="/checkin"
                element={
                  <RequireAuth>
                    <CheckIn />
                  </RequireAuth>
                }
              />
              <Route
                path="/quote/:quoteId"
                element={
                  <RequireAuth>
                    <QuoteProvider>
                      <CommonPropsProvider>
                        <QuoteEdit />
                      </CommonPropsProvider>
                    </QuoteProvider>
                  </RequireAuth>
                }
              >
                <Route path="/quote/:quoteId/" element={<QuoteEditLine />} />
              </Route>
            </Route>
            <Route path="/login" element={<Login />} />
            <Route path="*" element={<div>404</div>} />
          </Routes>
        </AuthProvider>
        <div className="print:hidden absolute bottom-0 right-5 p-3"></div>
      </BrowserRouter>
    </React.StrictMode>
  );
}
