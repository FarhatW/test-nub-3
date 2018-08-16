///<reference path="../../../../../../node_modules/@angular/core/src/metadata/lifecycle_hooks.d.ts"/>
import {Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges} from '@angular/core';
import {CE, FormCEModel} from '../../../../@core/data/models/ce/ce';
import {CeService} from '../../../../@core/data/services/ce.service';
import {ActivatedRoute, Router} from '@angular/router';
import {
  AbstractControl, AsyncValidator, Form, FormBuilder, FormControl, FormGroup, ValidationErrors,
  Validators
} from '@angular/forms';
import {trigger, state, style, animate, transition} from '@angular/animations';
import {ToastyService, ToastyConfig} from 'ng2-toasty';
import {Toast, ToasterConfig, ToasterService} from 'angular2-toaster';
import {CeSave} from '../../../../@core/data/models/ce/ceSave';
import {CeFormService} from '../ce-form.service';
import {PHONE_REGEX, ZIPCODE_REGEX, _compare} from '../../../../@core/utils/formUtils';
import {Subscription} from 'rxjs/Subscription';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import {Address} from '../../../../@core/data/models/shared/address';
import {NotificationService} from '../../../../@core/data/services/notification.service';
import {UsersService} from '../../../../@core/data/services/users.service';
import {UserProfile} from '../../../../@core/data/models/user/UserProfile';
import {expandCollapse} from '../../../../@theme/animations/collapse.animation';


@Component({
  selector: 'ngx-ces-form-infos',
  templateUrl: './ces-form-infos.component.html',
  styleUrls: ['./ces-form-infos.component.scss'],
  animations: [expandCollapse]
})
export class CesFormInfosComponent implements OnInit, OnChanges, OnDestroy {

  private readonly CeInfosQuery: boolean = true;
  private readonly UserType: number = 1;
  private readonly NoPageSize: boolean = true;
  private readonly  pageSize: number = 0;

  query: any = {
    CeInfosQuery: this.CeInfosQuery,
    userType: this.UserType,
    pageSize: 0
  };
  noCe: boolean = false;
  ce: CE;
  id: number;

  ceForm: FormGroup;
  formCE: FormCEModel;
  ceFormErrors: any;
  ceFormErrorsBool: any;
  formChangeSub: Subscription;
  isEdit: boolean;
  submitCeObj: CeSave;
  submitCeSub: Subscription;
  error: boolean;
  submitting: boolean;
  submitBtnText: string;
  ceSetupBtnText: string;
  catalogBtnText: string;
  resetBtnText: string;
  cardHeaderText: string;
  users: UserProfile[];


  constructor(public toasterService: ToasterService,
              private toastyService: ToastyService,
              private ceService: CeService,
              private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              public cef: CeFormService,
              public notificationService: NotificationService,
              public userService: UsersService,
  ) {
    route.params.subscribe(p => this.id = +p['id'] || 0);
  }

  ngOnInit() {
    if (this.id) {
      this.ceService.getById(this.id, this.query).subscribe(
        res => {
          this.ce = res;
          this.cef.changeCE(this.ce);
          this.cef.changeFormStep(1);
        },
        err => {
          this.noCe = true;
        },
        () => {
          this.setCfg();
        }
      )
    } else {
      this.cef.changeCE(null);
      this.setCfg();
    }
    this.userService.getAll(this.query).subscribe(
      users => {
        this.users = users.items;
      },
      err => {
        console.log(err);
      }
    )
  }

  ngOnChanges(changes: SimpleChanges) {
    console.log('changes', changes);
  }

  setCfg() {
    this.isEdit = !!this.ce;
    this.submitBtnText = this.isEdit ? 'Mettre à jour' : 'Créér un CE';
    this.ceFormErrors = this.cef.ceFormErrors;
    this.ceSetupBtnText = 'Configuration du CE';
    this.catalogBtnText = 'Configuration du Catalogue';
    this.resetBtnText = 'Reset';
    this.ceFormErrorsBool = this.cef.ceFormErrorsBool;
    this.formCE = this._setFormCe();
    console.log('formce', this.formCE);
    this._buildForm();
    this.cardHeaderText = this.isEdit ? 'Modfier les informations du CE' : 'Ajouter un CE';
  }

  private _setFormCe() {
    if (!this.isEdit) {
      return new FormCEModel(
        null,
        null,
        null,
        null,
        true,
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
      );
    } else {
      return new FormCEModel(
        this.ce.name,
        this.ce.fax,
        this.ce.telephone,
        this.ce.logo,
        this.ce.actif,
        this.ce.isDeleted,
        this.ce.createdBy,
        this.ce.updatedBy,
        this.ce.address.company,
        this.ce.address.agency,
        this.ce.address.service,
        this.ce.address.streetNumber,
        this.ce.address.address1,
        this.ce.address.address2,
        this.ce.address.postalCode,
        this.ce.address.city,
        this.ce.address.addressExtra,
        this.ce.adminJceProfileId
      )
    }
  }

  private _buildForm() {
    this.ceForm = this.fb.group({
      name: [this.formCE.name, [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(this.cef.nameMax),
      ]],
      fax: [this.formCE.fax, [
        Validators.pattern(PHONE_REGEX)
      ]],
      telephone: [this.formCE.telephone, [
        Validators.required,
        Validators.pattern(PHONE_REGEX)
      ]],
      company: [this.formCE.company, [
        Validators.required,
        Validators.minLength(this.cef.nameMin),
        Validators.maxLength(this.cef.nameMax)
      ]],
      address1: [this.formCE.address1, [
        Validators.required,
        Validators.minLength(this.cef.nameMin),
        Validators.maxLength(this.cef.nameMax)
      ]],
      postalCode: [this.formCE.postalCode, [
        Validators.required,
        Validators.pattern(ZIPCODE_REGEX)
      ]],
      city: [this.formCE.city, [
        Validators.required,
        Validators.minLength(this.cef.nameMin),
        Validators.maxLength(this.cef.nameMax)
      ]],
      streetNumber: [this.formCE.streetNumber, [
        Validators.min(1)
      ]],
      adminJceProfileId: [this.formCE.adminJceProfileId, [
        Validators.required,
        Validators.nullValidator
      ]],
      isActif: [this.formCE.actif, []],
      isDeleted: [this.formCE.isDeleted, []],
      logo: [this.formCE.logo, []],
      agency: [this.formCE.agency, []],
      service: [this.formCE.service, []],
      address2: [this.formCE.address2, []],
      addressExtra: [this.formCE.addressExtra, []],
    }, {
      updateOn: 'submit'
    });

    this.formChangeSub = this.ceForm
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
      _markDirty(this.ceForm);
    }
    this._onValueChanged();
  }

  _blurError(formError, event) {
    this.ceFormErrorsBool[event.srcElement.id] = !!formError;
  }

  private _onValueChanged() {
    if (!this.ceForm) {
      return;
    }
    const _setErrMsgs = (control: AbstractControl, errorsObj: any, field: string) => {
      if (control && control.dirty && control.invalid) {
        const messages = this.cef.ceFormValidationMessages[field];
        for (const key in control.errors) {
          if (control.errors.hasOwnProperty(key)) {
            errorsObj[field] += messages[key] + '<br>';
          }
        }
      }
    };
    for (const field in this.ceFormErrors) {
      if (this.ceFormErrors.hasOwnProperty(field)) {
        // if (field !== 'datesGroup') {
        this.ceFormErrors[field] = '';
        _setErrMsgs(this.ceForm.get(field), this.ceFormErrors, field);
      }
    }
  }

  private _getSubmitObj() {

    const ceSave: CeSave = new CeSave();
    ceSave.address = new Address();
    if (this.id) {
      ceSave.id = this.id;
    }
    ceSave.name = this.ceForm.get('name').value;
    ceSave.fax = this.ceForm.get('fax').value;
    ceSave.actif = this.ceForm.get('isActif').value;
    ceSave.telephone = this.ceForm.get('telephone').value;
    ceSave.address.streetNumber = this.ceForm.get('streetNumber').value;
    ceSave.address.address1 = this.ceForm.get('address1').value;
    ceSave.address.address2 = this.ceForm.get('address2').value;
    ceSave.address.postalCode = this.ceForm.get('postalCode').value;
    ceSave.address.city = this.ceForm.get('city').value;
    ceSave.address.company = this.ceForm.get('company').value;
    ceSave.address.agency = this.formCE.agency;
    ceSave.address.addressExtra = this.formCE.addressExtra;
    ceSave.isDeleted = this.formCE.isDeleted;
    ceSave.CreatedBy = this.formCE.createdBy;
    ceSave.UpdatedBy = this.formCE.createdBy;
    ceSave.adminJceProfileId = this.ceForm.get('adminJceProfileId').value;;

    return ceSave;
  }

  onSubmit() {

    this.submitting = true;
    console.log('_getSubmitObj()', this._getSubmitObj())
    this.submitCeObj = this._getSubmitObj();
    console.log()
    if (!this.isEdit) {
      this.submitCeSub = this.ceService.create(this.submitCeObj)
        .subscribe(data => this._handleSubmitSuccess(data),
          err => this._handleSubmitError(err));
    } else {
      this.submitCeSub = this.ceService.update(this.id, this.submitCeObj)
        .subscribe(data => this._handleSubmitSuccess(data),
          err => this._handleSubmitSuccess(err));
    }
  }

  private _handleSubmitSuccess(res) {
    console.log('res', res)
    this.error = false;
    this.submitting = false;
    const title = this.isEdit ? 'CE Modifié' : 'CE Ajouté'
    let body = 'Le CE ' + res.name + ' a bien été ';
    body += this.isEdit ? 'modifié.' : 'ajouté.'
    this.toasterService.popAsync(this.notificationService.showSuccessToast(title, body))
    if (!this.isEdit) {
      this.router.navigate(['/ces/ce-form/informations/', res.id]);
    }
  }

  private _handleSubmitError(err) {
    console.error(err);
    this.submitting = false;
    this.error = true;
    const title = 'une erreur est survenue';
    const body = err.body;
    this.toasterService.popAsync(this.notificationService.showErrorToast(title, body))
  }

  _ceSetupButtonClick() {
    this.router.navigate(['/ces/ce-form/configce/', this.ce.id]);
  }

  _catalogButtonClick() {
    this.router.navigate(['/ces/ce-form/catalogue/', this.ce.id]);
  }

  _resetForm() {
    this.ceForm.reset();
  }

  public ngOnDestroy(): void {
    if (this.submitCeSub) {
      this.submitCeSub.unsubscribe();
    }
  }
}
