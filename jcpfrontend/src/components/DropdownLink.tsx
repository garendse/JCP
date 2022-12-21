import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { link } from "../interfaces/link";
import { faCaretDown } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";
import { useAuth } from "../auth/AuthProvider";

export function DropdownLink(props: { link: link }) {
  const auth = useAuth();
  return (
    <div className="text-gray-400 text-lg select-none hover:text-gray-200 mx-2 whitespace-nowrap z-50">
      <div className="peer">
        {props.link.name}
        <FontAwesomeIcon className="pl-2" icon={faCaretDown} />
      </div>
      <div className="hidden absolute bg-white peer-hover:flex hover:flex flex-wrap flex-col min-w-[100px] w-fit drop-shadow-md rounded-sm py-1">
        {props.link.sub
          ?.sort((a: link, b: link) => {
            if (a.name == b.name) return 0;
            return a.name > b.name ? 1 : -1;
          })
          .map((val, idx) => {
            if (
              auth.user &&
              (val.access.includes(auth.user.role) ||
                val.access.includes("ALL") ||
                auth.user.role == "DEBUG")
            )
              return (
                <Link
                  className="text-sm text-gray-600 hover:text-black hover:bg-gray-300 w-full px-2 py-1"
                  key={idx}
                  to={val.url ?? ""}
                >
                  {val.name}
                </Link>
              );
          })}
      </div>
    </div>
  );
}
