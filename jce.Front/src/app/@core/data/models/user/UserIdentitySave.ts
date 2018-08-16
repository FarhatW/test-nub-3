

class UserIdentitySave {

  constructor(
    public id: number,
    public userName: string,
    public email: string,
    public password: string,
    public confirmPassword: string,
    public createdBy: string,
    public updatedBy: string,
    public roles: number[] = [] ){
  }
}



class FormUserIdentitySaveModel {
  constructor(
    public userName: string,
    public email: string,
    public CreatedBy: string,
    public UpdatedBy: string,
    public roles: number[]= []) {
  }
}
class FormUserPasswordSaveModel {
  constructor(
    public password: string,
    public confirmPassword: string) {
  }
}


export {UserIdentitySave, FormUserPasswordSaveModel, FormUserIdentitySaveModel}
