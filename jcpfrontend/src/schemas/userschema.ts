import { AdminTableSchema } from "../components/AdminTable";

const schema: AdminTableSchema = {
  api_suffix: "/api/Users",
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
      name: "Password",
      prop_name: "password",
      type: "passwordReadonly",
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
  pk: "id",
};

export default schema;
