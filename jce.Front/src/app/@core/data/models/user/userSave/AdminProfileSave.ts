import {FormUserProfileSaveModel, UserProfileSave} from "./UserProfileSave";
import {CE} from "../../ce/ce";
import {AddressSave} from "../../shared/address";

class AdminProfileSave extends UserProfileSave{

  public ces: CE[]

  // constructor(id: number, firstName: string, lastName: string, email: string, phone: string,
  //             CreatedBy: string, UpdatedBy: string, Address: AddressSave, Ces: CE[]) {
  //   super(id, firstName, lastName, email, phone, CreatedBy, UpdatedBy, Address);
  //   this.ces = Ces;
  // }
}

class FormAdminProfileSaveModel extends FormUserProfileSaveModel {
  public ces: CE[];


  // constructor(firstName: string, lastName: string, email: string, phone: string, CreatedBy: string,
  //             UpdatedBy: string, company: string, agency: string, service: string, streetNumber: number,
  //             address1: string, address2: string, postalCode: string, city: string, addressExtra: string, Ces: CE[]) {
  //   super(firstName, lastName, email, phone, CreatedBy, UpdatedBy, company, agency,
  //     service, streetNumber, address1, address2, postalCode, city, addressExtra);
  //   this.Ces = Ces;
  // }
}

export {AdminProfileSave, FormAdminProfileSaveModel}
