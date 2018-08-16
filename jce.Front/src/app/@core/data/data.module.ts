import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserService } from './users.service';
import { ElectricityService } from './electricity.service';
import { StateService } from './state.service';
import { SmartTableService } from './smart-table.service';
import { PlayerService } from './player.service';
import {CeService} from './services/ce.service';
import {CeSetupService} from './services/ceSetup.service';
import {CatalogService} from './services/catalog.service';
import {ProductService} from './services/product.service';
import {GoodService} from './services/good.service';
import {SearchService} from './services/search.service';
import {AgeGroupService} from './services/ageGroup.service';
import {PintelSheetService} from './services/pintelSheet.service';
import {LetterService} from './services/letter.service';
import {GoodDepartmentService} from './services/gooddepartment.service';
import {OriginService} from './services/origin.service';
import {ProductTypeService} from './services/productType.service';
import {SupplierService} from './services/supplier.service';
import {NotificationService} from './services/notification.service';
import {CatalogTypeService} from './services/catalog-type.service';
import {CatalogChoiceTypeService} from './services/catalog-choice-type.service';
import {UsersService} from './services/users.service';
import {GmapsService} from './services/gmaps.service';
import {FilesService} from './services/files.service';
import {HttpClientModule} from '@angular/common/http';
import {TitleService} from '../shared/title.service';

const SERVICES = [
  UserService,
  UsersService,
  ElectricityService,
  StateService,
  SmartTableService,
  PlayerService,
  CeService,
  CeSetupService,
  CatalogService,
  ProductService,
  SearchService,
  GoodService,
  AgeGroupService,
  PintelSheetService,
  LetterService,
  GoodDepartmentService,
  OriginService,
  ProductTypeService,
  SupplierService,
  NotificationService,
  CatalogTypeService,
  CatalogChoiceTypeService,
  GmapsService,
  FilesService,
  TitleService
];

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    ...SERVICES,
  ],
  declarations: [
    // FilterPipe,
  ],
})
export class DataModule {
  static forRoot(): ModuleWithProviders {
    return <ModuleWithProviders>{
      ngModule: DataModule,
      providers: [
        ...SERVICES,
      ],
    };
  }
}
