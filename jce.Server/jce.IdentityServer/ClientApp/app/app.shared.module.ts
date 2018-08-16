import {ErrorHandler, ModuleWithProviders, NgModule} from '@angular/core';
import {APP_BASE_HREF, CommonModule} from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import {LoginComponent} from "./components/login/login.component";
import {ConsentComponent} from "./components/consent/consent.component";
import {BrowserModule} from "@angular/platform-browser";
import {ConsentService} from "./services/consent.service";
import {UserService} from "./services/user.service";
import {AuthenticationService} from "./services/authentication.service";
import {AppErrorHndler} from "./app.error-handler";
import {HelperService} from "./services/helper.service";

import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from "@angular/common/http";
import {NgbModule} from "@ng-bootstrap/ng-bootstrap";
import {JceInterceptor} from "./services/JceInterceptor.service";
import {AppRoutingModule} from "./app-routing.module";
import {LoggedOutComponent} from "./components/loggedOut/loggedOut.component";


@NgModule({
    declarations: [
        AppComponent,
        LoggedOutComponent,
        LoginComponent,
        ConsentComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
    ],
    imports: [
        BrowserModule,
        CommonModule,
        HttpClientModule,
        FormsModule, 
        AppRoutingModule,
    ],
    
    exports: [BrowserModule, ToastyModule],
    providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JceInterceptor, multi: true},
    {provide : ErrorHandler, useClass : AppErrorHndler},
        HelperService,
        ConsentService,
        UserService,
        AuthenticationService,
 
],
    bootstrap: [AppComponent]
})



export class AppModuleShared {
   
}
