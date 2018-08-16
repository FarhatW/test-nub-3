import {Component, OnInit, ViewChild} from '@angular/core';
import {Role} from "../../../@core/data/models/Role/Role";
import {RolesService} from "../../../@core/data/services/roles.service";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {AddRoleModalComponent, } from "../modal/AddRoleModal.component";
import {Page} from "../../../@core/data/models/shared/page";
import {DatatableComponent} from "@swimlane/ngx-datatable";
import {DeleteRoleModalComponent} from "../modal/DeleteRoleModal.component";
import {HelpersService} from "../../../@core/utils/helpers.service";

@Component({
  selector: 'ngx-roles-list',
  templateUrl: './roles-list.component.html'
})
export class RolesListComponent implements OnInit {
  editing = {};

  @ViewChild('rolesTable') rolesTable: DatatableComponent;
  private readonly PAGE_SIZE = 10;
  roles: Role[] = [];
  selected;
  isSortAscending: boolean = true;
  page = new Page();
  sortBy: string = 'id';
  query: any = {
    pageSize: this.page.size,
    page: this.page.pageNumber,
    isSortAscending: this.isSortAscending,
    sortBy: this.sortBy,
  };

  queryResult: any = {totalitems: 0};
  rows: Role[] = [];
  columns: any [] = [
    {title: 'Id', key: 'id', isSortable: true},
    {title: 'Nom de role', key: 'name', isSortable: true},
    {title: 'Auteur de création', key: 'createdBy', isSortable: true},
    {title: 'Date de création', key: 'createdOn', isSortable: true},
    {title: 'Date de création', key: 'rank', isSortable: true},
    {title: 'Auteur de mise à jour', key: 'updatedBy', isSortable: true},
    {title: 'Date de mise ajour', key: 'updatedOn', isSortable: true},
    {title: ''}
  ];
  constructor( private roleService: RolesService,
               private  modalService: NgbModal,
               private helperService: HelpersService) {

    this.page.size = 10;
    this.query.pageSize = 10;
    this.query.page = 0;
    this.page.totalElements = 0;
    this.page.pageNumber = 0;
    this.page.totalPages = 0;
  }

  ngOnInit() {
    this.setPage({offset: 0, pageSize: 10}, '');
    this.rolesTable.messages.emptyMessage = 'Aucun role';
    this.rolesTable.messages.selectedMessage = 'role sélectionné';
    this.rolesTable.messages.totalMessage = 'roles au total';
    this.populateRoles();
  }

  setPage(pageInfo, searchValue) {
    this.query.page = pageInfo.offset + 1;
    this.query.pageSize = this.page.size;
    this.query.search = searchValue;

    this.roleService.getAll(this.query).subscribe(pagedData => {
      this.rows = pagedData.items;
      this.page.totalElements = pagedData.totalItems;
      this.page.pageNumber = 0;
    });
  }

  populateRoles() {
    this.roleService.getAll(this.query).subscribe(role => {
      this.queryResult = role;
      this.queryResult.totalitems;
      this.query;
    });
  }
  update(roleId) {

  }


  getRows(query) {
    this.roleService.getAll(this.query).subscribe(pagedData => {
      // this.rows = [];
      this.rows = pagedData.items;
      this.page.totalElements = pagedData.totalItems;
      this.page.pageNumber = query.page - 1;
    });
  }

  onSort(event) {
    const sort = event.sorts[0];
    this.query.isSortAscending = sort.dir === 'asc' ? true : false;
    this.query.sortBy = sort.prop;
    this.getRows(this.query);
  }


  showAddModal() {

    const activeModal = this.modalService.open(AddRoleModalComponent, { size: 'lg', container: 'nb-layout' });
    activeModal.result.then(result => {
     if(result === true)
       this.setPage({offset: 0, pageSize: 10}, '');
    })
    activeModal.componentInstance.modalHeader = 'Ajouter un Role';
  }

  showDeleteModal(roleId) {

    const activeModal = this.modalService.open(DeleteRoleModalComponent, { size: 'lg', container: 'nb-layout' });
    activeModal.result.then(result => {
      if(result === true)
        this.roleService.delete(roleId).subscribe(r => {
          this.setPage({offset: 0, pageSize: 10}, '');
        })
    })
    activeModal.componentInstance.modalHeader = 'Supprimer un Role';
  }


  updateValue(event, cell, rowIndex) {
    console.log('inline editing rowIndex', this.rows[rowIndex])
    this.editing[rowIndex + '-' + cell] = false;
    this.rows[rowIndex][cell] = event.target.value;
    this.roleService.update(this.rows[rowIndex]).subscribe(r => {
      this.rows = [...this.rows];
      console.log('UPDATED!', this.rows[rowIndex][cell]);
    })
  }


}
