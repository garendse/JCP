import { faTable } from "@fortawesome/free-solid-svg-icons";
import { useEffect, useState } from "react";
import { useAuth } from "../auth/AuthProvider";
import { AdminTable, AdminTableSchema } from "./AdminTable";

export interface MultiAdminTableSchema {
  table: AdminTableSchema;
  route: string;
  list_key_prop: string;
  list_value_prop: string;
  list_key_search_qry: string;
  name: string;
  define_key: string;
  define_value_prop: string;
}

export function MultiAdminTable(props: MultiAdminTableSchema) {
  const [data, setData] = useState<any>(undefined);
  const [error, setError] = useState<string | undefined>(undefined);
  const [selected, setSelected] = useState<string>("");
  const auth = useAuth();

  useEffect(() => {
    auth
      .requestv2(props.route, {
        method: "GET",
      })
      .then((res) => {
        setData(res);
      })
      .catch((err) => {
        setError(err);
      });
  }, []);

  if (error) return <h1>{error}</h1>;

  let onaddcustom;

  if (data && props.define_value_prop && props.define_key) {
    onaddcustom = {
      [props.define_key]: data?.find((val: any) => {
        if (val[props.list_key_prop] == selected) return val;
      })?.[props.define_value_prop],
    };
  }

  const cprops: AdminTableSchema = {
    ...props.table,
    name: "",
    custom_params: {
      [props.list_key_search_qry]: selected,
    },
    onaddcustom,
  };

  return (
    <>
      {data && (
        <>
          <div>
            <div className="flex justify-center">
              <h1 className="text-3xl font-bold pb-3">{props.name}</h1>
            </div>
            <div className="flex justify-center">
              <select
                className="shadow-md rounded-md p-1 bg-slate-100 min-w-[10em] max-w-full"
                value={selected}
                onChange={(e) => setSelected(e.target.value)}
              >
                <option disabled value={""}>
                  ----
                </option>
                {data.map((val: any, i: number) => (
                  <option key={i} value={val[props.list_key_prop]}>
                    {val[props.list_value_prop]}
                  </option>
                ))}
              </select>
            </div>
          </div>
          {selected !== "" && <AdminTable {...cprops}></AdminTable>}
        </>
      )}
    </>
  );
}
