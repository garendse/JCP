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
  ],
  name: "Job Codes",
  className: "mx-auto w-fit min-w-[60%]",
  pk: "code",
};

export default schema;
