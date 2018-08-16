import {Role} from "../Role/Role";

export class UserIdentity {

  id: number;
  userName: string;
  email: string;
  phoneNumber: string;
  lockoutEnd: boolean;
  lockoutEnabled: boolean;
  accessFailedCount: number;
  roles: Role[];
  createdBy: string;
  updatedBy: string;
  createdOn: string;
  updatedOn: string

}
