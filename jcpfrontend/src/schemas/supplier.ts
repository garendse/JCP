import { AdminTableSchema } from "../components/AdminTable";

const schema: AdminTableSchema = {
  api_suffix: "/api/Suppliers",
  columns: [
    {
      name: "Name",
      prop_name: "name",
      type: "text",
    },
    {
      name: "Reg No",
      prop_name: "reg_no",
      type: "text",
    },
    {
      name: "Vat No",
      prop_name: "vat_no",
      type: "text",
    },
    {
      name: "Tax Clearance",
      prop_name: "tax_clearance",
      type: "text",
    },
    {
      name: "Credit Limit",
      prop_name: "credit_limit",
      type: "currency",
    },
    {
      name: "Credit Balance",
      prop_name: "credit_balance",
      type: "currency",
    },
    {
      name: "Tel No",
      prop_name: "tel_num",
      type: "text",
    },
    {
      name: "Address line 1",
      prop_name: "address_line_1",
      type: "text",
    },
    {
      name: "Address line 2",
      prop_name: "address_line_2",
      type: "text",
    },
    {
      name: "Address line 3",
      prop_name: "address_line_3",
      type: "text",
    },
    {
      name: "Postal Code",
      prop_name: "postal",
      type: "text",
    },
    {
      name: "Contact Person",
      prop_name: "contact_person",
      type: "text",
    },
    {
      name: "Contact No",
      prop_name: "contact_no",
      type: "text",
    },
    {
      name: "Contact Email",
      prop_name: "email",
      type: "text",
    },
    {
      name: "After Hours No",
      prop_name: "after_hours_no",
      type: "text",
    },
    {
      name: "Standby Person",
      prop_name: "standby_person",
      type: "text",
    },
    {
      name: "Standby Email",
      prop_name: "standby_email",
      type: "text",
    },
  ],
  name: "Suppliers",
  className: "mx-auto w-fit min-w-[60%]",
  pk: "id",
};

export default schema;
