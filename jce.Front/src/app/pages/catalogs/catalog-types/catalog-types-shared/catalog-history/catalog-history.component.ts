import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {DateInputComponent} from '../../../../../@core/utils/dateInput.component';
import {LocalDataSource} from 'ng2-smart-table';
import {Catalog} from '../../../../../@core/data/models/catalog/catalog';
import {DateConverterService} from '../../../../../@core/utils/dateConverter.service';
import {CatalogService} from '../../../../../@core/data/services/catalog.service';
import {CatalogSave} from '../../../../../@core/data/models/catalog/catalogSave';
import {CatalogGood} from '../../../../../@core/data/models/products/catalogGood';
import {ToasterService} from "angular2-toaster";
import {NotificationService} from "../../../../../@core/data/services/notification.service";

@Component({
  selector: 'ngx-catalog-history',
  templateUrl: './catalog-history.component.html',
  styleUrls: ['./catalog-history.component.scss']
})
export class CatalogHistoryComponent implements OnInit, OnChanges {

  @Input() catalog: Catalog;
  @Input() catalogSave: CatalogSave;
  @Output() deleteEvent = new EventEmitter<CatalogGood>();
  @Output() editEvent = new EventEmitter<CatalogGood>();
  catalogTest: Catalog;

  settings = {
    noDataMessage: 'Ce Catalogue ne contient aucun produit.',

    pager: {
      display: true,
      perPage: 5,
    },

    actions: {
      add: false,
    },

    edit: {
      editButtonContent: '<i class="nb-edit"></i>',
      saveButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
      confirmSave: true,
    },
    delete: {
      deleteButtonContent: '<i class="nb-trash"></i>',
      confirmDelete: true,
    },
    columns: {
      refPintel: {
        title: 'Ref Pintel',
        type: 'string',
        sort: true,
        editable: false,
        width: '10px',
      },
      title: {
        title: 'Nom du Produit',
        type: 'string',
        editable: false,
        valuePrepareFunction: (value) => {
          return (value.length > 10 ? value.substring(0, 10) + '...' : value);
        },
      },
      price: {
        title: 'Prix',
        type: 'number',
        editable: false,
        sort: true,
      },
      dateMin: {
        title: 'Date Min',
        type: 'custom',
        sort: true,
        renderComponent: DateInputComponent,
        editor: {
          type: 'list',
          config: {
            list: this.dateConverterService.getLastDate(),
          },
        },
      },
      dateMax: {
        title: 'Date Max',
        type: 'custom',
        sort: true,
        renderComponent: DateInputComponent,
        editor: {
          type: 'list',
          config: {
            list: this.dateConverterService.getLastDate(),
          },
        },
      },
      employeeParticipationMessage: {
        title: 'Participation',
        type: 'string'
      }

    },
  };
  source: LocalDataSource = new LocalDataSource();

  constructor(private dateConverterService: DateConverterService,
              private catalogService: CatalogService,
              private toasterService: ToasterService,
              private notificationService: NotificationService) {
  }

  ngOnInit() {
    this.refreshTable()
  }

  ngOnChanges(changes: SimpleChanges) {
    console.log('change', changes);
    this.refreshTable();

  }

  refreshTable() {
    this.source.load(this.catalog.catalogGoods
      .filter(cg => cg.isAddedManually === true));
  }

  onDeleteConfirmation(event): void {

    if (window.confirm('Êtes vous sûres de vouloir supprimer ce produit de ce catalogue ?')) {
      this.deleteEvent.emit(event);
    } else {
      event.confirm.reject();
    }
  }

  onEditConfirm(event): void {
    console.log('evented', event);
    if (this.dateValidity(event.newData.dateMin, event.newData.dateMax)) {
      console.log('event.', event.newData);
      this.editEvent.emit(event.newData)
    } else {
      const title = 'Erreur sur les Dates'
      const body = 'Vérifiez les dates minimum et maximum du produit édité.'
      this.toasterService.popAsync(
        this.notificationService.showWarningToast(title, body));
    }
  }

  dateValidity(dateMin, dateMax): Boolean {
    if (dateMin <= dateMax) {
      return true;
    }
    return false;
  }

}
