import { AdminTableSchema } from "../components/AdminTable";

const schema: AdminTableSchema = {
  api_suffix: "/api/Vehicles",
  columns: [
    {
      name: "Vin no",
      type: "text",
      prop_name: "vin_number",
    },
    {
      name: "Engine no",
      type: "text",
      prop_name: "engine_number",
    },
    {
      name: "Registration",
      type: "text",
      prop_name: "registration",
    },
    {
      name: "Brand",
      type: "text",
      prop_name: "brand",
    },
    {
      name: "Model",
      type: "text",
      prop_name: "model",
    },
    {
      name: "Year",
      type: "number",
      prop_name: "year",
    },
  ],
  name: "Vehicles",
  className: "mx-auto w-fit min-w-[60%]",
  noadd: true,
  pk: "id",
};

export default schema;
