import { TechDTO } from "./TechDTO";

export interface QuoteLabourDTO {
  quote_id: string;
  line_id: number;
  sub_item_line_id: number;
  tech_id: string;
  description?: string;
  labour_rate?: number;
  labour_hours?: number;
  tech?: TechDTO;
}
