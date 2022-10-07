import { ChangeEventHandler } from "react";

export function SearchBox({
  value,
  id,
  onChange,
  autoComplete,
}: {
  value: string;
  id: string;
  onChange: ChangeEventHandler<HTMLInputElement>;
  autoComplete?: string[];
}) {
  return (
    <>
      <div className="relative text-gray-600 focus-within:text-gray-400 p-3 z-0">
        <span className="absolute inset-y-0 left-0 flex items-center pl-2">
          <button className="p-1 focus:outline-none focus:shadow-outline">
            <svg
              fill="none"
              stroke="currentColor"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="2"
              viewBox="0 0 24 24"
              className="w-6 h-6"
            >
              <path d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path>
            </svg>
          </button>
        </span>
        <input
          type="tel"
          name="q"
          id="search"
          className="py-2 text-sm w-full rounded-md pl-10 focus:outline-none focus:bg-white focus:text-gray-900 z-0"
          placeholder="Find customer by cellphone number or company name"
          autoComplete="off"
          value={value}
          list={id}
          onChange={onChange}
        />
        <datalist id={id}>
          {autoComplete?.map((val, i) => (
            <option key={i} value={val} />
          ))}
        </datalist>
      </div>
    </>
  );
}
