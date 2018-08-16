import {Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output} from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import {SupplierService} from '../../../../@core/data/services/supplier.service';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ToasterService} from 'angular2-toaster';
import {Supplier} from '../../../../@core/data/models/supplier/supplier';
import {Observable} from 'rxjs/Rx';
import {NotificationService} from '../../../../@core/data/services/notification.service';
import {SupplierFormService} from './shared/supplier-form.service';
import {ActivatedRoute, Router} from '@angular/router';
import {Title} from '@angular/platform-browser';
import {SuppliersListComponent} from '../suppliers-list.component';

@Component({
  selector: 'ngx-suppliers-list-form',
  providers: [SuppliersListComponent],
  templateUrl: './suppliers-list-form.component.html',
  styleUrls: ['./suppliers-list-form.component.scss']
})
export class SuppliersListFormComponent implements OnInit {


  selectedSupplier: Supplier;
  selectedSupplierId: number;
  selectSupplierSub: Subscription;
  isEdit: Boolean;
  supplier: Supplier;
  supplierForm: FormGroup;
  formSupplier: Supplier;
  formChangeSub: Subscription;
  supplierFormErrors: any;
  supplierFormErrorsBool: any;
  submitSupplierObj: Supplier;
  submitSupplierSub: Subscription;
  error: boolean;
  submitting: boolean;
  cardHeaderTitle: string = '';
  cardHeaderId: string = '';
  cardHeaderText: string;
  resetBtnText: string;
  submitBtnText: string;
  timerSubscription: Subscription;
  supplierSub: Subscription;

  constructor(private supplierService: SupplierService,
              private notificationService: NotificationService,
              private titleService: Title,
              private toasterService: ToasterService,
              private router: Router,
              private fb: FormBuilder,
              private supplierFormService: SupplierFormService,
              private suppListComponent: SuppliersListComponent,
              private route: ActivatedRoute) {
    this.route.params.subscribe(p => this.selectedSupplierId = +p['id'] || 0);
    this.route.data.subscribe(t => this.titleService.setTitle(t.title));
  }

  private refreshData(): void {

    this.selectSupplierSub = this.supplierService.getById(this.selectedSupplierId).subscribe(results => {
      this.selectedSupplier = results;
      this._setCfg();
    });

  }

  private subscribeToData(): void {
    this.timerSubscription = Observable.timer(5000).first().subscribe(() => this.refreshData());
  }

  ngOnInit() {
    this.supplierSub = this.route.params.subscribe(p => {
      this.selectedSupplierId = +p['id'] || 0;
      this.isEdit = !!this.selectedSupplierId;
      if (this.selectedSupplierId) {
        this.supplierService.getById(this.selectedSupplierId).subscribe(
          supplier => {
            this.selectedSupplier = supplier;
            this._setCfg();
          })
      } else {
        this._setCfg();
      }
    })
  }

  _setCfg() {
    this.formSupplier = this._setSupplierForm();
    this.submitBtnText = (this.isEdit ?
      'Mettre à jour ' : 'Ajouter le ') + 'fournisseur.'
    this.supplierFormErrors = this.supplierFormService.supplierFormErrors;
    this.supplierFormErrorsBool = this.supplierFormService.supplierFormErrorsBool;
    this.cardHeaderTitle = (this.isEdit ?
      'Modifier le ' : 'Ajout d un') + ' fournisseur.'
    this.cardHeaderId = this._setIdHeader(this.formSupplier);
    this._buildForm();
  }

  _setIdHeader(formSupplier: Supplier) {
    return formSupplier.id ? '#' + formSupplier.id : 'NEW'
  }

  private _setSupplierForm() {
    if (!this.isEdit) {
      return new Supplier(
        null,
        null,
        null,
        true,
        null,
        null,
        [],
      )
    } else {
      return new Supplier(
        this.selectedSupplier.id,
        this.selectedSupplier.name,
        this.selectedSupplier.description,
        this.selectedSupplier.isEnabled,
        this.selectedSupplier.supplierRef,
        null,
        this.selectedSupplier.goods,
      )
    }
  }

  private _buildForm() {
    this.supplierForm = this.fb.group({
      name: [this.formSupplier.name, [
        Validators.required,
        Validators.minLength(3),

      ]],
      description: [this.formSupplier.description, [
        Validators.required,
        Validators.minLength(3),

      ]],
      isEnabled: [this.formSupplier.isEnabled, []],
      supplierRef: [this.formSupplier.supplierRef, [
        Validators.required,
        Validators.minLength(3),

      ]],
      goods: [this.formSupplier.goods, []],
    });
    this.formChangeSub = this.supplierForm
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
      _markDirty(this.supplierForm);
    }
    this._onValueChanged();
  }

  private _onValueChanged() {
    if (!this.supplierForm) {
      return;
    }
    const _setErrMsgs = (control: AbstractControl, errorsObj: any, field: string) => {
      if (control && control.dirty && control.invalid) {
        const messages = this.supplierFormService.supplierFormValidationMessages[field];
        for (const key in control.errors) {
          if (control.errors.hasOwnProperty(key)) {
            errorsObj[field] += messages[key] + '<br>';
          }
        }
      }
    };
    for (const field in this.supplierFormErrors) {
      if (this.supplierFormErrors.hasOwnProperty(field)) {
        // if (field !== 'datesGroup') {
        this.supplierFormErrors[field] = '';
        _setErrMsgs(this.supplierForm.get(field), this.supplierFormErrors, field);
      }
    }
  }

  _blurError(formError, event) {
    this.supplierFormErrorsBool[event.srcElement.id] = !!formError;
  }

  private _getSubmitObj() {
    return new Supplier(
      this.isEdit ? this.formSupplier.id : 0,
      this.supplierForm.get('name').value,
      this.supplierForm.get('description').value,
      this.supplierForm.get('isEnabled').value,
      this.supplierForm.get('supplierRef').value,
      this.supplierForm.get('goods').value,
      null
    )
  }

  onSubmit() {

    this.submitting = true;
    this.submitSupplierObj = this._getSubmitObj();
    this.submitSupplierObj.createdBy = 'mathieu';
    const $result = this.isEdit ? this.supplierService.update(this.submitSupplierObj) :
      this.supplierService.create(this.submitSupplierObj)
    $result.subscribe(
      data => this._handleSubmitSuccess(data),
      err => this._handleSubmitError(err));
  }

  private _handleSubmitSuccess(res) {
    this.error = false;
    this.submitting = false;
    const title = 'Fournisseur ' + (res.id === 0 ? 'créé' : 'modifié');
    const body = 'Le fournisseur ' + res.refPintel + ' a bien été ' + (res.id === 0 ? 'créé' : 'modifié');
    this.toasterService.popAsync(this.notificationService.showSuccessToast(title, body));
    this.router.navigate(['/suppliers/list/' + res.id]);
  }

  private _handleSubmitError(err) {
    console.error(err);
    this.submitting = false;
    this.error = true;
    const title = 'une erreur est survenue';
    const body = err.body;
    this.toasterService.popAsync(this.notificationService.showErrorToast(title, body))
  }

  onClose() {
    this.router.navigate(['/suppliers/list/']);
  }
}
