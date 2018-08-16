import {Component, Input, OnInit} from '@angular/core';
import {Key} from "../../../@core/data/models/shared/key";
import {Routing} from "../../../@core/data/models/shared/Routing";

@Component({
  selector: 'ngx-shared-navbar',
  templateUrl: './shared-navbar.component.html',
  styleUrls: ['./shared-navbar.component.scss']
})
export class SharedNavbarComponent implements OnInit {

  @Input() routes: Routing[];
  @Input() title: string;

  constructor() { }

  ngOnInit() {
  }

}
