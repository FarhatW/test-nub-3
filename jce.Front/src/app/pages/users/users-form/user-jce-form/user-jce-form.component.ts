///<reference path="../../../../@core/data/models/user/userSave/UserProfileSave.ts"/>

import {Component, EventEmitter, Input, OnInit, Output, SimpleChanges} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {PHONE_REGEX, ZIPCODE_REGEX,EMAIL_REGEX } from '../../../../@core/utils/formUtils';
import {Subscription} from 'rxjs/Subscription';
import {Address, AddressSave} from '../../../../@core/data/models/shared/address';
import {UserProfile} from '../../../../@core/data/models/user/UserProfile';
import {ActivatedRoute, Router} from '@angular/router';
import {ToastyService} from 'ng2-toasty';
import {ToasterService} from 'angular2-toaster';
import {UsersService} from '../../../../@core/data/services/users.service';
import {UserJceFromService} from "./user-jce-from-service";
import {NotificationService} from "../../../../@core/data/services/notification.service";
import {Good} from "../../../../@core/data/models/products/Good";
import {UserIdentityService} from "../../../../@core/data/services/user-Identity.service";
import {UserIdentitySave} from "../../../../@core/data/models/user/UserIdentitySave";
import {UserIdentity} from "../../../../@core/data/models/user/UserIdentity";
import {CE} from "../../../../@core/data/models/ce/ce";
import {CeService} from "../../../../@core/data/services/ce.service";
import {FormUserProfileSaveModel, UserProfileSave} from "../../../../@core/data/models/user/userSave/UserProfileSave";
@Component({
  selector: 'ngx-user-jce-form',
  templateUrl: './user-jce-form.component.html',
  styleUrls: ['./user-jce-form.component.scss']
})
export class UserJceFormComponent implements OnInit {
  selectedUser: UserProfileSave;
  @Input() selectedUserId: number;
  selectUserSub: Subscription;
  @Input() isEdit: Boolean;
  @Input() isVisible: Boolean;
  @Input() isNewUser: boolean;
  @Output() closeUser = new EventEmitter<Good>();
  @Output() savedUserId = new EventEmitter<Good>();

  private readonly userInfosQuery: boolean = true;
  private readonly UserType: number = 1;

  query: any = {
    userInfosQuery: this.userInfosQuery,
  };

  id: number;

  userForm: FormGroup;
  formUser: FormUserProfileSaveModel;
  userFormErrors: any;
  userFormErrorsBool: any;
  formChangeSub: Subscription;
  isAdmin: boolean  = false;

  submitUserSub: Subscription;
  error: boolean;
  submitting: boolean;
  submitBtnText: string;
  userSetupBtnText: string;

  resetBtnText: string;
  cardHeaderText: string;
  userProfileSave: UserProfileSave;
  users: UserProfile[];
  user: UserProfile;
  noUser: boolean = false;
  ces: CE[];
  userIdentity: UserIdentity;


  constructor(public toasterService: ToasterService,
              private toastyService: ToastyService,
              private ceService: CeService,
              private userService: UsersService,
              private userIdentityService: UserIdentityService,
              private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              public userf: UserJceFromService,
              public notificationServiuser: NotificationService,
  ) {
    route.params.subscribe(p => this.id = +p['id'] || 0);
  }

  ngOnInit() {

    if (this.id) {
      this.userIdentityService.getById(this.id, this.query).subscribe(
        res => {
          this.userIdentity = res;
           this.TestAdmin();

          this.userService.getById(this.id, this.query).subscribe(
            result => {
              console.log('usersList result', result) ;
           // this.router.navigate(['usersList/usersList-list']);
              this.user = result;
              console.log('set user', this.user) ;
              this.setCfg();
            })
        },
        err => {
          console.log('err', err);
          this.noUser = true;
        }
      )
    }else {
      this.setCfg();
    }


    if (!this.isAdmin) {
      this.ceService.getAll(this.query).subscribe(
        c => {
          this.ces = c.items;
        },
        err => {
        }
      )
    }

  }


  TestAdmin() {
    this.userIdentity.roles.forEach(item => {
        this.isAdmin = item.rank <= 2;
    });

  }


  setCfg() {
    console.log('user found', this.user);
    this.isEdit = !!this.user;
    this.submitBtnText = this.isEdit ? 'Mettre à jour' : 'Créér un profile';
    this.userFormErrors = this.userf.userFormErrors;
    this.userSetupBtnText = 'Configuration du user';
    this.resetBtnText = 'Reset';
    this.userFormErrorsBool = this.userf.userFormErrorsBool;
    this.formUser = this._setFormuser();
    this._buildForm();
    this.cardHeaderText = this.isEdit ? 'Modfier les informations du user' : 'Ajouter les informations de user';
  }

  private _setFormuser() {
    if (!this.isEdit) {
      return new FormUserProfileSaveModel(
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null
      );
    } else {
      return new FormUserProfileSaveModel(
        this.user.firstName,
        this.user.lastName,
        this.user.email,
        this.user.phone,
        this.user.CreatedBy,
        this.user.UpdatedBy,
        this.user.address.company,
        this.user.address.agency,
        this.user.address.service,
        this.user.address.streetNumber,
        this.user.address.address1,
        this.user.address.address2,
        this.user.address.postalCode,
        this.user.address.city,
        this.user.address.addressExtra,
        this.user.ceId
      )
    }
  }

  private _buildForm() {
    this.userForm = this.fb.group({
      firstName: [this.formUser.firstName, [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(this.userf.nameMax),
      ]],
      lastName: [this.formUser.lastName, [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(this.userf.nameMax),
      ]],
      email: [this.formUser.email, [
        Validators.required,
        Validators.pattern(EMAIL_REGEX)
      ]],
      phone: [this.formUser.phone, [
        Validators.required,
        Validators.pattern(PHONE_REGEX)
      ]],
      company: [this.formUser.company, [
        Validators.required,
        Validators.minLength(this.userf.nameMin),
        Validators.maxLength(this.userf.nameMax)
      ]],
      address1: [this.formUser.address1, [
        Validators.required,
        Validators.minLength(this.userf.nameMin),
        Validators.maxLength(this.userf.nameMax)
      ]],
      postalCode: [this.formUser.postalCode, [
        Validators.required,
        Validators.pattern(ZIPCODE_REGEX)
      ]],
      city: [this.formUser.city, [
        Validators.required,
        Validators.minLength(this.userf.nameMin),
        Validators.maxLength(this.userf.nameMax)
      ]],
      streetNumber: [this.formUser.streetNumber, [
        Validators.min(1)
      ]],
      ceId: [this.formUser.ceId, [
        !this.isAdmin ? Validators.required :   Validators.nullValidator,

      ]],
      service: [this.formUser.service, []],
      agency: [this.formUser.agency, []],
      address2: [this.formUser.address2, []],
      addressExtra: [this.formUser.addressExtra, []],
    }, {
      updateOn: 'submit'
    });

    this.formChangeSub = this.userForm
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
      _markDirty(this.userForm);
    }
    this._onValueChanged();
  }

  _blurError(formError, event) {
    this.userFormErrorsBool[event.srcElement.id] = !!formError;
  }

  private _onValueChanged() {
    if (!this.userForm) {
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
        _setErrMsgs(this.userForm.get(field), this.userFormErrors, field);
      }
    }
  }

  private _getSubmitObj() {

    return new UserProfileSave(
          this.id,
          this.userForm.get('firstName').value,
          this.userForm.get('lastName').value,
          this.userForm.get('email').value,
          this.userForm.get('phone').value,
          '1',
         '1',
          null,
          new AddressSave(
            this.formUser.agency,
            this.userForm.get('company').value,
            this.userForm.get('streetNumber').value,
            this.userForm.get('streetNumber').value,
            this.userForm.get('address1').value,
            this.userForm.get('address2').value,
            this.userForm.get('postalCode').value,
            this.userForm.get('city').value,
            this.formUser.addressExtra,
          ),
          !this.isAdmin ? this.userForm.get('ceId').value :  null

      );


  }

  onSubmit() {

    this.submitting = true;
    console.log('_getSubmitObj()', this._getSubmitObj())
    this.userProfileSave = this._getSubmitObj();
    console.log()
    if (!this.isEdit) {

      this.submitUserSub = this.userService.create(this.userProfileSave, this.isAdmin)
        .subscribe(data => this._handleSubmitSucuserss(data),
          err => this._handleSubmitError(err));
    } else {
      this.submitUserSub = this.userService.update(this.userProfileSave, this.isAdmin)
        .subscribe(data => this._handleSubmitSucuserss(data),
          err => this._handleSubmitSucuserss(err));
    }
  }

  private _handleSubmitSucuserss(res) {
    console.log('res', res)
    this.error = false;
    this.submitting = false;
    const title = this.isEdit ? 'user Modifié' : 'user Ajouté'
    let body = 'Le user ' + res.firstName + ' a bien été ';
    body += this.isEdit ? 'modifié.' : 'ajouté.'
    this.toasterService.popAsync(this.notificationServiuser.showSuccessToast(title, body));
    console.log('is edited', this.isEdit);
    if (!this.isEdit) {
      this.router.navigate(['users/usersL-list']);
    }else {
      this.router.navigate(['users/users-view', this.id]);
    }
  }

  private _handleSubmitError(err) {
    console.error(err);
    this.submitting = false;
    this.error = true;
    const title = 'une erreur est survenue';
    const body = err.error.sqlMessage;
    this.toasterService.popAsync(this.notificationServiuser.showErrorToast(title, body))
  }


  _resetForm() {
    this.userForm.reset();
  }
}

