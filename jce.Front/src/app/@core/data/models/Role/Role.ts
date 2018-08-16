import {BaseEntity} from "../shared/baseEntity";

export class Role extends BaseEntity {
   id: number;
   name: string;
   enable: boolean = true;
   rank: number;

}
