import {Component, Input, OnInit} from '@angular/core';
import {AbstractControlDirective, AbstractControl} from '@angular/forms';
import {Key} from '../../data/models/shared/key';
import {PagerService} from "../../utils/pager.service";
import {Http} from "@angular/http";

@Component({
  selector: 'ngx-show-errors',
  template: `
    <div class="row">
      <div class="col-md-3">
      </div>
    </div>
    <div class="row error" *ngFor="let error of pagedItems; let i = index">
      <div class="col-md-3 goodid">{{ error.goodId }}</div>
      <div class="col-9">
        <div class="row" *ngFor="let err of error.errors">
          {{ err }}
        </div>
      </div>
    </div>
    <div *ngIf="pager.pages && pager.pages.length" class="pagination-container">
      <ul>
        <div class="btn-group btn-toggle-group btn-outline-toggle-group">
          <li [ngClass]="{disabled:pager.currentPage === 1}" class="btn btn-outline-success btn-sm">
            <a (click)="setPage(1)"> << </a>
          </li>
          <li [ngClass]="{disabled:pager.currentPage === 1}" class="btn btn-outline-success btn-sm">
            <a (click)="setPage(pager.currentPage - 1)"> < </a>
          </li>
          <li *ngFor="let page of pager.pages" [ngClass]="{active:pager.currentPage === page}"
              class="btn btn-outline-success">
            <a (click)="setPage(page)">{{page}}</a>
          </li>
          <li [ngClass]="{disabled:pager.currentPage === pager.totalPages}" class="btn btn-outline-success btn-sm">
            <a (click)="setPage(pager.currentPage + 1)"> > </a>
          </li>
          <li [ngClass]="{disabled:pager.currentPage === pager.totalPages}" class="btn btn-outline-success btn-sm">
            <a (click)="setPage(pager.totalPages)"> >> </a>
          </li>
        </div>
      </ul>
    </div>

  `,
  styles: ['.error {border-bottom: 1px black solid; }', '.error  .goodid { border-right: 1px black solid; }',
    'a { cursor: pointer;}']
})
export class ShowErrorsComponent implements OnInit {
  constructor(private http: Http, private pagerService: PagerService) {
  }

  @Input() errorsArr: Key[] = [];

  pager: any = {};
  pagedItems: any[];

  ngOnInit(): void {
    console.log('errors arrr', this.errorsArr);
    this.setPage(1)
  }

  setPage(page: number) {
    this.pager = this.pagerService.getPager(this.errorsArr.length, page);

    this.pagedItems = this.errorsArr.slice(this.pager.startIndex, this.pager.endIndex + 1);
  }
}
