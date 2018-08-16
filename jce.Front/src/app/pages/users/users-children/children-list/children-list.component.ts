import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {CesListSearchComponent} from "../../../ces/ces-list/ces-list-search/ces-list-search.component";
import {DatatableComponent} from "@swimlane/ngx-datatable";
import {Page} from "../../../../@core/data/models/shared/page";
import {Children} from "../../../../@core/data/models/user/child/Children";
import {ChildrenSave} from "../../../../@core/data/models/user/child/ChildrenSave";
import {NotificationService} from "../../../../@core/data/services/notification.service";
import {Router} from "@angular/router";
import {ToasterService} from "angular2-toaster";
import {ChildrenService} from "../../../../@core/data/services/children.service";


@Component({
  selector: 'ngx-children-list',
  templateUrl: './children-list.component.html',
  styleUrls: ['./children-list.component.scss']
})
export class ChildrenListComponent implements OnInit {
  ngOnInit(): void {
  }


}
