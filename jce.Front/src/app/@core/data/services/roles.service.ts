import { Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Role} from "../models/Role/Role";
import {JceData} from "../models/JceData";
import {HelpersService} from "../../utils/helpers.service";

@Injectable()
export class RolesService {


  constructor( private  http: HttpClient, private helperService: HelpersService) {}

  private readonly roleEndPoint = 'http://localhost:5000/api/roles/';

  getAll(filter) {
    return this.http.get<JceData<Role>>(this.roleEndPoint + '?' + this.helperService.toQueryString(filter));
  }

  getById(id: number, filter) {

  }

  create(role: Role) {
    this.helperService.verifyAutorData(role, true)
    return this.http.post<Role>(this.roleEndPoint, role);
  }

  update(role: Role) {
       this.helperService.verifyAutorData(role, false)

    return this.http.put<Role>(this.roleEndPoint + role.id, role);
  }

  delete(id: number) {
    return this.http.delete(this.roleEndPoint + id);
  }


}
