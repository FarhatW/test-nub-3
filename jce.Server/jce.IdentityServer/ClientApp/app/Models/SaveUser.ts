import {Address} from "./Address";
import {Role} from "./role";

export class SaveUser {
  id: number  ;

  firstName: string;

  lastName: string;

  email: string;

  password: string;

  phone: string;

  loginName: string;

  createdBy: number;

  UpdatedBy: number;

  idCe: number;

  commercialUniqueCode: string;

  address: Address;

  roles: Array<number>;
}
