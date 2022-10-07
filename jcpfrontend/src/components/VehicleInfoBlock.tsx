import { ChangeEvent, FormEventHandler, useState } from "react";
import { VehicleDTO } from "../DTO/VehicleDTO";

export function VehicleInfoViewBlock(props: { vehicle: VehicleDTO }) {
  const vec = props.vehicle;

  return (
    <div className="mx-3 mb-2 flex flex-wrap">
      <span className="pr-2 whitespace-nowrap">
        <b>Vehicle: </b> {`${vec.brand} ${vec.model} ${vec.year}`}
      </span>
      <span className="pr-2 whitespace-nowrap">
        <b>Registration: </b> {`${vec.registration}`}
      </span>

      <span className="pr-2 whitespace-nowrap">
        <b>VIN: </b> {`${vec.vin_number}`}
      </span>
      <span className="pr-2 whitespace-nowrap">
        <b>Engine Number: </b> {`${vec.engine_number}`}
      </span>
    </div>
  );
}

export function VehicleInfoEditBlock(props: {
  vehicle: VehicleDTO;
  onSave: (data: VehicleDTO) => void;
}) {
  const [vehicle, setVehicle] = useState<VehicleDTO>(props.vehicle);

  const onChange = (e: ChangeEvent<HTMLInputElement>) => {
    let new_vehicle: VehicleDTO = { ...vehicle };

    let index = e.target.id as keyof VehicleDTO;

    if (index != "year") new_vehicle[index] = e.target.value.toUpperCase();
    else new_vehicle.year = parseInt(e.target.value);

    setVehicle(new_vehicle);
  };

  const onSave: FormEventHandler<HTMLFormElement> = (e) => {
    e.preventDefault();
    props.onSave(vehicle);
  };

  return (
    <form className="mx-3 mb-2" onSubmit={onSave}>
      <div className="flex flex-wrap">
        <div className="w-72 flex justify-between items-center">
          <label htmlFor="title">Brand:</label>
          <input
            id="brand"
            className="m-2 rounded-md"
            value={vehicle.brand ?? ""}
            onChange={onChange}
            autoComplete="off"
            type="text"
            required={true}
          ></input>
        </div>
        <div className="w-72 flex justify-between items-center">
          <label>Model:</label>
          <input
            id="model"
            className="m-2 rounded-md"
            onChange={onChange}
            value={vehicle.model ?? ""}
            type="text"
            required={true}
            autoComplete="off"
          ></input>
        </div>
        <div className="w-72 flex justify-between items-center">
          <label>Registration:</label>
          <input
            id="registration"
            className="m-2 rounded-md"
            onChange={onChange}
            value={vehicle.registration ?? ""}
            type="text"
            autoComplete="off"
            required={true}
          ></input>
        </div>
        <div className="w-72 flex justify-between items-center">
          <label>Year:</label>
          <input
            id="year"
            className="m-2 rounded-md"
            onChange={onChange}
            value={vehicle.year ?? 0}
            type="number"
            autoComplete="off"
            required={true}
          ></input>
        </div>
        <div className="w-72 flex justify-between items-center">
          <label>Engine Number:</label>
          <input
            id="engine_number"
            className="m-2 rounded-md"
            onChange={onChange}
            value={vehicle.engine_number ?? ""}
            type="text"
            autoComplete="off"
            required={true}
          ></input>
        </div>
        <div className="w-72 flex justify-between items-center">
          <label>VIN:</label>
          <input
            id="vin_number"
            className="m-2 rounded-md"
            onChange={onChange}
            value={vehicle.vin_number ?? ""}
            type="text"
            autoComplete="off"
            required={true}
          ></input>
        </div>
      </div>
      <button
        className="bg-green-300 p-1 hover:bg-green-200 rounded-md"
        type="submit"
      >
        Confirm
      </button>
    </form>
  );
}
