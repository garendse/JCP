import { SupplierBranchDTO } from "./SupplierBranchDTO";

export interface SupplierQuotesDTO {
  id: string;
  supplier: SupplierBranchDTO;
  supplier_id: string;
  quote_item_id: string;
  quoted_price: number;
  part_number?: string;
  count: number;
  quoted_by: string;
  quoted_datetime: string;
  accepted_datetime?: string;
  accepted_by_user_id?: string;
  remarks: string;
}
