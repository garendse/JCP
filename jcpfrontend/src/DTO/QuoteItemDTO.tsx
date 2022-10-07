import { QuoteLabourDTO } from "./QuoteLabourDTO";
import { QuotePartDTO } from "./QuotePartDTO";
import { SupplierQuotesDTO } from "./SupplierQuotesDTO";

export interface QuoteItemDTO {
  show: boolean;
  id: string;
  quote_id: string;
  job_code?: string;
  description?: string;
  location?: string;
  sort_order: number;
  labour_hours: number;
  labour_rate: number;
  part_rate: number;
  part_markup: number;
  part_quantity: number;
  auth: boolean;
  subquotes: SupplierQuotesDTO[];
}
