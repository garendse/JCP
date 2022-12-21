import currency from "currency.js";
import { Children, useState } from "react";
import { AutoComplete } from "../DTO/AutoComplete";
import { CheckboxCell } from "./cellfromats/CheckboxCell";
import { CurrencyCell, CurrencyReadonlyCell } from "./cellfromats/CurrencyCell";
import { DateTimeCell } from "./cellfromats/DateTimeCell";
import { TextCell } from "./cellfromats/TextCell";
import { SuggestedSearch } from "./SuggestedSearch";

export interface SpreadSheetValidate {
  textValidate?: (s: string) => boolean;
  textLen?: number;
}

export function TextLengthValidate(s: string, max_len: number) {
  return s.length <= max_len;
}

export interface Cell {
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
    | "search"
    | "datetime-local"
    | "datetime-local-readonly"
    | "password"
    | "passwordReadonly";
  stringVal?: string;
  numberVal?: number;
  datetimeVal?: Date;
  boolVal?: boolean;
  colspan?: number;
  rowspan?: number;
  propName?: string;
  childVal?: JSX.Element;
  roundFloat?: number;
  validate?: SpreadSheetValidate;
  width?: number;
  getAutocomplete?: () => AutoComplete[];
}

interface SpreadsheetProps {
  headerRow: string[];
  cellMatrix: Cell[][];
  onChange: (x: number, y: number, NewCell: Cell) => void;
  doHover?: boolean;
}

function GetCell(props: { cell: Cell; onChange: (NewCell: Cell) => void }) {
  switch (props.cell.type) {
    case "text":
      return (
        <TextCell
          value={props.cell.stringVal ?? ""}
          setValue={(val) => {
            props.onChange({
              ...props.cell,
              stringVal: val
                .toString()
                .substring(
                  0,
                  Math.min(
                    props.cell.validate?.textLen ?? Number.MAX_SAFE_INTEGER,
                    val.toString().length
                  )
                ),
            });
          }}
          valid={
            (props.cell.validate?.textValidate?.(props.cell.stringVal ?? "") ??
              true) &&
            TextLengthValidate(
              props.cell.stringVal ?? "",
              props.cell.validate?.textLen ?? Number.MAX_SAFE_INTEGER
            )
          }
        />
      );
    case "textReadonly":
      return <p className="bg-gray-300/50">{props.cell.stringVal}</p>;
    case "number":
      return (
        <TextCell
          value={props.cell.numberVal?.toString() ?? ""}
          number
          setValue={(val) => {
            if (typeof val == "number")
              props.onChange({ ...props.cell, numberVal: val });
          }}
        />
      );
    case "numberReadonly":
      return (
        <p className="bg-gray-300/50 text-right">{props.cell.numberVal}</p>
      );
    case "currency":
      return (
        <CurrencyCell
          key={props.cell.numberVal ?? 0}
          value={props.cell.numberVal ?? 0}
          setValue={(val) => {
            if (typeof val == "number")
              props.onChange({ ...props.cell, numberVal: val });
          }}
        />
      );
    case "currencyReadonly":
      return <CurrencyReadonlyCell value={props.cell.numberVal ?? 0} />;
    case "checkbox":
      return (
        <CheckboxCell
          value={props.cell.boolVal ?? false}
          setValue={(val) => {
            props.onChange({ ...props.cell, boolVal: val });
          }}
        />
      );
    case "checkboxReadonly":
      return (
        <CheckboxCell
          value={props.cell.boolVal ?? false}
          readonly
          setValue={(val) => {
            props.onChange({ ...props.cell, boolVal: val });
          }}
        />
      );
    case "search":
      return (
        <SuggestedSearch
          value={props.cell.stringVal ?? ""}
          autocomplete={props?.cell?.getAutocomplete?.() ?? []}
          onChange={(val) => {
            props.onChange({
              ...props.cell,
              stringVal: val
                .toString()
                .substring(
                  0,
                  Math.min(
                    props.cell.validate?.textLen ?? Number.MAX_SAFE_INTEGER,
                    val.toString().length
                  )
                ),
            });
          }}
        />
      );
    case "datetime-local":
      return (
        <DateTimeCell
          value={props.cell.datetimeVal ?? new Date(Date.now())}
          setValue={(val: Date) => {
            props.onChange({
              ...props.cell,
              datetimeVal: val,
            });
          }}
        />
      );
    case "datetime-local-readonly":
      return (
        <DateTimeCell
          readOnly
          value={props.cell.datetimeVal ?? new Date(Date.now())}
          setValue={(val: Date) => {
            props.onChange({
              ...props.cell,
              datetimeVal: val,
            });
          }}
        />
      );
    case "password":
      return (
        <TextCell
          value={props.cell.stringVal ?? "****"}
          password
          setValue={(val) => {
            props.onChange({
              ...props.cell,
              stringVal: val
                .toString()
                .substring(
                  0,
                  Math.min(
                    props.cell.validate?.textLen ?? Number.MAX_SAFE_INTEGER,
                    val.toString().length
                  )
                ),
            });
          }}
        />
      );
    case "passwordReadonly":
      return <TextCell password value={"****"} readOnly setValue={() => {}} />;

    case "child":
      if (props.cell.childVal !== undefined) return props.cell.childVal;
      else return <></>;
  }
}

export function Spreadsheet(props: SpreadsheetProps) {
  return (
    <div className="p-2 w-full h-full">
      <table className="table-auto border-collapse border border-black w-full">
        <thead>
          <tr>
            {props.headerRow.map((cell, idx) => (
              <th
                className="border border-black bg-slate-300 text-sm whitespace-nowrap px-1 w-fit"
                key={idx}
              >
                {cell}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {props.cellMatrix.map((row, x) => {
            return (
              <tr key={x} className="odd:bg-slate-200 even:bg-white">
                {row.map((cell, y) => {
                  return (
                    <td
                      className="w-auto border border-black text-sm whitespace-nowrap"
                      key={y}
                      colSpan={cell.colspan}
                      rowSpan={cell.rowspan}
                    >
                      <GetCell
                        cell={cell}
                        onChange={(cell: Cell) => {
                          props.onChange(x, y, cell);
                        }}
                      />
                    </td>
                  );
                })}
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}
