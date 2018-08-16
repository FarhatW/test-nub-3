import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {AuthComponent} from './Auth.component';
import {AuthGuardService} from "../../@core/shared/auth/auth-guard.service";



@NgModule({
  imports: [],
  exports: [RouterModule],
})
export class AuthRoutingModule {
}

export const routedComponents = [
  AuthComponent,
];
export const authProviders = [
  AuthGuardService
];
