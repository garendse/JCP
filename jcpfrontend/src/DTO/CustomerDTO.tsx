export interface CustomerDTO {
  name: string;
  id: string;
  surname: string;
  email: string;
  title: string;
  mobile_no: string;
  home_no?: string;
  work_no?: string;
  alt_no?: string;
  type?: string;
  reg_number?: string;
  vat_no?: string;
  company_name?: string;
  address_line_1?: string;
  address_line_2?: string;
  address_line_3?: string;
  postal?: string;
}
