import { Address } from './Address';
import {Role} from './role';


export class User {
    id: number;

    firstName: string;

    lastName: string;

    email: string;

    password: string;

    phone: string;

    loginName: string;

    idCe: number;

    createdBy:  number;

    createdOn: string;

    updatedBy: number;

    updatedOn: string;

    commercialUniqueCode: string;
    redirectToLocal: boolean;
    address: Address;
    token: string;
    roles: Array<Role>;
   returnUrl: string;
}
