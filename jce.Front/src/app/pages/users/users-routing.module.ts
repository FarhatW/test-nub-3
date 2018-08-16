import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {UsersComponent} from "./users.component";
import {UsersFormComponent} from "./users-form/users-form.component";
import {UsersListComponent} from "./users-list/users-list.component";
import {UserJceFormComponent} from "./users-form/user-jce-form/user-jce-form.component";
import {UserIdentityFormComponent} from "./users-form/user-identity-form/user-identity-form.component";
import {UsersViewComponent} from "./users-view/users-view.component";
import {CesFormInfosComponent} from "../ces/ces-form/ces-form-infos/ces-form-infos.component";
import {ChildrenFormComponent} from "./users-children/children-form/children-form.component";


const routes: Routes = [{
  path: '',
  component: UsersComponent,
  children: [
    {
      path: 'users-list',
      component: UsersListComponent,
    },
    {
      path: 'users-view/:id',
      component: UsersViewComponent,
    },
    {
      path: 'users-form',
      component: UsersFormComponent,
      children: [
        {
          path: 'identity-form/:id',
          component: UserIdentityFormComponent,
        },
        {
          path: 'profile-form/:id',
          component: UserJceFormComponent,
        },
        {
          path: 'new',
          component: UserIdentityFormComponent,
        },
        {
          path: 'new/:id/profile',
          component: UserJceFormComponent,
        },
        {
          path: 'new/:id/Children',
          component: UserIdentityFormComponent,
        },
      ]
    },
    {
      path: 'user/:id',
      component: UsersFormComponent,
    },
    {
      path: ':userId/child-form/new',
      component: ChildrenFormComponent,
    },
  ]
}];
@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [
    RouterModule,
  ],
})
export class UsersRoutingModule {

}

export const routedComponents = [
  UsersComponent,
  UsersFormComponent,
  UsersListComponent,
  UserJceFormComponent,
  UserIdentityFormComponent
];
