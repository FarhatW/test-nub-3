import { Component, OnInit } from '@angular/core';
import {CE} from "../../../@core/data/models/ce/ce";
import {UserIdentity} from "../../../@core/data/models/user/UserIdentity";
import {ToastyService} from "ng2-toasty";
import {ToasterService} from "angular2-toaster";
import {ActivatedRoute, Router} from "@angular/router";
import {FormBuilder} from "@angular/forms";
import {UserIdentityService} from "../../../@core/data/services/user-Identity.service";
import {UsersService} from "../../../@core/data/services/users.service";
import {User} from "oidc-client";
import {UserProfile} from "../../../@core/data/models/user/userProfile";
import {LocalDataSource} from "ng2-smart-table";
import {CeService} from "../../../@core/data/services/ce.service";
import {Children} from "../../../@core/data/models/user/child/Children";

@Component({
  selector: 'ngx-users-view',
  templateUrl: './users-view.component.html',
  styleUrls: ['./users-view.component.scss']
})
export class UsersViewComponent implements OnInit {
  id: number;
  userIdentity: UserIdentity;
  userProfile: UserProfile;
  totalCes: number;
  ce: CE;
  ces: CE[];
  children: Children[];
  totalChild: number;
  pageSize: number = 10;
  pageCount: number;

  settingsCe = {

    noDataMessage: 'Cet utilisateur ne possède aucun CE.',


    actions: {
      edit:false,
      add: false,
      delete: false,
    },
    columns: {
      id: {
        title: 'Id',
        type: 'string',
        sort: true,
        editable: false,
      },
      name: {
        title: 'Nom',
        type: 'string',
        editable: false,
      },
      company: {
        title: 'Entreprise',
        type: 'string',
        editable: false,
      },
      address1: {
        title: 'Address',
        type: 'string',
        editable: false,
        sort: true,
      }
    },
  };
  settingsChild = {

    noDataMessage: 'Cet utilisateur ne possède aucun enfant.',


    actions: {

    },
    add: {
      addButtonContent: '<i class="nb-plus"></i>',
      createButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
    },
    edit: {
      editButtonContent: '<i class="nb-edit"></i>',
      saveButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
    },
    delete: {
      deleteButtonContent: '<i class="nb-trash"></i>',
      confirmDelete: true,
    },
    columns: {
      id: {
        title: 'Id',
        type: 'string',

      },
      firstName: {
        title: 'Nom',
        type: 'string'

      },
      lastName: {
        title: 'Prénom',
        type: 'string'

      },
      birthDate: {
        title: 'Age',
        type: 'string'

      },
      amountParticipationCe: {
        title: 'Montant de participation',
        type: 'number'

      }
    },
  }

  constructor(
    private toasterService: ToasterService,
    private toastyService: ToastyService,
    private ceService: CeService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userIdentityService: UserIdentityService,
    private userService: UsersService) {


    route.params.subscribe(p => this.id = +p['id'] || 0);
  }

  sourceCe: LocalDataSource = new LocalDataSource();
  sourceChild: LocalDataSource = new LocalDataSource();

  addChildConfirm(){

  }
  editChildConfirm(){

  }
  deletChildConfirm(){

  }
  ngOnInit() {
    if (this.id) {
      this.userIdentityService.getById(this.id, null)
        .subscribe(res => {
            this.userIdentity = res;
          },
          err => {
            if (err.status === 404) {
              this.router.navigate(['usersList/usersList-list'])
            }
          });

      this.userService.getById(this.id, null)
        .subscribe(res => {
            this.userProfile = res;
            if (this.userProfile.ceId !== undefined) {
              console.log('children', this.userProfile.children);
              this.children = this.userProfile.children;
              this.refreshChildrenTable();
              this.getCE(this.userProfile.ceId);
            }else {
              this.ces = this.userProfile.cEs;
              this.refreshCeTable();

            }


          },
          err => {
            if (err.status === 404) {
              this.router.navigate(['usersList/usersList-list'])
            }
          });

    }

  }

  refreshCeTable() {
    this.sourceCe.load(this.ces);
    this.sourceCe.setPaging(1, 10, true);
    this.totalCes = this.ces.length;
    this.pageCount = Math.ceil(this.totalCes / this.pageSize);
  }

  refreshChildrenTable() {
    this.sourceChild.load(this.children);
    this.sourceChild.setPaging(1, 10, true);
    this.totalChild = this.children.length;
    this.pageCount = Math.ceil(this.totalChild / this.pageSize);
  }
  getCE(idCe: number) {
    this.ceService.getById(idCe, null)
      .subscribe(res => {
          this.ce = res;
        },
        err => {
          if (err.status === 404) {
            this.router.navigate(['usersList/usersList-list'])
          }
        });
  }

}
