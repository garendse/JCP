import { useEffect, useState } from "react";
import { useAuth } from "../auth/AuthProvider";
import { Spreadsheet, Cell } from "../components/Spreadsheet";
import { Link } from "react-router-dom";

import hourglass from "../../images/icons/hourglass_empty_black_24dp.svg";
import { QuoteDTO } from "../DTO/QuoteDTO";

export function Dashboard() {
  const auth = useAuth();

  const [quoteData, setQuoteData] = useState<QuoteDTO[]>([]);
  const [error, setError] = useState<string | undefined>(undefined);

  function GetCellData() {
    let cells: Cell[][] = [[]];
    quoteData.forEach((element) => {
      cells.push([
        {
          type: "textReadonly",
          stringVal: element.ro_number,
        },
        {
          type: "textReadonly",
          stringVal:
            element.customer?.title +
            " " +
            element.customer?.name +
            " " +
            element.customer?.surname,
        },
        {
          type: "textReadonly",
          stringVal: element.customer?.company_name ?? "N/A",
        },
        {
          type: "textReadonly",
          stringVal: element.customer?.mobile_no,
        },
        {
          type: "textReadonly",
          stringVal: element.tech
            ? element.tech?.name + " " + element.tech?.surname
            : "N/A",
        },
        {
          type: "textReadonly",
          stringVal: element.vehicle?.registration,
        },
        {
          type: "textReadonly",
          stringVal:
            element.vehicle?.brand +
            " " +
            element.vehicle?.model +
            " " +
            element.vehicle?.year,
        },
        {
          type: "textReadonly",
          stringVal: element.status,
        },
        {
          type: "child",
          childVal: (
            <div className="w-full text-center h-full bg-blue-200">
              <Link to={"/quote/" + element.id}>Edit</Link>
            </div>
          ),
        },
      ]);
    });
    return cells;
  }

  useEffect(() => {
    auth
      .requestv2("api/quotes", {
        method: "GET",
      })
      .then((res) => {
        setQuoteData(res);
      })
      .catch((err) => {
        setError("Error: " + err);
      });
  }, []);

  if (!quoteData && error == undefined) {
    return (
      <div className="flex justify-center">
        <div>
          <img src={hourglass} className="animate-spin block"></img>
        </div>
        <h1>Loading...</h1>
      </div>
    );
  }

  if (error) {
    return (
      <div className="flex justify-center flex-wrap">
        <h1>{error}</h1>
        <div className="flex w-full justify-center">
          <button
            className="bg-red-500 rounded-md hover:bg-red-300 p-2 text-white"
            onClick={() => window.location.reload()}
          >
            Retry
          </button>
        </div>
      </div>
    );
  }

  return (
    <>
      <h1 className="text-lg font-bold">Quotes</h1>

      {quoteData.length > 0 && (
        <div className="overflow-x-auto">
          <Spreadsheet
            headerRow={[
              "RO Number",
              "Customer Name",
              "Customer Company",
              "Customer Contact",
              "Technician",
              "Vehicle Reg",
              "Vehicle Model",
              "Status",
              "Edit",
            ]}
            cellMatrix={GetCellData()}
            onChange={(x, y, cell) => {}}
          />
        </div>
      )}

      {quoteData?.length == 0 ?? <h1>No Quotes Created Yet!</h1>}
    </>
  );
}
