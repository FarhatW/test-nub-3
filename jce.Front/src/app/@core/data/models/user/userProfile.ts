import {Address} from "../shared/address";
import {CE} from "../ce/ce";
import {Children} from "./child/Children";

export class UserProfile {
  public id: number;
  public firstName: string;
  public lastName: string;
  public email: string;
  public phone: string;
  public CreatedBy: string;
  public UpdatedBy: string;
  public cEs: CE[];
  public isEnabled;
  public children: Children[];
  public address: Address;
  public ceId: number;
}
