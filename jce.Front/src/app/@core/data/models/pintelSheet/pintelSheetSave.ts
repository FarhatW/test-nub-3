import {BaseEntity} from "../shared/baseEntity";

class PintelSheetSave extends BaseEntity {
  constructor(public id: number,
              public filePath: string,
              public season: string,
              public sheetId: string,
              public ageGroupId: number) {    super()

  }
}
export {PintelSheetSave}
