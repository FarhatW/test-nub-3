///<reference path="../../../../../../node_modules/@angular/core/src/metadata/directives.d.ts"/>
import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CatalogService} from '../../../../@core/data/services/catalog.service';
import {CeService} from '../../../../@core/data/services/ce.service';
import {CE} from '../../../../@core/data/models/ce/ce';
import {Catalog, FormCatalogModel} from '../../../../@core/data/models/catalog/catalog';
import {DatePipe} from '@angular/common';
import {CatalogSave} from "../../../../@core/data/models/catalog/catalogSave";
import {KeyValuePairEnum} from "../../../../@core/data/models/Enums/keyValuePair.enum";
import {CatalogTypeService} from "../../../../@core/data/services/catalog-type.service";
import {AbstractControl, FormBuilder, FormControl, FormGroup, NgForm, Validators} from "@angular/forms";
import {CatalogChoiceTypeService} from "../../../../@core/data/services/catalog-choice-type.service";
import {CustomValidators} from "../../../../@theme/validators/customValidators";
import {Subscription} from "rxjs/Subscription";
import {composeAsyncValidators} from "@angular/forms/src/directives/shared";
import {CeFormService} from "../ce-form.service";
import {Address} from "../../../../@core/data/models/shared/address";
import {CeSave} from "../../../../@core/data/models/ce/ceSave";
import {ToasterService} from "angular2-toaster";
import {NotificationService} from "../../../../@core/data/services/notification.service";
import {DateConverterService} from "../../../../@core/utils/dateConverter.service";

@Component({
  selector: 'ngx-ces-catalog-form',
  templateUrl: './ces-catalog-form.component.html',
  styleUrls: ['./ces-catalog-form.component.scss'],
  providers: [DatePipe]
})
export class CesCatalogFormComponent implements OnInit {

  private readonly CatalogQuery: boolean = true;
  query: any = {CatalogQuery: this.CatalogQuery};
  isEdit: boolean;
  noCE: boolean = false;
  id: number;
  ce: CE;
  catalog: Catalog = new Catalog();
  catalogSave: CatalogSave = new CatalogSave();
  catalogTypes: KeyValuePairEnum[] = [];
  catalogChoiceTypes: KeyValuePairEnum[] = [];
  catalogForm: FormGroup;
  formCatalog: FormCatalogModel;
  error: boolean;
  submitCatalogObj: CatalogSave;
  submitCatalogSub: Subscription;
  submitting: boolean;
  submitBtnText: string;
  catalogFormErrors: any;
  catalogFormErrorsBool: any;
  formChangeSub: Subscription;

  constructor(private catalogService: CatalogService,
              public toasterService: ToasterService,
              private dateConverterService: DateConverterService,
              private catalogTypeService: CatalogTypeService,
              private catalogChoiceTypeService: CatalogChoiceTypeService,
              public notificationService: NotificationService,
              private route: ActivatedRoute,
              private ceService: CeService,
              private fb: FormBuilder,
              private datePipe: DatePipe,
              public cef: CeFormService,
              private router: Router) {
    route.params.subscribe(p => this.id = +p['id'] || 0);
  }

  ngOnInit() {
    this.cef.changeFormStep(3);

    this.catalogChoiceTypeService.getAll()
      .subscribe(cct => {
        this.catalogChoiceTypes = cct;
      });
    this.catalogTypeService.getAll()
      .subscribe(ct => {
        this.catalogTypes = ct;
      });

    this.ceService.getById(this.id, this.query)
      .subscribe(ce => {
        console.log('ce', ce);
          this.catalog = ce.catalog;
        },
        err => {
          console.log('err', err);
        },
        () => this._setCfg());
  }


  _setCfg() {
    this.isEdit = !!this.catalog;
    console.log('thisedit', this.isEdit);
    this.submitBtnText = this.isEdit ? 'Mettre à jour' : 'Créér un Catalogue';
    this.catalogFormErrors = this.cef.catalogFormErrors;
    this.catalogFormErrorsBool = this.cef.catalogFormErrors;
    this.formCatalog = this._setFormCe();
    this._buildForm();

  }

  private _setFormCe() {
    if (!this.isEdit) {
      return new FormCatalogModel(
        null,
        null,
        1,
        false,
        null,
        false,
        null,
        1,
        null,
        null,
        null,
        null
      );
    } else return new FormCatalogModel(
      this.catalog.id,
      this.catalog.ceId,
      this.catalog.catalogType,
      this.catalog.isActif,
      this.catalog.expirationDate,
      this.catalog.displayPrice,
      this.catalog.productChoiceQuantity,
      this.catalog.catalogChoiceTypeId,
      this.catalog.booksQuantity,
      this.catalog.toysQuantity,
      this.catalog.subscriptionQuantity,
      this.catalog.catalogGoods,
    )
  }

  private _buildForm() {
    this.catalogForm = this.fb.group({
      catalogType: [this.formCatalog.catalogType, [
        Validators.required
      ]],
      isActif: [this.formCatalog.isActif],
      expirationDate: [this.formCatalog.expirationDate, [
        Validators.required,
        CustomValidators.expirationDate
      ]],
      displayPrice: [this.formCatalog.displayPrice],
      productChoiceQuantity: [this.formCatalog.productChoiceQuantity, [
        Validators.min(0),
        Validators.max(5),
      ]],
      catalogChoiceTypeId: [this.formCatalog.catalogChoiceTypeId],
      booksQuantity: [this.formCatalog.booksQuantity, [
        Validators.min(0),
        Validators.max(5)
      ]],
      toysQuantity: [this.formCatalog.toysQuantity, [
        Validators.min(0),
        Validators.max(5),
      ]],
      subscriptionQuantity: [this.formCatalog.subscriptionQuantity, [
        Validators.min(0),
        Validators.max(5),
      ]]
    })

    this.formChangeSub = this.catalogForm
      .valueChanges
      .subscribe(data => this._onValueChanged())
    if (this.isEdit) {
      const _markDirty = group => {
        for (const i in group.controls) {
          if (group.controls.hasOwnProperty(i)) {
            group.controls[i].markAsDirty()
          }
        }
      };
      _markDirty(this.catalogForm);
    }
    this._onValueChanged();
  }

  private _onValueChanged() {
    if (!this.catalogForm) {
      return;
    }
    const _setErrMsgs = (control: AbstractControl, errorsObj: any, field: string) => {
      if (control && control.dirty && control.invalid) {
        const messages = this.cef.catalogFormValidationMessages[field];
        for (const key in control.errors) {
          if (control.errors.hasOwnProperty(key)) {
            errorsObj[field] += messages[key] + '<br>';
          }
        }
      }
    };
    for (const field in this.catalogFormErrors) {
      if (this.catalogFormErrors.hasOwnProperty(field)) {
        this.catalogFormErrors[field] = '';
        _setErrMsgs(this.catalogForm.get(field), this.catalogFormErrors, field);
      }
    }
  }

  private _getSubmitObj() {

    const catalogSave: CatalogSave = new CatalogSave();
    if (this.isEdit) {
      catalogSave.id = this.catalog.id;
    }
    catalogSave.ceId = this.id;
    catalogSave.catalogType = this.catalogForm.get('catalogType').value;
    catalogSave.isActif = this.catalogForm.get('isActif').value;
    catalogSave.expirationDate = this.catalogForm.get('expirationDate').value;
    catalogSave.displayPrice = this.catalogForm.get('displayPrice').value;
    catalogSave.productChoiceQuantity = this.catalogForm.get('productChoiceQuantity').value;
    catalogSave.catalogChoiceTypeId = this.catalogForm.get('catalogChoiceTypeId').value;
    catalogSave.booksQuantity = this.catalogForm.get('booksQuantity').value;
    catalogSave.toysQuantity = this.catalogForm.get('toysQuantity').value;
    catalogSave.subscriptionQuantity = this.catalogForm.get('subscriptionQuantity').value;
    catalogSave.createdBy = 'tamere';
    catalogSave.updatedBy = 'tamere';
    if (this.formCatalog.catalogGoods) {
      catalogSave.catalogGoods = this.formCatalog.catalogGoods;
      catalogSave.catalogGoods.forEach(item => {
        item.dateMax = this.dateConverterService.dateToYear(item.dateMax);
        item.dateMin = this.dateConverterService.dateToYear(item.dateMin);
      })
    }
    console.log('catalogsave', catalogSave);
    return catalogSave;
  }

  onSubmit() {

    this.submitting = true;
    this.submitCatalogObj = this._getSubmitObj();
    if (!this.isEdit) {
      this.submitCatalogSub = this.catalogService.create(this.submitCatalogObj)
        .subscribe(data => this._handleSubmitSuccess(data),
          err => this._handleSubmitError(err));
    } else {
      this.submitCatalogSub = this.catalogService.update(this.submitCatalogObj)
        .subscribe(data => this._handleSubmitSuccess(data),
          err => this._handleSubmitError(err));
    }
  }

  private _handleSubmitSuccess(res) {
    console.log('res', res)
    this.error = false;
    this.submitting = false;
    const title = this.isEdit ? 'Catalogue Modifié' : 'Catalogue Ajouté'
    let body = 'Le Catalogue numero ' + res.id + ' a bien été ';
    body += this.isEdit ? 'modifié.' : 'ajouté.'
    this.toasterService.popAsync(this.notificationService.showSuccessToast(title, body));
    this.router.navigate(['/ces/ce-form/catalogue/', this.ce.id]);
  }

  private _handleSubmitError(err) {
    console.error(err);
    this.submitting = false;
    this.error = true;
    const title = 'une erreur est survenue';
    const body = err.body;
    this.toasterService.popAsync(this.notificationService.showErrorToast(title, body))
  }

  _resetForm() {
    this.catalogForm.reset();
  }

}
