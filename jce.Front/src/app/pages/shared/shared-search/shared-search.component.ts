import { Component, OnInit } from '@angular/core';
import {SearchService} from "../../../@core/data/services/search.service";
import {Subject} from "rxjs/Subject";

@Component({
  selector: 'ngx-shared-search',
  templateUrl: './shared-search.component.html',
  styleUrls: ['./shared-search.component.scss']
})
export class SharedSearchComponent implements OnInit {
  searchTerm$ = new Subject<string>();
  searchType: string;

  constructor(private searchService: SearchService ) {
    this.searchService.searchPerPage(this.searchTerm$)
      .subscribe(results => {
        console.log(results)
      })
  }

  ngOnInit() {


  }

}
