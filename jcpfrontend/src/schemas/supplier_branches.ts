import { MultiAdminTableSchema } from "../components/MultiAdminTable";

const schema: MultiAdminTableSchema = {
  list_key_prop: "id",
  list_key_search_qry: "branch_id",
  define_key: "supplier_id",
  define_value_prop: "id",
  list_value_prop: "name",
  route: "/api/Suppliers",
  name: "Supplier Branches",
  table: {
    api_suffix: "/api/SupplierBranches",
    columns: [
      {
        name: "Branch Description",
        prop_name: "name",
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
        name: "Lat",
        prop_name: "lat",
        type: "number",
      },
      {
        name: "Long",
        prop_name: "lgn",
        type: "number",
      },
      {
        name: "Contact Person",
        prop_name: "contact_person",
        type: "text",
      },
      {
        name: "Contact Number",
        prop_name: "contact_number",
        type: "text",
      },
      {
        name: "Email",
        prop_name: "email",
        type: "text",
      },
    ],
    name: "Supplier",
    className: "mx-auto w-fit min-w-[60%]",
    pk: "id",
  },
};

export default schema;
