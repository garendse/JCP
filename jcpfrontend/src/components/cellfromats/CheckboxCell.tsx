import { useState } from "react";

export function CheckboxCell(props: {
  value: boolean;
  setValue: (newValue: boolean) => void;
  readonly?: boolean;
}) {
  return (
    <input
      className="rounded-none outline-none w-full focus:ring-1 focus:ring-sky-400"
      type="checkbox"
      disabled={props.readonly}
      checked={props.value}
      onChange={(e) => props.setValue(e.target.checked)}
    ></input>
  );
}
