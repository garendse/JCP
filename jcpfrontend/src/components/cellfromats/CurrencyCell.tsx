import currency from "currency.js";
import { useState } from "react";
import config from "../../config";

export function CurrencyCell(props: {
  value: number;
  setValue: (value: number) => void;
}) {
  let [edtVal, setVal] = useState<number>(props.value / 100);
  let [editing, setEditing] = useState(false);

  let decval = edtVal == 0 ? "" : edtVal;

  return (
    <input
      className="font-mono w-full rounded-none bg-transparent outline-none focus:ring-1 focus:ring-sky-400 text-right"
      type={editing ? "number" : "text"}
      value={
        editing
          ? decval
          : currency(props.value, {
              fromCents: true,
            }).format({
              symbol: config.currencySymbol,
              separator: config.currencySeparator,
              decimal: config.currencyDot,
            })
      }
      onChange={(e) => setVal(parseFloat(e.target.value))}
      step={0.01}
      onFocus={(e) => {
        setEditing(true);
      }}
      onBlur={() => {
        setEditing(false);

        let out = edtVal * 100;
        if (isNaN(edtVal)) out = 0;

        props.setValue(out);
      }}
    ></input>
  );
}

export function CurrencyReadonlyCell(props: { value: number }) {
  return (
    <div className="font-mono bg-gray-300/50 w-full h-full text-right px-0.5">
      {currency(props.value, {
        fromCents: true,
      }).format({
        symbol: config.currencySymbol,
        separator: config.currencySeparator,
        decimal: config.currencyDot,
      })}
    </div>
  );
}
