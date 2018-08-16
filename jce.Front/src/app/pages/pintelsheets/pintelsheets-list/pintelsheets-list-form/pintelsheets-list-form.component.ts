import {Component, EventEmitter, Input, OnChanges, OnInit, Output} from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ToasterService} from 'angular2-toaster';
import {Observable} from 'rxjs/Rx';
import {NotificationService} from '../../../../@core/data/services/notification.service';
import {FormPintelSheet, PintelSheet} from '../../../../@core/data/models/pintelSheet';
import {PintelSheetService} from '../../../../@core/data/services/pintelSheet.service';
import {PintelsheetFormService} from './shared/pintelsheet-form.service';
import {AgeGroupService} from '../../../../@core/data/services/ageGroup.service';
import {AgeGroupEnum} from '../../../../@core/data/models/Enums/ageGroup.enum';
import {ActivatedRoute, Router} from "@angular/router";
import {PintelSheetSave} from "../../../../@core/data/models/pintelSheet/pintelSheetSave";

@Component({
  selector: 'ngx-pintelsheets-list-form',
  templateUrl: './pintelsheets-list-form.component.html',
  styleUrls: ['./pintelsheets-list-form.component.scss']
})
export class PintelsheetsListFormComponent implements OnInit {

  selectedPintelSheet: PintelSheet;
  selectedPintelSheetId: number;
  selectPintelSheetSub: Subscription;
  isEdit: Boolean;
  @Output() closePintelSheet = new EventEmitter<PintelSheet>();
  @Output() savedPintelSheetId = new EventEmitter<PintelSheet>();
  pintelsheetForm: FormGroup;
  formPintelSheet: FormPintelSheet;
  formChangeSub: Subscription;
  pintelsheetFormErrors: any;
  pintelsheetFormErrorsBool: any;
  submitPintelSheetObj: PintelSheetSave;
  submitPintelSheetSub: Subscription;
  error: boolean;
  submitting: boolean;
  cardHeaderTitle: string = '';
  cardHeaderId: string = '';
  cardHeaderText: string;
  resetBtnText: string;
  submitBtnText: string;
  timerSubscription: Subscription;
  pintelSheetSub: Subscription;
  ageGroups: AgeGroupEnum[];

  constructor(private pintelSheetService: PintelSheetService,
              private notificationService: NotificationService,
              private toasterService: ToasterService,
              private fb: FormBuilder,
              private pintelsheetFormService: PintelsheetFormService,
              private ageGroupService: AgeGroupService,
              private route: ActivatedRoute,
              private router: Router) {
    this.route.params.subscribe(p => this.selectedPintelSheetId = +p['id'] || 0);
  }

  ngOnInit() {

    this.pintelSheetSub = this.route.params.subscribe(p => {
      this.selectedPintelSheetId = +p['id'] || 0;
      this.isEdit = !!this.selectedPintelSheetId;
      if (this.selectedPintelSheetId) {
        this.pintelSheetService.getById(this.selectedPintelSheetId)
          .subscribe(ps => {
            this.selectedPintelSheet = ps;
            this._setCfg();
          })
      } else {
        this._setCfg()
      }
    })
    this.ageGroupService.getAll().subscribe(results => {
      this.ageGroups = results
    });
  }

  _setCfg() {
    this.formPintelSheet = this._setSupplierForm();
    this.submitBtnText = (this.isEdit ?
      'Mettre à jour ' : 'Ajouter la ') + 'fiche de collectivités.'
    this.pintelsheetFormErrors = this.pintelsheetFormService.pintelsheetFormErrors;
    this.pintelsheetFormErrorsBool = this.pintelsheetFormService.pintelsheetFormErrorsBool;
    this.cardHeaderTitle = (this.isEdit ?
      'Modifier la ' : 'Ajout de la') + ' fiche de collectivités..'
    this.cardHeaderId = this._setIdHeader(this.formPintelSheet);
    this._buildForm();
  }

  _setIdHeader(formPintelSheet: FormPintelSheet) {
    return formPintelSheet.id ? '#' + formPintelSheet.id : 'NEW';
  }

  _compare(item1, item2): boolean {
    return item1 === item2;
  }

  private _setSupplierForm() {
    if (!this.isEdit) {
      return new FormPintelSheet(
        null,
        null,
        null,
        null,
        null,
      )
    } else {
      return new FormPintelSheet(
        this.selectedPintelSheet.id,
        this.selectedPintelSheet.filePath,
        this.selectedPintelSheet.season,
        this.selectedPintelSheet.sheetId,
        this.selectedPintelSheet.ageGroup.id,
      )
    }
  }

  private _buildForm() {
    this.pintelsheetForm = this.fb.group({
      filePath: [this.formPintelSheet.filePath, [
        Validators.required,
        Validators.minLength(3),
      ]],
      season: [this.formPintelSheet.season, [
        Validators.required,
        Validators.minLength(3),

      ]],
      sheetId: [this.formPintelSheet.sheetId, [
        Validators.required,
        Validators.minLength(3),

      ]],
      ageGroupId: [this.formPintelSheet.ageGroupId, [
        Validators.required,
      ]],
    });
    this.formChangeSub = this.pintelsheetForm
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
      _markDirty(this.pintelsheetForm);
    }
    this._onValueChanged();
  }

  private _onValueChanged() {
    if (!this.pintelsheetForm) {
      return;
    }
    const _setErrMsgs = (control: AbstractControl, errorsObj: any, field: string) => {
      if (control && control.dirty && control.invalid) {
        const messages = this.pintelsheetFormService.pintelsheetFormValidationMessages[field];
        for (const key in control.errors) {
          if (control.errors.hasOwnProperty(key)) {
            errorsObj[field] += messages[key] + '<br>';
          }
        }
      }
    };
    for (const field in this.pintelsheetFormErrors) {
      if (this.pintelsheetFormErrors.hasOwnProperty(field)) {
        // if (field !== 'datesGroup') {
        this.pintelsheetFormErrors[field] = '';
        _setErrMsgs(this.pintelsheetForm.get(field), this.pintelsheetFormErrors, field);
      }
    }
  }

  _blurError(formError, event) {
    this.pintelsheetFormErrorsBool[event.srcElement.id] = !!formError;
  }

  private _getSubmitObj() {
    return new PintelSheetSave(
      (this.isEdit ? this.selectedPintelSheetId : 0),
      this.pintelsheetForm.get('filePath').value,
      this.pintelsheetForm.get('season').value,
      this.pintelsheetForm.get('sheetId').value,
      this.pintelsheetForm.get('ageGroupId').value,
    );
  }

  onSubmit() {

    this.submitting = true;
    this.submitPintelSheetObj = this._getSubmitObj();
    console.log('submitPintelSheetObj', this.submitPintelSheetObj);
    this.submitPintelSheetObj.createdBy = 'mathieu';
    const $result = this.isEdit ? this.pintelSheetService.update(this.submitPintelSheetObj) :
      this.pintelSheetService.create(this.submitPintelSheetObj)
    $result.subscribe(
      data => this._handleSubmitSuccess(data),
      err => this._handleSubmitError(err));
  }

  private _handleSubmitSuccess(res) {
    console.log('res', res);
    this.error = false;
    this.submitting = false;
    const title = 'Fiche de Collectivité' + (res.id === 0 ? 'créée' : 'modifiée');
    const body = 'La Fiche de Collectivité ' + res.refPintel + ' a bien été ' + (res.id === 0 ? 'créée' : 'modifiée');
    this.toasterService.popAsync(this.notificationService.showSuccessToast(title, body));
    this.savedPintelSheetId.emit(res);
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
    this.router.navigate(['/pintelsheets/list/']);
  }
}
