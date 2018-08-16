import {Injectable} from '@angular/core';
import {SupplierService} from "../../../../@core/data/services/supplier.service";
import {GoodDepartmentService} from "../../../../@core/data/services/gooddepartment.service";
import {LetterService} from "../../../../@core/data/services/letter.service";
import {OriginService} from "../../../../@core/data/services/origin.service";
import {PintelSheetService} from "../../../../@core/data/services/pintelSheet.service";
import {ProductTypeService} from "../../../../@core/data/services/productType.service";
import {ProviderService} from "../../../../@core/data/services/provider.service";
import {Supplier} from "../../../../@core/data/models/supplier/supplier";
import {
  DIMENSIONS_REGEX,
  LETTERS_AND_SPACES_REGEX,
  NUMBERS_AND_COMMA_REGEX,
  NUMBERS_REGEX, PRICE_REGEX,
  REFPINTEL_REGEX
} from '../../../../@core/utils/formUtils';
// import {D} from "@angular/core/src/render3";

@Injectable()
export class ExcelImportService {

  formatNumberOnly = NUMBERS_REGEX;
  formatNumberAndCommaOnly = NUMBERS_AND_COMMA_REGEX;
  formatLettersAndSpaces = LETTERS_AND_SPACES_REGEX;
  formatDimensions = DIMENSIONS_REGEX;

  format4Digits = REFPINTEL_REGEX;
  formatPrice = PRICE_REGEX;

  constructor(
    private supplierService: SupplierService,
    private goodDepartmentService: GoodDepartmentService,
    private letterService: LetterService,
    private originService: OriginService,
    private pintelSheetService: PintelSheetService,
    private productTypeService: ProductTypeService,
  ) {}


  check4Digits(refPintel: string): boolean {

    return this.format4Digits.test(refPintel.replace(' ', ''));
  }

  checkNullOrEmpty(value: string): boolean {

    return !!value;
  }

  checkPrice(price: string): boolean {
    console.log('price', price);

    return this.formatPrice.test(price.replace(',', '.'));
  }

  // Product Excel Errors

  errors = {
    refPintel: {
      required: 'Ref Pintel requise.',
      format: 'Mauvais format de Ref Pintel.'
    },
    details: {
      required: 'Description est un champ requis.',
      format: 'Mauvais format de Description',
    },
    title: {
      required: 'Nom du produit requis.',
      format: 'Mauvais format de nom de produit'
    },
    price: {
      required: 'Prix requis.',
      format: 'Mauvais format de prix (ex: 44,99)'
    },
    index: {
      required: 'Index est un champ requis.',
      format: 'Mauvais format d\'Index'
    },
    productType: {
      required: 'Type de produit est un champ requis.',
      format: 'Mauvais format de type de produit'
    },
    goodDepartment: {
      required: 'L\'univers est un champ requis.',
      format: 'Mauvais format d\'univers'
    },
    supplier: {
      required: 'Fournisseur est un champ requis.',
      format: 'Mauvais format de Fournisseur'
    },
    season: {
      required: 'Saison est un champ requis.',
      format: 'Mauvais format de saison'
    },
    pintelSheet: {
      required: 'Fiche Pintel est un champ requis.',
      format: 'Mauvais format de fiche pintel'
    },
    isBasicProduct: {
      required: 'Champ requis.',
      format: 'Mauvais format'
    },
    isDisplayedOnJCE: {
      required: 'Champ requis.',
      format: 'Mauvais format'
    },
    isDiscountable: {
      required: 'Champ requis.',
      format: 'Mauvais format'
    },
    isEnabled: {
      required: 'Champ requis.',
      format: 'Mauvais format'
    },
    origin: {
      required: 'Provenance est un champ requis.',
      format: 'Mauvais format de provenance'
    },
    products: '',
  };

  // Product Import Value Converter

  indexValueConverter(index: string): string {
    return this.letterService.getCurrentLetter().find(
      x => x.name.toLowerCase() === index.toLowerCase()).id.toString();
  }

  productTypeValueConverter(productType: string): number {
    return this.productTypeService.getCurrentTypes().find(
      x => x.name === productType).id
  }

  goodDepartmentValueConverter(goodDepartment: string): number {
    return this.goodDepartmentService.getCurrentDepartments().find(
      x => x.name === goodDepartment).id;
  }

  supplierValueConverter(supplier: string): number {
    return this.supplierService.getCurrentSuppliers().find(
      x => x.supplierRef === supplier).id
  }

  pintelSheetValueConverter(pintelSheet: string) {
  }

  booleanValueConverter(val: string): boolean {
    if (val) {
      return val.toLowerCase() === 'o' || val.toLowerCase() === 'y';
    }
    return false;
  }

  ageValueConverter(age: number, type: string): number {
    return type === 'ans' || type === 'years' ? age * 12 : age;
  }

  vatRateValueConverter(vatRate: string): number {
    console.log('vatrate', vatRate.substring(0, vatRate.length - 1));
    return +vatRate.substring(0, vatRate.length - 1);
  }

}
