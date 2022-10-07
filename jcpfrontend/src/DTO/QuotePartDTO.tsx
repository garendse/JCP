export interface QuotePartDTO {
  quote_id: string;
  line_id: number;
  sub_item_line_id: number;
  part_count?: number;
  part_description?: string;
  part_id?: string;
  part_markup: number;
  part_rate: number;
}
