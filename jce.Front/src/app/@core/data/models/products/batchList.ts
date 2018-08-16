import {Good} from "./Good";
import {KeyValuePairEnum} from "../Enums/keyValuePair.enum";

export class BatchList {
  batches: Good[];
  addedBatchCount: number;
  notAddedBatchCount: number;
  duplicatedRefList: string[];
  nonExistentProducts: KeyValuePairEnum[];
}
