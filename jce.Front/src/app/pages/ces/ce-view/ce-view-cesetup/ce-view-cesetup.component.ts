import {Component, Input, OnInit} from '@angular/core';
import {CeSetup} from "../../../../@core/data/models/ce/ceSetup";

@Component({
  selector: 'ngx-ce-view-cesetup',
  templateUrl: './ce-view-cesetup.component.html',
  styleUrls: ['./ce-view-cesetup.component.scss']
})
export class CeViewCesetupComponent implements OnInit {

  @Input() ceSetup: CeSetup;
  @Input() ceId: number;

  constructor() { }

  ngOnInit() {

  }

}
