import { SupplierDTO } from "./SupplierDTO";

export interface SupplierBranchDTO {
  id: string;
  supplier_id: string;
  supplier: SupplierDTO;
  name: string;
  address: string;
  lat: number;
  lng: number;
  quote_email: string;
}
