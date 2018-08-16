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
import {Children} from "../models/user/child/Children";
import {ChildrenSave} from "../models/user/child/ChildrenSave";

@Injectable()
export class ChildrenService {


  constructor(private  http: HttpClient,private  helperService: HelpersService) {}
  url = environment.apiUrl;

  children: Children[];
  private childrenSource = new BehaviorSubject<Children[]>(this.children);

  private readonly userEndPoint = this.url + 'children/';


  changeChildren(children: Children[]) {
    this.childrenSource.next(children);
  }

  getAll(filter) {
    return this.http.get<JceData<Children>>(this.userEndPoint + '?' + this.helperService.toQueryString(filter));
  }

  getById(id: number, filter) {
    return this.http.get<Children>(this.userEndPoint + id + '?' + this.helperService.toQueryString(filter));
  }

  create(child: ChildrenSave) {

    return this.http.post<ChildrenSave>( this.userEndPoint, child);
  }

  update(child: ChildrenSave) {
    return this.http.put<ChildrenSave>(this.userEndPoint + child.id, child);
  }

  delete(id: number) {

  }

  setNameFirstName(user: UserProfile): string {
    // LogOutAuthComponent
    return user.firstName + ' ' + user.lastName;
  }
}
