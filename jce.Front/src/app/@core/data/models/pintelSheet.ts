import {AgeGroupEnum} from './Enums/ageGroup.enum';
import {BaseEntity} from "./shared/baseEntity";


class PintelSheet extends BaseEntity {
  constructor(public id: number,
              public filePath: string,
              public season: string,
              public sheetId: string,
              public ageGroup: AgeGroupEnum,
              public productCount: number) {
    super()
  }
}

class FormPintelSheet {
  constructor(public id: number,
              public filePath: string,
              public season: string,
              public sheetId: string,
              public ageGroupId: number) {
  }
}

export { PintelSheet, FormPintelSheet}
