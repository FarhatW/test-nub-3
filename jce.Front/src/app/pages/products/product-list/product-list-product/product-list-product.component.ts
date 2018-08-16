import {
  Component, DoCheck, EventEmitter, forwardRef, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChange,
  SimpleChanges, ViewChild
} from '@angular/core';
import {FormGoodModel, Good} from '../../../../@core/data/models/products/Good';
import {Product} from '../../../../@core/data/models/products/product';
import {LetterService} from '../../../../@core/data/services/letter.service';
import {LetterEnum} from '../../../../@core/data/models/Enums/letter.enum';
import {GoodDepartmentService} from '../../../../@core/data/services/gooddepartment.service';
import {KeyValuePairEnum} from '../../../../@core/data/models/Enums/keyValuePair.enum';
import {OriginService} from '../../../../@core/data/services/origin.service';
import {ProductTypeService} from '../../../../@core/data/services/productType.service';
import {Supplier} from '../../../../@core/data/models/supplier/supplier';
import {SupplierService} from '../../../../@core/data/services/supplier.service';
import {GoodService} from '../../../../@core/data/services/good.service';
import {ToasterService} from 'angular2-toaster';
import {NotificationService} from '../../../../@core/data/services/notification.service';
import {AbstractControl, Form, FormArray, FormBuilder, FormGroup, NG_VALUE_ACCESSOR, Validators} from '@angular/forms';
import {Subscription} from 'rxjs/Subscription';
import {ProductFormService} from './shared/product-form.service';
import {GoodSave} from '../../../../@core/data/models/products/goodSave';
import {REFPINTEL_REGEX, PRICE_REGEX} from '../../../../@core/utils/formUtils';
import {FilesService} from '../../../../@core/data/services/files.service';
import {ActivatedRoute, Router} from '@angular/router';
import {
  AutocompleteItem,
  CreateNewAutocompleteGroup,
  NgAutocompleteComponent,
  SelectedAutocompleteItem
} from 'ng-auto-complete';

@Component({
  selector: 'ngx-product-list-product',
  templateUrl: './product-list-product.component.html',
  styleUrls: ['./product-list-product.component.scss'],
})

export class ProductListProductComponent implements OnInit, OnDestroy {
  @ViewChild('autocompleter') public autoCompleter: NgAutocompleteComponent;

  visibility: string = 'hidden';
  goodImage: any;
  selectedGood: Good;
  selectGoodSub: Subscription;
  isEdit: Boolean;
  suppliers: Supplier[] = [];
  isNewBatch: boolean;
  good: Good;
  products: Product[];
  letters: LetterEnum[];
  toyDepartments: KeyValuePairEnum[];
  origins: KeyValuePairEnum[];
  toyTypes: KeyValuePairEnum[];
  uploadStatus = new EventEmitter();

  public group = [
    CreateNewAutocompleteGroup(
      'Ajouter un Fournisseur',
      'completer',
      [],
      {titleKey: 'title', childrenKey: null}
    ),
  ];

  goodSub: Subscription;
  goodForm: FormGroup;
  formGood: FormGoodModel;
  formChangeSub: Subscription;
  goodFormErrors: any;
  goodFormErrorsBool: any;
  submitGoodObj: GoodSave;
  submitGoodSub: Subscription;
  error: boolean;
  submitting: boolean;
  cardHeaderTitle: string = '';
  cardHeaderId: string = '';
  cardHeaderText: string;
  resetBtnText: string;
  submitBtnText: string;
  product;
  timerSubscription: Subscription;
  id: number;
  supplierGroupItem: any[] = [];
  subTest: Subscription;

  query: any = {
    pageSize: 1000,
    isSortAscending: true,
    isForDropDown: true,
  };

  constructor(private letterService: LetterService,
              private goodDepartmentService: GoodDepartmentService,
              private originService: OriginService,
              private productTypeService: ProductTypeService,
              private supplierService: SupplierService,
              private goodService: GoodService,
              private toasterService: ToasterService,
              private notificationService: NotificationService,
              private fb: FormBuilder,
              private productFormService: ProductFormService,
              private route: ActivatedRoute,
              private router: Router,
              private filesService: FilesService) {
    this.route.params.subscribe(p => this.id = +p['id'] || 0);
  }

  private refreshData(): void {

    this.selectGoodSub = this.goodService.getById(this.id).subscribe(results => {
      this.selectedGood = results;
      this._setCfg();
    });
  }

  ngOnInit() {

    this.letters = this.letterService.getCurrentLetter();

    this.letterService.currentLetters.subscribe(letters => this.letters = letters);
    this.goodDepartmentService.currentDepartments.subscribe(departments => this.toyDepartments = departments);
    this.originService.currentOrigins.subscribe(origins => this.origins = origins);
    this.productTypeService.currentTypes.subscribe(types => this.toyTypes = types);

    this.goodSub = this.route.params.subscribe(p => {
      this.id = +p['id'] || 0;
      this.isEdit = !!this.id;
      if (this.id) {
        this.goodService.getById(this.id).subscribe(good => {
            this.selectedGood = good;
            this.getImageFromService(this.selectedGood.refPintel);
            this._setCfg();
          }
        )
      } else {
        this.route.data.subscribe(d => this.isNewBatch = d.isBatch);
        this._setCfg();
      }
    })
    this.supplierService.getAll(this.query).subscribe(sup => {
      this.suppliers = sup.items;
      const suppliersGroup: any[] = [];
      this.suppliers.forEach(item => {
        const supplierGroupItem = new AutocompleteItem(item.name, item.id, item.description);
        this.supplierGroupItem.push(supplierGroupItem);
      })
      this.autoCompleter.SetValues('completer', this.supplierGroupItem);
      if (this.isEdit) {
        this.autoCompleter.SelectItem('completer', this.selectedGood.supplierId);
      }
    })
  }

  _setCfg() {
    this.formGood = this._setGoodForm();
    this.submitBtnText = this.isEdit ?
      'Mettre à jour' : 'Ajouter le ' + (this.formGood.isBatch ? 'lot' : 'produit')
    this.goodFormErrors = this.productFormService.goodFormErrors;
    this.goodFormErrorsBool = this.productFormService.goodFormErrorsBool;
    this.cardHeaderTitle = this._setTitle(this.formGood);
    this.cardHeaderId = this._setIdHeader(this.formGood);
    this._buildForm();
  }

  _setTitle(formGood: FormGoodModel): string {

    let headerText: string = formGood.id ? ' Modifier le ' : ' Ajouter un ';
    headerText += this.formGood.isBatch ? 'lot.' : 'produit.'
    return headerText;
  }

  _setIdHeader(formGood: FormGoodModel) {
    return formGood.id ? '#' + formGood.id : 'NEW '
  }

  _compare(item1, item2): boolean {
    return item1 === item2;
  }

  private _setGoodForm() {
    if (!this.isEdit) {
      return new FormGoodModel(null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        this.isNewBatch ? 1 : null,
        null,
        null,
        null,
        this.isNewBatch,
        null,
        false,
        true,
        this.isNewBatch ? 1 : null,
        this.isNewBatch ? [] : null,
        null,
        null,
        null,
        null,
      )
    } else return new FormGoodModel(
      this.selectedGood.id,
      this.selectedGood.refPintel,
      this.selectedGood.details,
      this.selectedGood.title,
      this.selectedGood.price,
      this.selectedGood.indexId,
      this.selectedGood.productTypeId,
      this.selectedGood.goodDepartmentId,
      this.selectedGood.isBatch ? 1 : this.selectedGood.supplierId,
      this.selectedGood.season,
      this.selectedGood.pintelSheetId,
      this.selectedGood.isBasicProduct,
      this.selectedGood.isBatch,
      this.selectedGood.isDisplayedOnJCE,
      this.selectedGood.isDiscountable,
      this.selectedGood.isEnabled,
      this.selectedGood.isBatch ? 1 : this.selectedGood.originId,
      this.selectedGood.products,
      this.selectedGood.createdOn,
      this.selectedGood.createdBy,
      this.selectedGood.updatedOn,
      this.selectedGood.updatedBy,
    )
  }

  private _buildForm() {
    this.goodForm = this.fb.group({
      refPintel: [this.formGood.refPintel, [
        Validators.required,
        Validators.pattern(REFPINTEL_REGEX)
      ]],
      details: [this.formGood.details, [
        Validators.required
      ]],
      title: [this.formGood.title, [
        Validators.required
      ]],
      price: [this.formGood.price, [
        Validators.required,
        Validators.min(0.1),
        Validators.pattern(PRICE_REGEX)
      ]],
      indexId: [this.formGood.indexId, [
        Validators.required,
        Validators.nullValidator
      ]],
      productTypeId: [this.formGood.productTypeId, [
        Validators.required,
        Validators.nullValidator

      ]],

      goodDepartmentId: [this.formGood.goodDepartmentId, [
        Validators.required,
        Validators.nullValidator

      ]],
      // file: [this.formGood.file],
      // supplierId: [this.formGood.supplierId, [
      //   Validators.required,
      //   Validators.nullValidator
      //
      // ]],
      supplierId: [this.formGood.supplierId, [
        Validators.required,
        Validators.nullValidator

      ]],
      season: [this.formGood.season, [
        Validators.pattern(REFPINTEL_REGEX)
      ]],
      originId: [this.formGood.originId, [
        Validators.required,
      ]],

      pintelSheetId: [this.formGood.pintelSheetId, []],
      isBasicProduct: [this.formGood.isBasicProduct, []],
      isBatch: [this.formGood.isBatch, []],
      isDisplayedOnJCE: [this.formGood.isDisplayedOnJCE, []],
      isDiscountable: [this.formGood.isDiscountable, []],
      isEnabled: [this.formGood.isEnabled, []],
      products: [this.formGood.products, []],
    });
    this.formChangeSub = this.goodForm
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
      _markDirty(this.goodForm);
    }
    this._onValueChanged();
  }

  private _onValueChanged() {
    if (!this.goodForm) {
      return;
    }
    const _setErrMsgs = (control: AbstractControl, errorsObj: any, field: string) => {
      if (control && control.dirty && control.invalid) {
        const messages = this.productFormService.goodFormValidationMessages[field];
        for (const key in control.errors) {
          if (control.errors.hasOwnProperty(key)) {
            errorsObj[field] += messages[key] + '<br>';
          }
        }
      }
    };
    for (const field in this.goodFormErrors) {
      if (this.goodFormErrors.hasOwnProperty(field)) {
        // if (field !== 'datesGroup') {
        this.goodFormErrors[field] = '';
        _setErrMsgs(this.goodForm.get(field), this.goodFormErrors, field);
      }
    }
  }

  _blurError(formError, event) {
    this.goodFormErrorsBool[event.srcElement.id] = !!formError;
  }

  private _getSubmitObj() {
    console.log('thisedit', this.isEdit);
    const good: GoodSave = new GoodSave(
    );
    if (this.isEdit) {
      good.id = this.formGood.id
    }
    good.refPintel = this.goodForm.get('refPintel').value;
    good.details = this.goodForm.get('details').value;
    good.title = this.goodForm.get('title').value;
    good.price = this.goodForm.get('price').value;
    good.indexId = this.goodForm.get('indexId').value;
    good.productTypeId = this.goodForm.get('productTypeId').value;
    good.goodDepartmentId = this.goodForm.get('goodDepartmentId').value;
    good.isDiscountable = this.goodForm.get('isDiscountable').value;
    good.supplierId = this.goodForm.get('supplierId').value;
    good.season = this.goodForm.get('season').value;
    good.pintelSheetId = this.goodForm.get('pintelSheetId').value;
    good.isBasicProduct = this.goodForm.get('isBasicProduct').value;
    good.isBatch = this.goodForm.get('isBatch').value;
    good.isDisplayedOnJCE = this.goodForm.get('isDisplayedOnJCE').value;
    good.originId = this.goodForm.get('originId').value;
    good.isEnabled = this.goodForm.get('isEnabled').value;
    good.products = this.goodForm.get('products').value;
    good.createdBy = 'tamere';
    good.updatedBy = 'taemre';

    return good;
  }

  onSubmit() {
    console.log('goodformsubmit', this.goodForm);

    this.submitting = true;
    this.submitGoodObj = this._getSubmitObj();

    console.log('submitGoodObj', this.submitGoodObj)

    const $result = this.isEdit ? this.goodService.update(this.submitGoodObj) :
      this.goodService.create(this.submitGoodObj)

    $result.subscribe(
      data => this._handleSubmitSuccess(data),
      err => this._handleSubmitError(err));
  }

  private _handleSubmitSuccess(res) {
    this.selectedGood = res;
    this.error = false;
    this.submitting = false;
    const title = (res.isBatch ? 'Lot ' : 'Produit ') + (res.id === 0 ? 'créé' : 'modifié');
    const body = 'L article ref ' + res.refPintel + ' a bien été ' + (!this.isEdit ? 'créé' : 'modifié');
    this.toasterService.popAsync(this.notificationService.showSuccessToast(title, body));
    this.router.navigate(['/products/product-list/product/' + res.id]);

  }

  private _handleSubmitError(err) {
    this.submitting = false;
    this.error = true;
    const title = 'une erreur est survenue';
    const body = err.body;
    this.toasterService.popAsync(this.notificationService.showErrorToast(title, body))
  }

  goodSupplier(product: Product): string {
    return this.suppliers.find(x => x.id === product.supplierId).name;
  }

  deleteProd(batch: GoodSave, prod: Good) {
    const index: number = batch.products.indexOf(prod);
    if (index > -1) {
      batch.products.splice(index, 1);
      if (batch.id) {
        batch.isBatch = true;
        this.goodService.update(batch)
          .subscribe(res => {
              console.log(res);
            },
            err => {
              const title = 'une erreur est survenue';
              const body = err.body;
              this.toasterService
                .popAsync(this.notificationService.showErrorToast(title, body));
            },
            () => {
              const title = 'Lot modifié';
              const body = 'Le produit a bien été retiré du lot';
              this.toasterService
                .popAsync(this.notificationService.showSuccessToast(title, body));
            },
          );
      }
    }
  }

  addProd2Batch(batch: Good, prod: Good) {
    if (batch.products && !batch.products.find(x => x.refPintel === prod.refPintel)) {
      batch.products.push(prod);
      if (batch.id) {
        this.goodService.update(batch)
          .subscribe(res => {
              console.log(res);
            },
            err => {
              console.log('errer', err);
              const title = 'une erreur est survenue';
              const body = err.body;
              this.toasterService
                .popAsync(this.notificationService.showErrorToast(title, body));
            },
            () => {
              const title = 'Lot modifié';
              const body = 'Le produit ref ' + prod.refPintel + ' a bien été ajouté au lot';
              this.toasterService
                .popAsync(this.notificationService.showSuccessToast(title, body));
            }
          );
      }
    } else {
      const title = 'Produit déjà présent';
      const body = 'Le produit ref ' + prod.refPintel + ' est déjà présent dans ce lot';
      this.toasterService
        .popAsync(this.notificationService.showWarningToast(title, body))
    }
  }

  getImageFromService(refPintel: string) {

    // this.filesService.getImageJPG(refPintel)
    //   .subscribe(data => {
    //     console.log(data);
    //     this.createImageFromBlob(data);
    //   }, err => {
    //     this.handleImageError(refPintel);
    //   })
  }

  createImageFromBlob(image: Blob) {
    // let reader = new FileReader();
    // reader.addEventListener('load', () => {
    //   this.goodImage = reader.result;
    // }, false);
    //
    // if (image) {
    //   reader.readAsDataURL(image);
    // }
  }

  handleImageError(refpintel) {
    this.filesService.getImagePNG(refpintel)
      .subscribe(data => {
        this.createImageFromBlob(data);
      }, err => {
        this.goodImage = 'assets/images/img-placeholder.png';
      })
  }

  ngOnDestroy(): void {
    if (this.selectGoodSub) {
      this.selectGoodSub.unsubscribe();
    }
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe()
    }
  }

  onClose() {
    this.router.navigate(['/products/product-list/']);
  }

  Selected(item: SelectedAutocompleteItem) {
    console.log('item', item.item);
    if (this.selectedGood.isBatch){
      this.goodForm.controls['supplierId'].setValue(1);
      this.autoCompleter.SelectItem('completer', 1);
    }

    if (item.item) {
      console.log('la');
      // this.goodForm.controls['supplierId'] = this.fb.array(
      //   [this.goodForm.controls['supplierId'].value, item.item.id]);
      console.log('item', item.item.id);

      this.goodForm.controls['supplierId'].setValue(item.item.id);
    } else {
      console.log('ou  la');

      this.goodForm.controls['supplierId'].setValue(1);
      this.autoCompleter.SelectItem('completer', 1);
    }
  }
}
