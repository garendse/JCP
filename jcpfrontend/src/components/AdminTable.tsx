import { useEffect, useState } from "react";
import { useAuth } from "../auth/AuthProvider";
import { Loading } from "./Loading";
import { Cell, Spreadsheet } from "./Spreadsheet";

import save from "../../images/icons/save-solid.svg";
import cancel from "../../images/icons/cancel_FILL0_wght400_GRAD0_opsz48.svg";

export interface SchemaColumn {
  name: string;
  prop_name: string;
  type:
    | "text"
    | "textReadonly"
    | "number"
    | "numberReadonly"
    | "currency"
    | "currencyReadonly"
    | "checkbox"
    | "checkboxReadonly"
    | "child"
    | "search";
}

export interface AdminTableSchema {
  name: string;
  api_suffix: string;
  className?: string;
  columns: SchemaColumn[];
  pk: string;
  custom_params?: any;
  onaddcustom?: any;
  noadd?: boolean;
}

export function AdminTable(props: AdminTableSchema) {
  const auth = useAuth();
  const [error, setError] = useState<string | undefined>(undefined);
  const [data, setData] = useState<any>(undefined);
  const [changed, setChanged] = useState<boolean>(false);

  useEffect(() => {
    console.log("ue");
    auth
      .requestv2(
        props.api_suffix +
          "?" +
          new URLSearchParams(props.custom_params).toString(),
        {
          method: "GET",
        }
      )
      .then((res) => {
        let ndata = [];
        for (let row of res) {
          ndata.push({ ...row, flag: "unchanged" });
        }
        setData(ndata);
      })
      .catch((err) => {
        setError(err);
      });
  }, [props.custom_params]);

  let onChange = (y: number, x: number, val: Cell) => {
    let ndata = [...data];
    let name = props.columns[x].prop_name;

    if (ndata.length <= y) {
      let blank: any = {};
      for (let col of props.columns) {
        switch (col.type) {
          case "checkbox":
          case "checkboxReadonly":
            blank[col.prop_name] = false;
            break;
          case "currency":
          case "currencyReadonly":
          case "number":
          case "numberReadonly":
            blank[col.prop_name] = 0;
            break;
          case "text":
          case "textReadonly":
            blank[col.prop_name] = "";
        }
      }
      Object.keys(props.onaddcustom ?? {}).forEach((key: string) => {
        blank[key] = props.onaddcustom[key];
      });
      setChanged(true);
      ndata.push({ ...blank, flag: "added" });
    }

    switch (val.type) {
      case "checkbox":
        ndata[y][name] = val.boolVal;
        break;
      case "number":
      case "currency":
        ndata[y][name] = val.numberVal;
        break;
      case "text":
        ndata[y][name] = val.stringVal;
        break;
    }

    if (ndata[y].flag == "unchanged") {
      ndata[y].flag = "changed";
      setChanged(true);
    }

    setData(ndata);
  };

  const doSave = () => {
    let ndata = [...data];
    for (let row of ndata) {
      if (row.flag == "changed") {
        auth.requestv2(props.api_suffix + "/" + row[props.pk], {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(row),
        });
      }
      if (row.flag == "added") {
        auth.requestv2(props.api_suffix, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(row),
        });
      }
      row.flag = "unchanged";
    }
    setChanged(false);
  };

  if (error)
    return (
      <div className="flex justify-center">
        <h1>{error}</h1>
      </div>
    );
  if (!data) return <Loading />;

  const getMatrix = (): Cell[][] => {
    let matrix = data.map((val: any) => {
      return props.columns.map((col) => {
        let type: typeof col.type = col.type;
        if (val.flag == "added")
          switch (col.type) {
            case "checkbox":
            case "checkboxReadonly":
              type = "checkbox";
              break;
            case "currency":
            case "currencyReadonly":
              type = "currency";
              break;
            case "number":
            case "numberReadonly":
              type = "number";
              break;
            case "text":
            case "textReadonly":
              type = "text";
              break;
          }

        const cellValue = val[col.prop_name];
        const cell: Cell = {
          type: type,
          propName: col.prop_name,
          colspan: 1,
        };
        switch (col.type) {
          case "checkbox":
          case "checkboxReadonly":
            cell.boolVal = !!cellValue;
            break;
          case "currency":
          case "currencyReadonly":
          case "number":
          case "numberReadonly":
            cell.numberVal = cellValue;
            break;
          case "text":
          case "textReadonly":
            cell.stringVal = cellValue;
        }
        return cell;
      });
    });

    const blankrow = props.columns.map((col) => {
      let type: typeof col.type = "text";
      switch (col.type) {
        case "checkbox":
        case "checkboxReadonly":
          type = "checkbox";
          break;
        case "currency":
        case "currencyReadonly":
          type = "currency";
          break;
        case "number":
        case "numberReadonly":
          type = "number";
          break;
        case "text":
        case "textReadonly":
          type = "text";
          break;
      }

      const cell: Cell = {
        type: type,
        propName: col.prop_name,
        colspan: 1,
      };
      switch (col.type) {
        case "checkbox":
        case "checkboxReadonly":
          cell.boolVal = false;
          break;
        case "currency":
        case "currencyReadonly":
        case "number":
        case "numberReadonly":
          cell.numberVal = 0;
          break;
        case "text":
        case "textReadonly":
          cell.stringVal = "";
      }
      return cell;
    });

    if (props.noadd) return matrix;

    return [...matrix, blankrow];
  };

  return (
    <div className="overflow-x-auto">
      <div className="flex justify-center">
        <h1 className="text-3xl font-bold">{props.name}</h1>
      </div>
      <div className={"flex justify-end px-3" + (changed ? "" : " invisible")}>
        <button className="mx-2" title="Save" onClick={doSave}>
          <img src={save} className="block w-6"></img>
        </button>
        <button
          className="mx-2"
          title="cancel"
          onClick={() => window.location.reload()}
        >
          <img src={cancel} className="block w-6"></img>
        </button>
      </div>
      <div className={props.className}>
        <Spreadsheet
          headerRow={props.columns.map((v) => v.name)}
          cellMatrix={getMatrix()}
          onChange={onChange}
        />
      </div>
    </div>
  );
}
