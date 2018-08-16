import { Injectable} from '@angular/core';
import {CE} from '../models/ce/ce';
import {BehaviorSubject} from "rxjs/BehaviorSubject";
import {HttpClient} from "@angular/common/http";
import {UserProfileSave} from "../models/user/userSave/UserProfileSave";
import {JceData} from "../models/JceData";
import {Role} from "../models/Role/Role";
import {HelpersService} from "../../utils/helpers.service";
import {environment} from "../../../../environments/environment";
import {UserProfile} from "../models/user/UserProfile";

@Injectable()
export class UsersService {


  constructor(private  http: HttpClient,private  helperService: HelpersService) {}
  url = environment.apiUrl;

  userProfiles: UserProfile[];
  private usersSource = new BehaviorSubject<UserProfile[]>(this.userProfiles);

  private readonly userEndPoint = this.url + 'users/';


  changeUsers(userProfile: UserProfile[]) {
    this.usersSource.next(userProfile);
  }

  getAll(filter) {
    return this.http.get<JceData<UserProfile>>(this.userEndPoint + '?' + this.helperService.toQueryString(filter));
  }

  getById(id: number, filter) {
    return this.http.get<UserProfile>(this.userEndPoint + id + '?' + this.helperService.toQueryString(filter));
  }

  create(user: UserProfileSave, admin: boolean) {
    console.log(admin);
    return this.http.post<UserProfileSave>(
      admin ===  true ? this.userEndPoint +'admin' : this.userEndPoint +'person', user);
  }

  update(user: UserProfileSave, admin: boolean) {
    return this.http.put<UserProfileSave>(
      admin === true ? this.userEndPoint + 'admin/' + user.id : this.userEndPoint + 'person/' + user.id, user);
  }

  delete(id: number) {

  }

  setNameFirstName(user: UserProfile): string {
   // LogOutAuthComponent
    return user.firstName + ' ' + user.lastName;
  }
}
