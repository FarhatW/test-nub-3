import { Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {UserProfileSave} from "../models/user/userSave/UserProfileSave";
import {HelpersService} from "../../utils/helpers.service";
import { UserIdentitySave} from "../models/user/UserIdentitySave";
import {JceData} from "../models/JceData";
import {UserIdentity} from "../models/user/UserIdentity";


@Injectable()
export class UserIdentityService {


  constructor(private  http: HttpClient,private  helperService: HelpersService) {}

  private readonly userEndPoint = 'http://localhost:5000/api/users/';
  getAll(filter) {
    return this.http.get<JceData<UserIdentity>>(this.userEndPoint + '?' + this.helperService.toQueryString(filter));
  }
  getById(id: number, filter) {
    return this.http.get<UserIdentity>(this.userEndPoint + id + '?' + this.helperService.toQueryString(filter));
  }
  create(user: UserIdentitySave) {
    return this.http.post<UserIdentity>(this.userEndPoint, user);
  }

  update(user: UserIdentitySave) {
    return this.http.put<UserIdentity>(this.userEndPoint + user.id, user);
  }

}
