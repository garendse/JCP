import { AdminTableSchema } from "../components/AdminTable";

const schema: AdminTableSchema = {
  api_suffix: "/api/JobCodes",
  columns: [
    {
      name: "Code",
      prop_name: "code",
      type: "textReadonly",
    },
    {
      name: "Description",
      prop_name: "description",
      type: "text",
    },
    {
      name: "Location",
      prop_name: "location",
      type: "text",
    },
    {
      name: "Cost",
      prop_name: "cost",
      type: "currency",
    },
    {
      name: "Markup",
      prop_name: "markup",
      type: "number",
    },
    {
      name: "Standard Hours",
      prop_name: "standard_hours",
      type: "number",
    },
    {
      name: "Volume",
      prop_name: "standard_volume",
      type: "number",
    },
  ],
  name: "Job Codes",
  className: "mx-auto w-fit min-w-[60%]",
  pk: "code",
};

export default schema;
