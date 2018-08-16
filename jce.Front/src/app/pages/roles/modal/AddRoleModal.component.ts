import {Component, EventEmitter, Output} from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import {RolesService} from "../../../@core/data/services/roles.service";
import {Role} from "../../../@core/data/models/Role/Role";
import {OAuthService} from 'angular-oauth2-oidc';
import {HelpersService} from "../../../@core/utils/helpers.service";
@Component({
  selector: 'ngx-modal',
  template: `
    <div class="modal-header">
      <span>Ajouter un nouveau role</span>
      <button class="close" aria-label="Close" (click)="closeModal()">
        <span aria-hidden="true">&times;</span>
      </button>
    </div>
    <form name="form" (ngSubmit)="f.form.valid && addRole()" #f="ngForm">
    <div class="modal-body">
      <div class="input-group ">
          <input type="text"  id="name" name="name"
                 [(ngModel)]="role.name" #name="ngModel"
                 required placeholder="nom" class="form-control"
                 [ngClass]="{ 'form-control-danger': f.submitted && !name.valid }">
      </div>
    </div>
    <div class="modal-footer">
      <button class="btn btn-md btn-primary">Ajouter</button>
    </div>
    </form>
  `,
})
export class AddRoleModalComponent {
  @Output() roleAdd = new EventEmitter<boolean>();

  modalHeader: string;
  modalContent = `<h1>test test</h1>`;
  role: Role = {
    id: 0,
    name: '',
    createdBy : '',
    updatedBy: '',
    createdOn: '',
    updatedOn: '',
    enable: true,
    rank: 0,
  };

  constructor(private activeModal: NgbActiveModal,
              private helperService: HelpersService,
              private oauthService: OAuthService,
              private roleService: RolesService) { }

  closeModal() {
    this.activeModal.close();
  }

  addRole(){
      this.roleService.create(this.role).subscribe(r => {
        this.activeModal.close(true);
        this.roleAdd.emit(true);
      })
  }



}
