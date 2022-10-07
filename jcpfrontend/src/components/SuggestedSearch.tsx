import { useState } from "react";
import { AutoComplete } from "../DTO/AutoComplete";

export function SuggestedSearch(props: {
  onChange: (val: string) => void;
  value: string;
  autocomplete: AutoComplete[];
  placeholder?: string;
}) {
  const { autocomplete, onChange, value, placeholder } = props;

  const [focus, setFocus] = useState<boolean>(false);

  let max_len = 0;
  autocomplete.forEach((val) => {
    max_len = Math.max(val.label.length, max_len);
  });

  return (
    <div className="relative block w-full">
      <div className="flex flex-col justify-center w-full">
        <input
          type="text"
          className="bg-transparent w-full rounded-none outline-none focus:ring-1 focus:ring-sky-400 z-0"
          placeholder={placeholder}
          value={value}
          size={max_len}
          onFocus={() => setFocus(true)}
          onBlur={() => setFocus(false)}
          onChange={(e) => onChange(e.target.value)}
        />
        {focus && (
          <ul className="bg-white border border-gray-100 w-min-52 mt-2 absolute top-4 z-50">
            {autocomplete.map((val, i) => (
              <li
                key={i}
                onMouseDown={() => {
                  onChange(val.name);
                  val.onClick(val.name);
                  setFocus(false);
                }}
                className="py-1 border-b-2 border-gray-100 relative cursor-pointer hover:bg-yellow-50 hover:text-gray-900"
              >
                <span className="whitespace-nowrap text-xs">{val.label}</span>
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
}
