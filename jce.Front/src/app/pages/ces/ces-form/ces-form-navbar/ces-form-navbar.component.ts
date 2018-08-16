import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {CeFormService} from '../ce-form.service';
import {CE} from "../../../../@core/data/models/ce/ce";

@Component({
  selector: 'ngx-ces-form-navbar',
  templateUrl: './ces-form-navbar.component.html',
  styleUrls: ['./ces-form-navbar.component.scss'],
})
export class CesFormNavbarComponent implements OnInit {

 step: number;
 ce: CE;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private cef: CeFormService) {
    // route.params.subscribe(p => this.id = +p['id']);
  }

  ngOnInit() {
    this.cef.currentStep.subscribe(step => this.step = step);
    this.cef.currentCe.subscribe(ce => {
      this.ce = ce;
      console.log('thisce', this.ce);
    } );
  }
}
