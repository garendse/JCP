import { ChangeEvent, FormEventHandler, useState } from "react";
import { CustomerDTO } from "../DTO/CustomerDTO";

export function CustomerInfoShowBlock(props: { customer: CustomerDTO }) {
  const cust = props.customer;

  return (
    <div className="mx-3 mb-2 flex flex-wrap">
      <span className="pr-2 whitespace-nowrap">
        <b>Name: </b> {`${cust.title} ${cust.name} ${cust.surname}`}
      </span>
      <span className="pr-2 whitespace-nowrap">
        <b>Email: </b> {cust.email}
      </span>
      <span className="pr-2 whitespace-nowrap">
        <b>Cell: </b> {cust.mobile_no}
      </span>
      <span className="pr-2 whitespace-nowrap">
        <b>Work: </b> {cust.work_no ?? "N/A"}
      </span>
      <span className="pr-2 whitespace-nowrap">
        <b>Home: </b> {cust.home_no ?? "N/A"}
      </span>
      <span className="pr-2 whitespace-nowrap">
        <b>Alternative: </b> {cust.alt_no ?? "N/A"}
      </span>
    </div>
  );
}

export function CustomerInfoEditBlock(props: {
  customer: CustomerDTO;
  onCancel: () => void;
  onSave: (data: CustomerDTO, changed: boolean) => void;
}) {
  const [customer, setCustomer] = useState<CustomerDTO>(props.customer);
  const [changed, setChanged] = useState<boolean>(false);
  const [confirmed, setConfirmed] = useState<boolean>(false);
  const [company, setCompany] = useState<boolean>(
    props.customer.type === "CORPORATE"
  );

  const onChange = (e: ChangeEvent<HTMLInputElement>) => {
    let new_cust: CustomerDTO = { ...customer };

    new_cust[e.target.id as keyof typeof new_cust] = e.target.value;

    setCustomer(new_cust);
    setChanged(true);
  };

  const onSave: FormEventHandler<HTMLFormElement> = (e) => {
    e.preventDefault();
    props.onSave(customer, changed);
  };

  return (
    <form className="mx-3 mb-2" onSubmit={onSave}>
      <h1 className="text-md font-bold">
        Customer Info (or Representative for a Corporation)
      </h1>
      <div className="flex flex-wrap">
        <div className="w-72 flex justify-between items-center">
          <label htmlFor="title">Title:</label>
          <input
            id="title"
            className="m-2 rounded-md"
            value={customer.title ?? ""}
            onChange={onChange}
            autoComplete="off"
            type="text"
            required={true}
          ></input>
        </div>
        <div className="w-72 flex justify-between items-center">
          <label>Name:</label>
          <input
            id="name"
            className="m-2 rounded-md"
            onChange={onChange}
            value={customer.name ?? ""}
            type="text"
            required={true}
            autoComplete="off"
          ></input>
        </div>
        <div className="w-72 flex justify-between items-center">
          <label>Surname:</label>
          <input
            id="surname"
            className="m-2 rounded-md"
            onChange={onChange}
            value={customer.surname ?? ""}
            type="text"
            autoComplete="off"
            required={true}
          ></input>
        </div>
        <div className="w-72 flex justify-between items-center">
          <label>Email:</label>
          <input
            id="email"
            className="m-2 rounded-md"
            onChange={onChange}
            value={customer.email ?? ""}
            type="email"
            autoComplete="off"
            required={true}
          ></input>
        </div>
        <div className="w-72 flex justify-between items-center">
          <label>Mobile:</label>
          <input
            id="mobile_no"
            className="m-2 rounded-md"
            onChange={onChange}
            value={customer.mobile_no ?? ""}
            type="tel"
            autoComplete="off"
            required={true}
          ></input>
        </div>
        <div className="w-72 flex justify-between items-center">
          <label>Home:</label>
          <input
            id="home_no"
            className="m-2 rounded-md"
            onChange={onChange}
            value={customer.home_no ?? ""}
            type="tel"
            autoComplete="off"
          ></input>
        </div>
        <div className="w-72 flex justify-between items-center">
          <label>Work:</label>
          <input
            id="work_no"
            className="m-2 rounded-md"
            onChange={onChange}
            value={customer.work_no ?? ""}
            type="tel"
            autoComplete="off"
          ></input>
        </div>
        <div className="w-72 flex justify-between items-center">
          <label>Alternative:</label>
          <input
            id="alt_no"
            className="m-2 rounded-md"
            onChange={onChange}
            value={customer.alt_no ?? ""}
            type="tel"
            autoComplete="off"
          ></input>
        </div>
      </div>
      <div className="mb-2 select-none" onClick={() => setCompany(!company)}>
        <input
          className="m-2 rounded-md"
          onChange={() => setCompany(!company)}
          type="checkbox"
          checked={company}
          autoComplete="off"
        ></input>
        Corporate Customer
      </div>
      {company && (
        <>
          <h1 className="text-md font-bold">Company info</h1>
          <div className="flex flex-wrap whitespace-nowrap">
            <div className="w-80 flex justify-between items-center">
              <label>Company Name:</label>
              <input
                id="company_name"
                className="m-2 rounded-md"
                onChange={onChange}
                value={customer.company_name ?? ""}
                type="text"
                autoComplete="off"
              ></input>
            </div>
            <div className="w-80 flex justify-between items-center">
              <label>Reg Number:</label>
              <input
                id="reg_number"
                className="m-2 rounded-md"
                onChange={onChange}
                value={customer.reg_number ?? ""}
                type="text"
                autoComplete="off"
              ></input>
            </div>
            <div className="w-80 flex justify-between items-center">
              <label>Vat Number:</label>
              <input
                id="vat_no"
                className="m-2 rounded-md"
                onChange={onChange}
                value={customer.vat_no ?? ""}
                type="text"
                autoComplete="off"
              ></input>
            </div>
          </div>
        </>
      )}
      <h1 className="text-md font-bold">Location Info</h1>
      <div className="flex flex-wrap whitespace-nowrap">
        <div className="w-80 flex justify-between items-center">
          <label>Address line 1:</label>
          <input
            id="address_line_1"
            className="m-2 rounded-md"
            onChange={onChange}
            value={customer.address_line_1 ?? ""}
            type="text"
            autoComplete="off"
          ></input>
        </div>
        <div className="w-80 flex justify-between items-center">
          <label>Address line 2:</label>
          <input
            id="address_line_2"
            className="m-2 rounded-md"
            onChange={onChange}
            value={customer.address_line_2 ?? ""}
            type="text"
            autoComplete="off"
          ></input>
        </div>
        <div className="w-80 flex justify-between items-center">
          <label>Address line 3:</label>
          <input
            id="address_line_3"
            className="m-2 rounded-md"
            onChange={onChange}
            value={customer.address_line_3 ?? ""}
            type="text"
            autoComplete="off"
          ></input>
        </div>
        <div className="w-80 flex justify-between items-center">
          <label>Postal Code</label>
          <input
            id="postal"
            className="m-2 rounded-md"
            onChange={onChange}
            value={customer.postal ?? ""}
            inputMode={"numeric"}
            type="text"
            autoComplete="off"
          ></input>
        </div>
      </div>
      <div
        className="mb-2 select-none"
        onClick={() => setConfirmed(!confirmed)}
      >
        <input
          className="m-2 rounded-md"
          onChange={() => setConfirmed(!confirmed)}
          type="checkbox"
          required={true}
          checked={confirmed}
          autoComplete="off"
        ></input>
        I have confirmed these details are correct.
      </div>
      <button
        className="bg-blue-300 p-1 mr-2 hover:bg-blue-200 rounded-md"
        onClick={props.onCancel}
        type="reset"
      >
        Cancel
      </button>
      <button
        className="bg-green-300 p-1 hover:bg-green-200 rounded-md"
        type="submit"
      >
        Confirm
      </button>
    </form>
  );
}
