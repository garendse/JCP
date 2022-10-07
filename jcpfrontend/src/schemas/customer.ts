import { AdminTableSchema } from "../components/AdminTable";

const schema: AdminTableSchema = {
  api_suffix: "/api/Customers",
  columns: [
    {
      name: "Title",
      type: "text",
      prop_name: "title",
    },
    {
      name: "Name",
      type: "text",
      prop_name: "name",
    },
    {
      name: "Surname",
      type: "text",
      prop_name: "surname",
    },
    {
      name: "Company Name",
      type: "text",
      prop_name: "company_name",
    },
    {
      name: "Cust type",
      type: "text",
      prop_name: "type",
    },
    {
      name: "Reg no",
      type: "text",
      prop_name: "reg_number",
    },
    {
      name: "VAT no",
      type: "text",
      prop_name: "vat_no",
    },
    {
      name: "Mobile Number",
      type: "text",
      prop_name: "mobile_no",
    },
    {
      name: "Home Number",
      type: "text",
      prop_name: "home_no",
    },
    {
      name: "Work Number",
      type: "text",
      prop_name: "work_no",
    },
    {
      name: "Alt Number",
      type: "text",
      prop_name: "alt_no",
    },
    {
      name: "Email",
      type: "text",
      prop_name: "email",
    },
    {
      name: "Address Line 1",
      prop_name: "address_line_1",
      type: "text",
    },
    {
      name: "Address Line 2",
      prop_name: "address_line_2",
      type: "text",
    },
    {
      name: "Address Line 3",
      prop_name: "address_line_3",
      type: "text",
    },
    {
      name: "Postal Code",
      prop_name: "postal",
      type: "text",
    },
  ],
  name: "Customers",
  className: "mx-auto w-fit min-w-[60%]",
  pk: "id",
};

export default schema;
