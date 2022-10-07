export interface JobCodeDTO {
  code: string;
  location?: string;
  description: string;
  cost: number;
  markup: number;
  standard_hours: number;
  standard_volume: number;
}
