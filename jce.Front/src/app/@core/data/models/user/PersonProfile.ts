import {UsersService} from "../../services/users.service";
import {Children} from "./child/Children";

export class PersonProfile extends UsersService{
  public ceId: number;
  public child: Children[]
}
