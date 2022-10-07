import { calculateJwkThumbprint } from "jose";
import { useState } from "react";
import { SupplierQuotesDTO } from "../DTO/SupplierQuotesDTO";
import { useCommonProps } from "../provider/CommonProps";
import { UserAdmin } from "../routes/UserAdmin";
import { Spreadsheet, Cell } from "./Spreadsheet";

export function SupplierQuotes(props: {
  quotes: SupplierQuotesDTO[];
  addLine: (line: SupplierQuotesDTO) => void;
  accept: (line: number) => void;
}) {
  const [state, setState] = useState<SupplierQuotesDTO>();

  const anyvalid = props.quotes.find((val) => {
    if (!!val.accepted_datetime) return val;
  });

  const cProps = useCommonProps();

  const handleInputChange = (event: { target: any }) => {
    const target = event.target;
    let value = target.type === "checkbox" ? target.checked : target.value;
    value = target.type === "datetime-local" ? target.value : value;
    const name = target.name;

    let add = {};

    if (name == "supplier_id") {
      add = {
        supplier: cProps.supplierBranches.find((val) => {
          if (val.id == value) return val;
        }),
      };
    }

    //@ts-ignore
    setState({
      ...state,
      ...add,
      [name]: value,
    });
    //@ts-check
  };

  const getMatrix = (): Cell[][] => {
    const data: Cell[][] = props.quotes?.map((val) => {
      const cells: Cell[] = [
        {
          type: "textReadonly",
          stringVal: val.supplier.supplier.name,
        },
        {
          type: "textReadonly",
          stringVal: val.supplier.name,
        },
        {
          type: "currencyReadonly",
          numberVal: val.quoted_price,
        },
        {
          type: "textReadonly",
          stringVal: val.part_number ?? "",
        },
        {
          type: "numberReadonly",
          numberVal: val.count,
        },
        {
          type: "textReadonly",
          stringVal: val.quoted_by,
        },
        {
          type: "datetime-local-readonly",
          datetimeVal: new Date(Date.parse(val.quoted_datetime)),
        },
        {
          type: anyvalid ? "checkboxReadonly" : "checkbox",
          boolVal: !!val.accepted_datetime,
        },
        {
          type: "textReadonly",
          stringVal: val.remarks,
        },
      ];
      return cells;
    }) ?? [[]];
    return data;
  };

  const submit = (event: any) => {
    event.preventDefault();
    if (state) props.addLine(state);
    setState(undefined);
  };

  return (
    <div className="bg-slate-200">
      <div className="flex justify-center flex-wrap">
        <h1 className="font-bold text-lg">Supplier Quotes</h1>
      </div>
      <div className="flex justify-center flex-wrap">
        <Spreadsheet
          cellMatrix={getMatrix()}
          headerRow={[
            "Supplier",
            "Branch",
            "Quoted Price",
            "Part Number / Part Desc",
            "Part Count",
            "Quoted By",
            "Quote Date",
            "Accepted",
            "Remarks",
          ]}
          onChange={(x) => {
            if (confirm("This action is final, Continue?")) props.accept(x);
          }}
        />
        {anyvalid && (
          <span className="text-xs mx-1 mb-1 text-gray-600">
            Accepted on{" "}
            {new Date(Date.parse(anyvalid.accepted_datetime ?? "")).toString()}
          </span>
        )}
        {!anyvalid && (
          <div className="mb-3">
            <div className="flex justify-center flex-wrap">
              <h1 className="font-bold text-lg">Add</h1>
            </div>
            <form onSubmit={submit}>
              <div className="flex flex-wrap">
                <div className="p-1">
                  <label className="mx-2">Supplier:</label>
                  <select
                    name="supplier_root"
                    // @ts-ignore
                    value={state?.supplier_root ?? ""}
                    // @ts-check
                    required
                    onChange={handleInputChange}
                  >
                    <option value={""}>---</option>
                    {cProps.suppliers.map((val, i) => (
                      <option key={i} value={val.id}>
                        {val.name}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="p-1">
                  <label className="mx-2">Branch:</label>
                  <select
                    name="supplier_id"
                    value={state?.supplier_id ?? ""}
                    required
                    onChange={handleInputChange}
                  >
                    <option value={""}>---</option>
                    {cProps.supplierBranches.map((val, i) => {
                      // @ts-ignore
                      if (val.supplier_id == state?.supplier_root)
                        return (
                          <option key={i} value={val.id}>
                            {val.name}
                          </option>
                        );
                      // @ts-check
                    })}
                  </select>
                </div>
                <div className="p-1">
                  <label className="mx-2">Quoted Price:</label>
                  <input
                    name="quoted_price"
                    type="number"
                    value={state?.quoted_price ?? ""}
                    required
                    onChange={handleInputChange}
                  />
                </div>
                <div className="p-1">
                  <label className="mx-2">Part No/Desc:</label>
                  <input
                    name="part_number"
                    type="text"
                    value={state?.part_number ?? ""}
                    required
                    onChange={handleInputChange}
                  />
                </div>
                <div className="p-1">
                  <label className="mx-2">Part Count:</label>
                  <input
                    name="count"
                    type="number"
                    value={state?.count ?? ""}
                    required
                    onChange={handleInputChange}
                  />
                </div>
                <div className="p-1">
                  <label className="mx-2">Quoted By:</label>
                  <input
                    name="quoted_by"
                    type="text"
                    value={state?.quoted_by ?? ""}
                    required
                    onChange={handleInputChange}
                  />
                </div>
                <div className="p-1">
                  <label className="mx-2">Quoted date:</label>
                  <input
                    name="quoted_datetime"
                    type="datetime-local"
                    value={state?.quoted_datetime ?? ""}
                    required
                    onChange={handleInputChange}
                  />
                </div>
              </div>
              <div className="flex justify-center flex-wrap w-full">
                <div className="p-1">
                  <label className="mx-2 font-bold">Remarks</label>
                </div>
              </div>
              <div className="flex justify-center flex-wrap w-full p-1">
                <textarea
                  className="w-full"
                  name="remarks"
                  required
                  value={state?.remarks ?? "None"}
                  onChange={handleInputChange}
                />
              </div>
              <div>
                <button
                  type="submit"
                  className="bg-green-300 p-1 hover:bg-green-200 rounded-md mx-3 mb-2"
                >
                  Submit
                </button>
              </div>
            </form>
          </div>
        )}
      </div>
    </div>
  );
}
