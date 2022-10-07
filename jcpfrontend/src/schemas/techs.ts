import { AdminTableSchema } from "../components/AdminTable";

const schema: AdminTableSchema = {
  api_suffix: "/api/Techs",
  columns: [
    {
      name: "Name",
      prop_name: "name",
      type: "text",
    },
    {
      name: "Surname",
      prop_name: "surname",
      type: "text",
    },
  ],
  name: "Technicians",
  pk: "id",
};

export default schema;
