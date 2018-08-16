
import {Address, AddressSave} from '../../shared/address';


class UserProfileSave {
  constructor(
    public id: number,
    public firstName: string,
    public lastName: string,
    public isEnabled: boolean,
    public email: string,
    public phone: string,
    public createdBy: string,
    public updatedBy: string,
    public address: AddressSave,
    public ceId: number
    ) {
  }
}

class FormUserProfileSaveModel {
  constructor(
    public firstName: string,
    public lastName: string,
    public email: string,
    public  phone: string,
    public CreatedBy: string,
    public UpdatedBy: string,
    public company: string,
    public agency: string,
    public service: string,
    public streetNumber: number,
    public address1: string,
    public address2: string,
    public postalCode: string,
    public city: string,
    public addressExtra: string,
    public ceId: number) {
  }
}

export {UserProfileSave, FormUserProfileSaveModel}

