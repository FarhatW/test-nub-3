import {UserProfile} from "../userProfile";
import {FormUserProfileSaveModel, UserProfileSave} from "./UserProfileSave";
import {AddressSave} from "../../shared/address";

class PersonProfileSave extends UserProfileSave{
public ceId: number;


  // constructor(id: number, firstName: string, lastName: string, email: string, phone: string,
  //             CreatedBy: string, UpdatedBy: string, Address: AddressSave, ceId: number) {
  //   super(id, firstName, lastName, email, phone, CreatedBy, UpdatedBy, Address);
  //   this.ceId = ceId;
  // }
}

class FormPersonProfileSaveModel extends FormUserProfileSaveModel{
  public ceId: number;


  // constructor(firstName: string, lastName: string, email: string, phone: string, CreatedBy: string,
  //             UpdatedBy: string, company: string, agency: string, service: string,
  //             streetNumber: number, address1: string, address2: string, postalCode: string,
  //             city: string, addressExtra: string, ceId: number) {
  //
  //   super(firstName, lastName, email, phone, CreatedBy, UpdatedBy, company, agency, service,
  //     streetNumber, address1, address2, postalCode, city, addressExtra);
  //   this.ceId = ceId;
  // }
}
export {PersonProfileSave, FormPersonProfileSaveModel}
