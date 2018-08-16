
import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import {LoggedOutResource} from "../../Models/LoggedOutResource";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthenticationService} from "../../services/authentication.service";
@Component({
    selector: 'app-loggedOut',
    templateUrl: './loggedOut.component.html'
})
export class LoggedOutComponent implements OnInit, OnChanges {
    model: LoggedOutResource;
    logoutId:string;

    constructor(private userService: AuthenticationService, private activatedRoute: ActivatedRoute){
        this.logoutId = this.activatedRoute.snapshot.queryParams['logoutId'];
        console.log("logout ",this.logoutId);
    }
    ngOnInit(): void {



        this.userService.logOut(this.logoutId).subscribe(data => {
            console.log(data);
            this.model = data;
        })
    }

    ngOnChanges(changes: SimpleChanges): void {
        console.log(changes);
    }


}