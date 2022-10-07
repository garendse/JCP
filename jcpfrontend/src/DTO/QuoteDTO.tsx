import { CustomerDTO } from "./CustomerDTO";
import { QuoteItemDTO } from "./QuoteItemDTO";
import { TechDTO } from "./TechDTO";
import { UserDTO } from "./UserDTO";
import { VehicleDTO } from "./VehicleDTO";

export interface QuoteDTO {
  id: string;
  ro_number: string;
  branch_id: string;
  customer_id: string;
  customer?: CustomerDTO;
  vehicle?: VehicleDTO;
  vehicle_id: string;
  create_user_id: string;
  create_datetime: string;
  update_user_id: string;
  update_datetime: string;
  status: string;
  tech_id?: string;
  items: QuoteItemDTO[];
  create_user?: UserDTO;
  update_user?: UserDTO;
  tech?: TechDTO;
}
