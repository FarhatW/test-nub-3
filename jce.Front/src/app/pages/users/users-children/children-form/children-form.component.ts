import {Component, Input, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Subscription} from "rxjs/Subscription";
import {FormUserProfileSaveModel, UserProfileSave} from "../../../../@core/data/models/user/userSave/UserProfileSave";
import {UserProfile} from "../../../../@core/data/models/user/userProfile";
import {ChildrenSave, FormChildrenSave} from "../../../../@core/data/models/user/child/ChildrenSave";
import {ToasterService} from "angular2-toaster";
import {ToastyService} from "ng2-toasty";
import {ActivatedRoute, Router} from "@angular/router";
import {NotificationService} from "../../../../@core/data/services/notification.service";
import {UserJceFromService} from "../../users-form/user-jce-form/user-jce-from-service";
import {ChildrenService} from "../../../../@core/data/services/children.service";
import {Children} from "../../../../@core/data/models/user/child/Children";
import {ChildService} from "./ChildService";
import {
  EMAIL_REGEX,
  NUMBERS_AND_COMMA_REGEX,
  NUMBERS_REGEX,
  PHONE_REGEX,
  ZIPCODE_REGEX
} from "../../../../@core/utils/formUtils";
import {AddressSave} from "../../../../@core/data/models/shared/address";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'ngx-children-form',
  templateUrl: './children-form.component.html',
  styleUrls: ['./children-form.component.scss'],
  providers: [DatePipe]
})
export class ChildrenFormComponent implements OnInit {

  @Input() isEdit: Boolean;

  private readonly childInfosQuery: boolean = true;
  query: any = {
    childInfosQuery: this.childInfosQuery,
  };


  childForm: FormGroup;
  formChild: FormChildrenSave;
  childFormErrors: any;
  childFormErrorsBool: any;
  formChangeSub: Subscription;
  submitChildSub: Subscription;

  error: boolean;
  submitting: boolean;
  submitBtnText: string;
  childSetupBtnText: string;

  resetBtnText: string;
  cardHeaderText: string;
  childSave: ChildrenSave;
  children: Children[];
  child: Children;
  noChild: boolean = false;
  id: number;
  userId: number;

  constructor(
              public childService:  ChildrenService,
              public toasterService: ToasterService,
              private toastyService: ToastyService,
              private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              public childF: ChildService,
              public notificationServiuser: NotificationService) { }

  ngOnInit() {

    if (this.id) {
          this.childService.getById(this.id, this.query).subscribe(
            result => {
              console.log('usersList result', result) ;

              this.setCfg();
            }),
        err => {
          console.log('err', err);
          this.noChild = true;
        }

    }else {
      this.setCfg();
    }
  }

  setCfg() {
    console.log('user found', this.child);
    this.isEdit = !!this.child;
    this.submitBtnText = this.isEdit ? 'Mettre à jour' : 'Créér un profile';
    this.childFormErrors = this.childF.userFormErrors;
    this.childSetupBtnText = 'Configuration du user';
    this.resetBtnText = 'Reset';
    this.childFormErrorsBool = this.childF.userFormErrorsBool;
    this.formChild = this._setFormuser();
    this._buildForm();
    this.cardHeaderText = this.isEdit ? 'Modfier les informations de l\'enfant' : 'Ajouter les informations de l\'enfant';
  }
  private _setFormuser() {
    if (!this.isEdit) {
      return new FormChildrenSave(
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
        false ,
      );
    } else {
      return new FormChildrenSave(
        this.child.firstName,
        this.child.lastName,
        this.child.birthDate,
        this.child.gender,
        true,
        this.child.amountParticipationCe,
        this.userId,
        this.child.createdBy,
        this.child.updatedBy,
        this.child.createdOn,
        this.child.updatedOn,
        false

      )
    }
  }

  _resetForm() {
    this.childForm.reset();
  }
  private _buildForm() {
    this.childForm = this.fb.group({
      firstName: [this.formChild.firstName, [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(this.childF.nameMax),
      ]],
      lastName: [this.formChild.lastName, [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(this.childF.nameMax),
      ]],
      birthDate: [this.formChild.birthDate, [
        Validators.required,
      ]],
      gender: [this.formChild.gender, [
        Validators.required
      ]],
      amountParticipationCe: [this.formChild.amountParticipationCe, [
        Validators.required,
        Validators.pattern(NUMBERS_AND_COMMA_REGEX)
      ]]

    }, {
    });

    this.formChangeSub = this.childForm
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
      _markDirty(this.childForm);
    }
    this._onValueChanged();
  }

  private _onValueChanged() {
    if (!this.childForm) {
      return;
    }
    const _setErrMsgs = (control: AbstractControl, errorsObj: any, field: string) => {
      if (control && control.dirty && control.invalid) {
        const messages = this.childF.userFormValidationMessages[field];
        for (const key in control.errors) {
          if (control.errors.hasOwnProperty(key)) {
            errorsObj[field] += messages[key] + '<br>';
          }
        }
      }
    };
    for (const field in this.childFormErrors) {
      if (this.childFormErrors.hasOwnProperty(field)) {
        // if (field !== 'datesGroup') {
        this.childFormErrors[field] = '';
        _setErrMsgs(this.childForm.get(field), this.childFormErrors, field);
      }
    }
  }
  _blurError(formError, event) {
    this.childFormErrorsBool[event.srcElement.id] = !!formError;
  }

  private _getSubmitObj() {

    return new ChildrenSave(
      this.id,
      this.childForm.get('firstName').value,
      this.childForm.get('lastName').value,
      this.childForm.get('birthDate').value,
      this.childForm.get('gender').value,
      true,
      this.childForm.get('amountParticipationCe').value,
      this.userId,
      1,
      1,
      '1',
      '1',
    );
  }
  onSubmit() {

    this.submitting = true;
    console.log('_getSubmitObj()', this._getSubmitObj())
    this.childSave = this._getSubmitObj();
    console.log()
    if (!this.isEdit) {

      this.submitChildSub = this.childService.create(this.childSave)
        .subscribe(data => this._handleSubmitSucuserss(data),
          err => this._handleSubmitError(err));
    } else {
      this.submitChildSub = this.childService.update(this.childSave)
        .subscribe(data => this._handleSubmitSucuserss(data),
          err => this._handleSubmitSucuserss(err));
    }
  }
  private _handleSubmitSucuserss(res) {
    console.log('res', res)
    this.error = false;
    this.submitting = false;
    const title = this.isEdit ? 'user Modifié' : 'user Ajouté'
    let body = 'L\'enfant ' + res.firstName + ' a bien été ';
    body += this.isEdit ? 'modifié.' : 'ajouté.'
    this.toasterService.popAsync(this.notificationServiuser.showSuccessToast(title, body));
    console.log('is edited', this.isEdit);
    if (!this.isEdit) {
      this.router.navigate(['users/users-list']);
    }else {
      this.router.navigate(['usersList/users-view', this.id]);
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

}
