import { ExtraOptions, RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import {
    NbAuthComponent,
    NbLoginComponent,
    NbLogoutComponent,
    NbRegisterComponent,
    NbRequestPasswordComponent,
    NbResetPasswordComponent,
} from '@nebular/auth';
import {ConsentComponent} from "./components/consent/consent.component";
import {LoginComponent} from "./components/login/login.component";
import {BrowserModule} from "@angular/platform-browser";
import {LoggedOutResource} from "./Models/LoggedOutResource";
import {LoggedOutComponent} from "./components/loggedOut/loggedOut.component";

const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
//            { path: 'home', component: HomeComponent },
//            { path: 'counter', component: CounterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'consent', component: ConsentComponent },
    { path: 'logOut', component: LoggedOutComponent },
//            { path: 'fetch-data', component: FetchDataComponent },

    { path: '**', redirectTo: 'login' },
];

const config: ExtraOptions = {
    useHash: true,
};

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule, BrowserModule],
})
export class AppRoutingModule {
}
