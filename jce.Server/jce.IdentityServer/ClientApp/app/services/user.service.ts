import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { User } from '../models/user';
import { HelperService } from './helper.service';
import {HttpClient} from "@angular/common/http";


@Injectable()
export class UserService {
    constructor(private http: HttpClient, private helper: HelperService) { }
    private readonly userEndpoint = 'http://localhost:60383/api/users' ;
    
    getAll(parameters: { filter: any }) {
        let filter = parameters.filter;
        return this.http.get<User>(this.userEndpoint + '?' + UserService.toQueryString({obj: filter}));
    }

    getById(id: number) {
        return this.http.get<User>(this.userEndpoint + id);
    }

    create(user: User) {
        return this.http.post<User>(this.userEndpoint, user);
    }

    update(user: User) {
        return this.http.put<User>(this.userEndpoint + user.id, user);
    }

    delete(id: string) {
        return this.http.delete<User>(this.userEndpoint + id);
    }


    // private helper methods
    static toQueryString(parameters: { obj: any }){
        let obj = parameters.obj;
        const parts = [''];
        for (const property in obj){
            const value =  obj[property];
            if (value != null && value != undefined){
                parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value))  ;
            }
        }

        return parts.join('&');
    }

}
