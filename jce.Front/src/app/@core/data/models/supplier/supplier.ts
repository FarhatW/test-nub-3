import {Good} from '../products/Good';
import {BaseEntity} from '../shared/baseEntity';

class Supplier extends BaseEntity {
  constructor(
    public id: number,
    public  name: string,
    public  description: string,
    public isEnabled: boolean,
    public supplierRef: string,
    public productCount: number,
    public goods: Good[]) {super(); }
}
class FormSupplierModel extends BaseEntity {
  constructor(
    public id: number,
    public  name: string,
    public  description: string,
    public isEnabled: boolean,
    public supplierRef: string,
    public goods: Good[]) {super(); }
}

export {Supplier, FormSupplierModel};
