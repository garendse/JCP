import { AdminTableSchema } from "../components/AdminTable";

const schema: AdminTableSchema = {
  api_suffix: "/api/User",
  columns: [
    {
      name: "Username",
      prop_name: "username",
      type: "textReadonly",
    },
    {
      name: "Name",
      prop_name: "name",
      type: "textReadonly",
    },
    {
      name: "Surname",
      prop_name: "surname",
      type: "textReadonly",
    },
    {
      name: "Role",
      prop_name: "role",
      type: "textReadonly",
    },
    {
      name: "Tel",
      prop_name: "tel_no",
      type: "textReadonly",
    },
  ],
  name: "Users",
  noadd: true,
  pk: "id",
};

export default schema;
