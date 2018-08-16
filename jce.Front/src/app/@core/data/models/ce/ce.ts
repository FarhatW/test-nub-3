import {Address} from '../shared/address';
import {Catalog} from '../catalog/catalog';
import {CeSetup} from './ceSetup';
import {BaseEntity} from '../shared/baseEntity';

class CE extends BaseEntity {

  constructor(public id: number,
              public name: string,
              public fax: string,
              public telephone: string,
              public logo: string,
              public actif: boolean,
              public isDeleted: boolean,
              public createdBy: string,
              public updatedBy: string,
              public adminJceProfileId: number,
              public address: Address,
              // public baseEntity: BaseEntity,
              public catalog: Catalog,
              public ceSetup: CeSetup,
  ) {super()
  }
}

class FormCEModel {
  constructor(public name: string,
              public fax: string,
              public telephone: string,
              public logo: string,
              public actif: boolean,
              public isDeleted: boolean,
              public createdBy: string,
              public updatedBy: string,
              public company: string,
              public agency: string,
              public service: string,
              public streetNumber: number,
              public address1: string,
              public address2: string,
              public postalCode: string,
              public city: string,
              public addressExtra: string,
              public adminJceProfileId: number) {
  }
}

export {CE, FormCEModel};
