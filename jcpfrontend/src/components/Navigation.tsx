import React from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../auth/AuthProvider";
import { link } from "../interfaces/link";
import { UserAdmin } from "../routes/UserAdmin";
import { DropdownLink } from "./DropdownLink";

const links: link[] = [
  {
    url: "/",
    name: "Dashboard",
    access: ["ALL"],
  },
  {
    url: "/checkin",
    name: "Check In Vehicle",
    access: ["ALL"],
  },
  {
    name: "Admin",
    access: ["ALL"],
    sub: [
      {
        name: "Job Codes",
        url: "/maintain/job-codes",
        access: ["ALL"],
      },
      {
        name: "Customers",
        url: "/maintain/customer",
        access: ["ALL"],
      },
      {
        name: "Supplier",
        url: "/maintain/supplier",
        access: ["ALL"],
      },
      {
        name: "Supplier Branches",
        url: "/maintain/supplier-branches",
        access: ["ALL"],
      },
      {
        name: "Users",
        url: "/maintain/userlist",
        access: ["ALL"],
      },
      {
        name: "Techs",
        url: "/maintain/techs",
        access: ["ALL"],
      },
      {
        name: "Vehicles",
        url: "/maintain/vehicles",
        access: ["ALL"],
      },
    ],
  },
];

export function Navigation() {
  const auth = useAuth();
  const navigate = useNavigate();
  const location = useLocation();

  return (
    <div className="print:hidden h-fit bg-gray-900 items-center flex px-2 p-3">
      <p className="text-xl text-gray-400 select-none">JobCostPro</p>
      <div className="bg-gray-700 w-px h-8 mx-2" />
      <div className="flex bg-gray-900 items-center flex-wrap sm:flex-nowrap">
        {links.map((link, idx) => {
          if (
            auth.user &&
            (link.access.includes(auth.user.role) ||
              link.access.includes("ALL") ||
              auth.user.role == "SUPPORT" ||
              auth.user.role == "DEBUG")
          )
            return (
              <div className="flex items-center" key={idx}>
                {link.url && (
                  <Link to={link.url ?? ""} key={idx}>
                    <span
                      className={
                        "text-lg select-none hover:text-gray-200 mx-2 whitespace-nowrap" +
                        (location.pathname === link.url
                          ? " text-gray-200"
                          : " text-gray-400")
                      }
                    >
                      {link.name}
                    </span>
                  </Link>
                )}
                {link.sub && !link.url && <DropdownLink link={link} />}
                <div className="w-px h-4 mx-2 bg-gray-700" />
              </div>
            );
        })}
      </div>
      <div className="w-full flex justify-end items-center">
        <span className="text-md text-gray-400">
          {auth.user?.site_name ?? ""}
        </span>
        <div className="bg-gray-700 w-px h-8 mx-2" />
        <span
          className="text-lg text-gray-600 hover:text-gray-400 px-2 select-none"
          onClick={() => auth.signOut(() => navigate("/login"))}
        >
          Sign Out
        </span>
      </div>
    </div>
  );
}
