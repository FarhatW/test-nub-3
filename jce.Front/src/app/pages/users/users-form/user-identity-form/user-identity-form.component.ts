///<reference path="../../../../../../node_modules/@angular/core/src/metadata/lifecycle_hooks.d.ts"/>
import {
  AfterContentChecked,
  Component,
  DoCheck,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges
} from '@angular/core';
import {FormUserIdentitySaveModel, UserIdentitySave} from "../../../../@core/data/models/user/UserIdentitySave";
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Subscription} from "rxjs/Subscription";
import {UsersService} from "../../../../@core/data/services/users.service";
import {ToastyService} from "ng2-toasty";
import {NotificationService} from "../../../../@core/data/services/notification.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ToasterService} from "angular2-toaster";
import {EMAIL_REGEX, PASSWORD_REGEX, PHONE_REGEX} from "../../../../@core/utils/formUtils";
import {UserAuthFormService} from "./UserAuthFormService";
import {Role} from "../../../../@core/data/models/Role/Role";
import {RolesService} from "../../../../@core/data/services/roles.service";
import {UserIdentity} from "../../../../@core/data/models/user/UserIdentity";
import {forEach} from "@angular/router/src/utils/collection";
import {Passwords} from "../../../../@core/data/models/user/Passwords";
import {UserIdentityService} from "../../../../@core/data/services/user-Identity.service";

@Component({
  selector: 'ngx-user-identity-form',
  templateUrl: './user-identity-form.component.html',
  styleUrls: ['./user-identity-form.component.scss']
})
export class UserIdentityFormComponent implements OnInit {

  @Output() formIdentity = new EventEmitter<boolean>();
  @Output() isECE = new EventEmitter<boolean>();
  pwd : string;
  pwdc:string;
  IsPasswordConfirm: boolean = false;
  id: number;
  private readonly userInfosQuery: boolean = true;
  query: any = {
    userInfosQuery: this.userInfosQuery,
  };
  isEdit: boolean;
  noUserAuth: boolean = false;
  userAuth: UserIdentitySave;
  userIdentity : UserIdentity;
  userAuthForm: FormGroup;
  formUserAuth: FormUserIdentitySaveModel;
  userFormErrors: any;
  userFormErrorsBool: any;
  password_confirmed: boolean = false;
  formChangeSub: Subscription;
  userSetupBtnText: string;
  submitUserAuthObj: UserIdentitySave;
  submitUserAuthSub: Subscription;
  error: boolean;
  submitting: boolean;
  submitBtnText: string;
  resetBtnText: string;
  cardHeaderText: string;

  availableRoles: Role[] = [] ;
  //   [
  //   {id: 1, name: 'ADMIN', createdBy: '', createdOn: '', updatedBy: '', updatedOn: '', enable : true, rank : 1},
  //   {id: 1, name: 'COMMERCIAL', createdBy: '', createdOn: '', updatedBy: '', updatedOn: '', enable : true, rank : 2},
  //   {id: 1, name: 'RCE', createdBy: '', createdOn: '', updatedBy: '', updatedOn: '', enable : true, rank : 3},
  //   {id: 1, name: 'ECE', createdBy: '', createdOn: '', updatedBy:  '', updatedOn: '', enable : true, rank : 4}
  // ];

  selectedRoles: Role[] = [];
  rolesToSend: number[] = [];
  draggedRole: Role;

  constructor(public toasterService: ToasterService,
              private roleService: RolesService,
              private toastyService: ToastyService,
              private userIdentityService: UserIdentityService,
              private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              public userf: UserAuthFormService,
              public notificationService: NotificationService) {
    route.params.subscribe(p => this.id = +p['id'] || 0);
  }

  ngOnInit() {
    this.selectedRoles = [];


    if (this.id) {
      this.userIdentityService.getById(this.id, this.query).subscribe(
              res => {
                this.userIdentity = res;
                console.log('res', res)

                this.roleService.getAll(this.query).subscribe(role =>{
                  this.availableRoles = role.items;

                  this.setCfg();
                });

              },
              err => {
                console.log('err', err);
                this.noUserAuth = true;

              },

            )
    } else {
      this.setCfg();
    }
  }

  dragStart(event, role: Role) {

    this.draggedRole = role;
  }
  getPasswordValue(event: Passwords) {
    this.pwdc = event.confirmPassword;
    this.pwd  = event.password;
  }
  getPasswordConfirming(event, pwd, pwdc) {
    this.pwd = pwd;
    this.pwdc = pwdc;
   if (event)
      this.IsPasswordConfirm = true;
   else
     this.IsPasswordConfirm = false;
  }

checkeRole(roles: Role[]){
  console.log('role availableRoles', this.availableRoles);
  console.log('role user', roles);
  roles.forEach(item => {
    const result =  this.availableRoles.filter(x => x.id !== item.id);
    console.log('ae',      this.availableRoles.filter(x => x.id !== item.id));
    this.availableRoles = result;
    this.chnageEnabledRole(item);
    this.rolesToSend.push(item.id);
  });


  console.log('role availableRoles', this.availableRoles);
  console.log('role user', roles);
}

  chnageEnabledRole(role: Role){
    if (role.rank > 2  ) {
      console.log(role.name, 'je ne suis pas admin');
      this.availableRoles.forEach(item => {
        if (this.selectedRoles.some(e => e.rank > 2)) {
          console.log('je  suis selectionné ');
          /* vendors contains the element we're looking for */
          if (item.rank > 2) {
            item.enable = true;
          } else
            item.enable = false;
        }
        else {
          console.log('je en suis pas selectionné');
            /* vendors contains the element we're looking for */
              item.enable = true;
          }
      });

    }else {
      console.log(role.name, 'je suis admin');
      this.availableRoles.forEach(item => {
        if (this.selectedRoles.some(e => e.rank <= 2)) {
          console.log('je  suis selectionné ');
          /* vendors contains the element we're looking for */
          if (item.rank <= 2) {
            item.enable = true;
          } else
            item.enable = false;
        }  else {
          console.log('je en suis pas selectionné');
          /* vendors contains the element we're looking for */
          item.enable = true;
        }
      });
    }

  }

  drop(event) {
    if (this.draggedRole) {
      this.selectedRoles = [...this.selectedRoles, this.draggedRole];
      this.rolesToSend = [...this.rolesToSend, this.draggedRole.id];
      this.chnageEnabledRole(this.draggedRole);
      this.userAuthForm.get('roles').setValue(this.rolesToSend);
      this.availableRoles = this.availableRoles.filter((val, i) => i !== this.availableRoles.indexOf(this.draggedRole));
      this.draggedRole = null;
    }
  }

  dragEnd(event) {

   this.draggedRole = null;
  }

  deleteRole(event, role: Role){
    this.selectedRoles.splice(this.selectedRoles.indexOf(role), 1)
    this.availableRoles.push(role);
    this.rolesToSend.splice(this.rolesToSend.indexOf(role.id), 1);
    this.chnageEnabledRole(role);
  }

  resetRoleList(event){
    this.selectedRoles.forEach(item => {
      this.availableRoles.push(item);
    });
    this.selectedRoles = [];
    this.rolesToSend = [];
  }

  setCfg() {
    this.isEdit = !!this.userIdentity;
    if (this.isEdit) {
      this.selectedRoles = this.userIdentity.roles;
      this.checkeRole(this.userIdentity.roles);
      this.IsPasswordConfirm = true;
    }

    this.submitBtnText = this.isEdit ? 'Mettre à jour' : 'Créér un user';
    this.userFormErrors = this.userf.userAuthFormErrors;
    this.userSetupBtnText = 'Configuration du user';
    this.resetBtnText = 'Reset';
    this.userFormErrorsBool = this.userf.userFormErrorsBool;
    this.formUserAuth = this._setFormUser();
    this._buildForm();
    this.cardHeaderText = this.isEdit ? 'Modfier les informations du user' : 'Ajouter un user';
  }

  private _setFormUser() {
    if (!this.isEdit) {
      return new FormUserIdentitySaveModel(
        null,
        null,
        null,
        null,
        null,
      );
    } else {
      return new FormUserIdentitySaveModel(
        this.userIdentity.userName,
        this.userIdentity.email,
        this.userIdentity.createdBy,
        this.userIdentity.updatedBy,
        this.userIdentity.roles.map(a => a.id)
      )
    }
  }

  private _buildForm() {
    this.userAuthForm = this.fb.group({
      userName: [this.formUserAuth.userName, [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100),
      ]],
      roles: [this.formUserAuth.roles, [
        Validators.required,
      ]],
      email: [this.formUserAuth.email, [
        Validators.required,
        Validators.pattern(EMAIL_REGEX)
      ]],
  });

    this.formChangeSub = this.userAuthForm
      .valueChanges
      .subscribe(data => this._onValueChanged());

    if (this.isEdit) {
      const _markDirty = group => {
        for (const i in group.controls) {
          if (group.controls.hasOwnProperty(i)) {
            group.controls[i].markAsDirty()
          }
        }
      };
      _markDirty(this.userAuthForm);
    }
    this._onValueChanged();
  }

  _blurError(formError, event) {
    this.userFormErrorsBool[event.srcElement.id] = !!formError;
  }

  private _onValueChanged() {
    if (!this.userAuthForm) {
      return;
    }
    const _setErrMsgs = (control: AbstractControl, errorsObj: any, field: string) => {

      if (control && control.dirty && control.invalid) {
        const messages = this.userf.userFormValidationMessages[field];

        for (const key in control.errors) {
          if (control.errors.hasOwnProperty(key)) {
            errorsObj[field] += messages[key] + '<br>';
          }
        }
      }
    };
    for (const field in this.userFormErrors) {
      if (this.userFormErrors.hasOwnProperty(field)) {
        // if (field !== 'datesGroup') {
        this.userFormErrors[field] = '';
        _setErrMsgs(this.userAuthForm.get(field), this.userFormErrors, field);
      }
    }
  }

  private _getSubmitObj() {
console.log('this.rolesToSend', this.rolesToSend);
    return new UserIdentitySave(
        this.id,
        this.userAuthForm.get('userName').value,
        this.userAuthForm.get('email').value,
        this.pwd,
        this.pwdc,
       '1',
        '1',
        this.rolesToSend)
  }

  onSubmit() {


    this.submitting = true;

    if (!this.isEdit) {
        console.log('sended user', this._getSubmitObj());
      this.submitUserAuthSub = this.userIdentityService.create(this._getSubmitObj())
        .subscribe(data => this._handleSubmitSuccessUser(data),
          err => this._handleSubmitError(err));
    } else {

      this.submitUserAuthSub = this.userIdentityService.update(this._getSubmitObj())
        .subscribe(data => this._handleSubmitSuccessUser(data),
          err => this._handleSubmitSuccessUser(err));
    }
  }

  private _handleSubmitSuccessUser(res) {

    this.error = false;
    this.submitting = false;
    const title = this.isEdit ? 'user Modifié' : 'user Ajouté'
    let body = 'Le user ' + res.firstName + ' a bien été ';
    body += this.isEdit ? 'modifié.' : 'ajouté.'
    this.toasterService.popAsync(this.notificationService.showSuccessToast(title, body))
    if (this.isEdit) {
      this.router.navigate(['/usersList/user-form/informations/', res.id]);
    }
    this.router.navigate(['usersList/usersList-form/new/' , res.id , 'profile'])
    this.formIdentity.emit(false);
  }

  private _handleSubmitError(err) {
    console.error(err);
    this.submitting = false;
    this.error = true;
    const title = 'une erreur est survenue';
    const body = err.body;
    this.toasterService.popAsync(this.notificationService.showErrorToast(title, body))
  }

  _resetForm(event) {
    this.userAuthForm.reset();
    this.resetRoleList(event);
  }

  ngAfterContentChecked(): void {
  }


}
