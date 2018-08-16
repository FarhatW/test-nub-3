import {BaseEntity} from '../shared/baseEntity';

export class CeSetup {
  id: number;
  ceId: number;
  isHomeDelivery: boolean;
  isOrderConfirmationMail: boolean;
  isOrderConfirmationMail4Admin: boolean;
  isAgeGroupChoiceLimitation: boolean;
  isCeParticipation: boolean;
  isEmployeeParticipation: boolean;
  isGroupingAllowed: boolean;
  welcomeMessage: string;
  createdBy: string;
  updatedBy: string;
  // isNoChildrenOrderAllowed: boolean;
}
