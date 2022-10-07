import { memo } from "react";

export const TextCell = memo(
  (props: {
    value: string | number;
    setValue: (newValue: string | number) => void;
    number?: boolean;
    valid?: boolean;
  }) => {
    return (
      <input
        className={
          "w-full w-max-full rounded-none bg-transparent outline-none focus:ring-1 focus:ring-sky-400 " +
          (props.valid === false ? " bg-red-300 " : "") +
          (props.number ? " text-right appearance-none" : "")
        }
        type={props.number ? "number" : "text"}
        value={props.value}
        size={props.value.toString().length}
        onChange={(e) =>
          props.setValue(props.number ? Number(e.target.value) : e.target.value)
        }
      ></input>
    );
  }
);
