import {Address} from '../shared/address';
import {Catalog} from '../catalog/catalog';
import {CeSetup} from './ceSetup';
import {BaseEntity} from "../shared/baseEntity";

export class CeSave {

  // constructor(public id: number,
  //             public name: string,
  //             public fax: string,
  //             public telephone: string,
  //             public logo: string,
  //             public actif: boolean,
  //             public isDeleted: boolean,
  //             public createdBy: string,
  //             public updatedBy: string,
  //             public address: Address) {
  // }
  //


  id: number;
  name: string;
  fax: string;
  telephone: string;
  logo: string;
  actif: boolean;
  isDeleted: boolean;
  CreatedBy: string;
  UpdatedBy: string;
  address: Address;
  adminJceProfileId: number;
  // catalog: Catalog;
  //  ceSetup: CeSetup;
  //
  // catalog: Catalog;
  // ceSetup: CeSetup;
}
