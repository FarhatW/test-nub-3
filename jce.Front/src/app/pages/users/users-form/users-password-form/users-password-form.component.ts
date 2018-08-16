import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {UserAuthFormService} from "../user-identity-form/UserAuthFormService";
import {UsersPasswordFormService} from "./UsersPasswordFormService";
import {ActivatedRoute, Router} from "@angular/router";
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {EMAIL_REGEX, PASSWORD_REGEX} from "../../../../@core/utils/formUtils";

import {Subscription} from "rxjs/Subscription";
import {
  FormUserIdentitySaveModel,
  FormUserPasswordSaveModel, UserIdentitySave
} from "../../../../@core/data/models/user/UserIdentitySave";
import {Passwords} from "../../../../@core/data/models/user/Passwords";

@Component({
  selector: 'ngx-users-password-form',
  templateUrl: './users-password-form.component.html',
  styleUrls: ['./users-password-form.component.scss']
})
export class UsersPasswordFormComponent implements OnInit {

  @Output() IsPasswordConfirm = new EventEmitter<boolean>();
  @Output() pwd = new EventEmitter<Passwords>();


  userFormErrorsBool: any;
  userAuthForm: FormGroup;
  userFormErrors: any;
  userAuth: UserIdentitySave;
  formUserAuth: FormUserPasswordSaveModel;
  formChangeSub: Subscription;
  isEdit: boolean;
  constructor(public userf: UsersPasswordFormService,
              private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {

    this.userFormErrors = this.userf.usersPasswordFormErrors;
    this.userFormErrorsBool = this.userf.usersPasswordFormErrorsBool;
    this.formUserAuth = this._setFormUser();
    this._buildForm();
  }

  isPasswordConfirmed() {
    this.IsPasswordConfirm.emit(this.userAuthForm.get([ 'password']).value ===
      this.userAuthForm.get(['confirmPassword']).value
      && this.userAuthForm.get([ 'confirmPassword']).value !== '')

    this.pwd.emit(new Passwords(this.userAuthForm.get([ 'password']).value,
            this.userAuthForm.get(['confirmPassword']).value));


    return  this.userAuthForm.get([ 'password']).value ===
      this.userAuthForm.get(['confirmPassword']).value
      && this.userAuthForm.get([ 'confirmPassword']).value !== '' ;


  }

  passwordConfirming(c: AbstractControl): { invalid: boolean } {
    if (c.get('password').value !== c.get('confirmPassword').value)  {

      return {invalid: true};
    }
  }

  private _setFormUser() {
    if (!this.isEdit) {
      return new FormUserPasswordSaveModel(
        null,
        null
      );
    } else {
      return new FormUserPasswordSaveModel(
        this.userAuth.password,
        this.userAuth.confirmPassword,
      )
    }
  }

  _blurError(formError, event) {
    this.userFormErrorsBool[event.srcElement.id] = !!formError;
    this.isPasswordConfirmed();
  }

  private _buildForm() {
    this.userAuthForm = this.fb.group({
      password:  [this.formUserAuth.password,[
        Validators.pattern(PASSWORD_REGEX),
        Validators.required
      ]],
      confirmPassword:  [this.formUserAuth.confirmPassword,[
        Validators.required
      ]],


    }, {validator : this.passwordConfirming});

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

  private _onValueChanged() {
    if (!this.userAuthForm) {
      return;
    }
    this.isPasswordConfirmed();
    const _setErrMsgs = (control: AbstractControl, errorsObj: any, field: string) => {

      if (control && control.dirty && control.invalid) {

        const messages = this.userf.usersPasswordFormValidationMessages[field];

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
}
