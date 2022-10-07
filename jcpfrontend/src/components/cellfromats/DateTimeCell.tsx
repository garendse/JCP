import { memo } from "react";

export const DateTimeCell = memo(
  (props: {
    value: Date;
    readOnly?: boolean;
    setValue: (newValue: Date) => void;
  }) => {
    const date = props.value.toISOString().replace("Z", "");
    return (
      <input
        className={
          "w-full w-max-full rounded-none bg-transparent outline-none focus:ring-1 focus:ring-sky-400 "
        }
        type="datetime-local"
        value={date}
        readOnly={props.readOnly}
        onChange={(e) => props.setValue(new Date(Date.parse(date)))}
      ></input>
    );
  }
);
