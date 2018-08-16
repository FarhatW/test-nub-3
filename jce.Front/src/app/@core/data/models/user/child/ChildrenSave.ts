class ChildrenSave {

  constructor(
    public id: number,

    public firstName: string,
    public lastName: string,
    public birthDate: string,
    public  gender: number,
    public  isEnabled: boolean,
    public  amountParticipationCe: number,
    public  personJceProfileId: number,
    public  createdBy: number,
    public  updatedBy: number,
    public  createdOn: string,
    public  updatedOn: string,
  ) {
  }
}
  class FormChildrenSave {

  constructor(
    public  firstName: string,
    public  lastName: string,
    public  birthDate: string,
    public  gender: number,
    public  isEnabled: boolean,
    public  amountParticipationCe: number,
    public  personJceProfileId: number,
    public  createdBy: number,
    public  updatedBy: number,
    public  createdOn: string,
    public  updatedOn: string,
    public isExpectBirth: boolean
  ) {}
}

export {ChildrenSave, FormChildrenSave}
