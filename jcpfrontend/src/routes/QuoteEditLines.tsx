import {
  Cell,
  Spreadsheet,
  TextLengthValidate,
} from "../components/Spreadsheet";

import chevron from "../../images/icons/chevron_right.svg";
import config from "../config";
import { useQuote } from "../provider/Quote";
import { useCommonProps } from "../provider/CommonProps";
import { QuoteDTO } from "../DTO/QuoteDTO";
import { QuoteItemDTO } from "../DTO/QuoteItemDTO";
import { SupplierQuotes } from "../components/SupplierQuotes";
import { SupplierQuotesDTO } from "../DTO/SupplierQuotesDTO";
import { useAuth } from "../auth/AuthProvider";
import { useEffect, useState } from "react";

export function QuoteEditLine() {
  const { quote, setQuoteData, setChanged } = useQuote();

  if (!quote) return <></>;

  const auth = useAuth();
  const { jobCodes } = useCommonProps();

  const [totalExcl, setTotalExcl] = useState<number>(0);
  const [totalAuthorisedExcl, setTotalAuthorisedExcl] = useState<number>(0);

  useEffect(() => {
    let rtExcl = 0;
    let rtAuth = 0;

    quote.items.forEach((element) => {
      const total_part_rate =
        element.part_rate * (element.part_markup / 100) * element.part_quantity;

      const total_labour_rate = element.labour_hours * element.labour_rate;

      const total_excl = total_labour_rate + total_part_rate;

      rtExcl += total_excl;

      if (element.auth) {
        rtAuth += total_excl;
      }
    });

    setTotalExcl(rtExcl);
    setTotalAuthorisedExcl(rtAuth);
  }, [quote]);

  const GetLinesAsMatrix = (): Cell[][] => {
    let cells: Cell[][] = [[]];

    let items: QuoteItemDTO[] = [...(quote?.items ?? [])];

    let shown = 0;
    items.forEach((val) => {
      shown += val.show ? 1 : 0;
    });

    if (shown == 0)
      items.push({
        description: "",
        id: "",
        sort_order: quote?.items?.length,
        show: false,
        quote_id: quote.id,
        job_code: "",
        location: "",
        auth: false,
        labour_hours: 0,
        labour_rate: 0,
        part_markup: 100,
        part_quantity: 1,
        part_rate: 0,
        subquotes: [],
      });

    items.forEach((element, i) => {
      const total_part_rate =
        element.part_rate * (element.part_markup / 100) * element.part_quantity;

      const total_labour_rate = element.labour_hours * element.labour_rate;

      const total_excl = total_labour_rate + total_part_rate;
      const VAT = total_excl * config.VAT;
      const total = total_excl + VAT;

      const allowShow = (shown == 0 && i != items.length - 1) || element.show;

      cells.push([
        {
          type: "child",
          childVal: allowShow ? (
            <button
              type="button"
              className="flex justify-center items-center w-full"
              onClick={() => {
                let new_data = { ...quote };
                if (new_data?.items) new_data.items[i].show = !element.show;

                setQuoteData(new_data);
              }}
            >
              <img
                src={chevron}
                className={"w-4 p-0" + (element.show ? " rotate-90" : "")}
              ></img>
            </button>
          ) : (
            <></>
          ),
        },
        {
          type: "search",
          stringVal: element.job_code ?? "",
          getAutocomplete: () =>
            jobCodes
              ?.filter((val) => {
                if (
                  val.code
                    .toUpperCase()
                    .includes((element?.job_code ?? "").toUpperCase()) ||
                  val.description
                    .toUpperCase()
                    .includes((element?.job_code ?? "").toUpperCase())
                )
                  return val;
              })
              .map((val) => {
                return {
                  label:
                    val.code +
                    "-" +
                    val.description +
                    " " +
                    (val.location ? val.location : ""),
                  name: val.code,
                  onClick: (name) => {
                    if (quote) {
                      let nq: QuoteDTO = { ...quote };
                      if (nq.items) {
                        const code = jobCodes.find((val) => {
                          return val.code == name;
                        });
                        if (!code) return;

                        nq.items[i].description = code.description ?? "";
                        nq.items[i].location = code.location ?? "N/A";
                        nq.items[i].part_markup = code.markup ?? 100;
                        nq.items[i].part_quantity = code.standard_volume ?? 1;
                        nq.items[i].labour_hours = code.standard_hours ?? 1;
                        nq.items[i].part_rate = code.cost ?? 0;
                      }

                      setQuoteData(nq);
                      setChanged(true);
                    }
                  },
                };
              }) ?? [],
          propName: "job_code",
          validate: {
            textLen: 10,
          },
        },
        {
          type: "text",
          stringVal: element.description ?? "",
          propName: "description",
          validate: {
            textLen: 255,
          },
        },
        {
          type: "text",
          stringVal: element.location ?? "N/A",
          propName: "location",
          validate: {
            textLen: 50,
          },
        },
        {
          type: "currency",
          numberVal: element.part_rate,
          propName: "part_rate",
        },
        {
          type: "number",
          numberVal: element.part_markup,
          propName: "part_markup",
        },
        {
          type: "number",
          numberVal: element.part_quantity,
          propName: "part_quantity",
        },
        {
          type: "currencyReadonly",
          numberVal: total_part_rate,
        },
        {
          type: "currency",
          numberVal: element.labour_rate,
          propName: "labour_rate",
        },
        {
          type: "number",
          numberVal: element.labour_hours,
          propName: "labour_hours",
        },
        {
          type: "currencyReadonly",
          numberVal: total_labour_rate,
          propName: "labour_hours",
        },
        {
          type: "currencyReadonly",
          numberVal: total_excl,
        },
        {
          type: "currencyReadonly",
          numberVal: VAT,
        },
        {
          type: "currencyReadonly",
          numberVal: total,
        },
        {
          type: "checkbox",
          boolVal: element.auth,
          propName: "auth",
        },
      ]);
      if (element.show) {
        cells.push([
          {
            colspan: 15,
            type: "child",
            childVal: (
              <SupplierQuotes
                quotes={element.subquotes}
                addLine={(line) => {
                  let new_data = { ...quote };
                  let nline: SupplierQuotesDTO = {
                    ...line,
                    quoted_price: line.quoted_price * 100,
                    quote_item_id: element.id,
                  };
                  if (new_data?.items)
                    new_data.items[i].subquotes = [...element.subquotes, nline];
                  setQuoteData(new_data);
                  setChanged(true);
                }}
                accept={(line) => {
                  let new_data = { ...quote };

                  new_data.items[i].subquotes[line].accepted_datetime =
                    new Date().toJSON();
                  new_data.items[i].subquotes[line].accepted_by_user_id =
                    auth.user.id;

                  setQuoteData(new_data);
                  setChanged(true);
                }}
              />
            ),
          },
        ]);
      }
    });

    cells.push([
      { type: "textReadonly", colspan: 10, rowspan: 2 },
      {
        type: "child",
        childVal: (
          <div className="bg-slate-400 text-right">
            <b>Totals: </b>
          </div>
        ),
      },
      { type: "currencyReadonly", numberVal: totalExcl },
      { type: "currencyReadonly", numberVal: totalExcl * config.VAT },
      { type: "currencyReadonly", numberVal: totalExcl * (1 + config.VAT) },
    ]);
    cells.push([
      {
        type: "child",
        childVal: (
          <div className="bg-slate-400 text-right">
            <b>Authorised Totals: </b>
          </div>
        ),
      },
      { type: "currencyReadonly", numberVal: totalAuthorisedExcl },
      { type: "currencyReadonly", numberVal: totalAuthorisedExcl * config.VAT },
      {
        type: "currencyReadonly",
        numberVal: totalAuthorisedExcl * (1 + config.VAT),
      },
    ]);

    return cells;
  };

  const onChange = (x: number, y: number, cell: Cell) => {
    if (quote) {
      let nquote: QuoteDTO = { ...quote };

      let index = cell.propName as keyof QuoteItemDTO;

      if (nquote?.items?.length == x - 1)
        nquote.items.push({
          description: "",
          id: "",
          show: false,
          quote_id: quote.id,
          job_code: "",
          location: "",
          auth: false,
          labour_hours: 0,
          sort_order: nquote?.items?.length,
          labour_rate: 0,
          part_markup: 100,
          part_quantity: 1,
          part_rate: 0,
          subquotes: [],
        });

      if (quote?.items?.[x - 1])
        switch (cell.type) {
          case "number":
          case "currency":
            // @ts-ignore
            nquote.items[x - 1][index] = cell.numberVal ?? 0;
            break;
          case "checkbox":
            // @ts-ignore
            nquote.items[x - 1][index] = cell.boolVal ?? false;
            break;
          case "text":
          case "search":
            // @ts-ignore
            nquote.items[x - 1][index] = cell.stringVal ?? "";
            break;
        }
      setChanged(true);
      setQuoteData(nquote);
    }
  };

  return (
    <>
      <div className="overflow-x-auto h-full">
        <div className="h-full">
          <Spreadsheet
            headerRow={[
              "Info",
              "Job Code",
              "Description",
              "Location",
              "Parts Cost",
              "Parts Markup",
              "Parts Count",
              "Parts Excl",
              "Labour Rate",
              "Labour Hours",
              "Labour Cost Excl",
              "Total Excl",
              "VAT",
              "Total Incl",
              "Auth",
            ]}
            cellMatrix={GetLinesAsMatrix()}
            onChange={onChange}
          />
        </div>
      </div>
    </>
  );
}
