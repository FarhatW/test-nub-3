
import {NgModule} from "@angular/core";
import {authProviders, AuthRoutingModule} from "./Auth-routing.module";
import {AuthComponent} from "./Auth.component";

import { CommonModule } from '@angular/common';
import { OAuthService} from "angular-oauth2-oidc";


@NgModule({
  imports: [
    CommonModule,
    AuthRoutingModule,
  ],
  declarations: [
    AuthComponent,
  //  LogOutAuthComponent,

  ],
  providers: [
    authProviders,
    OAuthService
  ],
})
export class AuthJceModule { }
