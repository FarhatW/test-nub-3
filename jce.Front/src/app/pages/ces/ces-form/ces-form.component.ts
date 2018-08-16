import {AfterViewInit, Component, Input, OnInit, ViewChild} from '@angular/core';
import {CE} from '../../../@core/data/models/ce/ce';
import {CeService} from '../../../@core/data/services/ce.service';
import {ActivatedRoute, Router} from '@angular/router';
// import {ToasterConfig, ToasterService} from "angular2-toaster";
import {Form, FormBuilder} from '@angular/forms';
import { trigger, state, style, animate, transition} from '@angular/animations';
import {map} from 'rxjs/operator/map';
import {ToastyService} from "ng2-toasty";
import {ToasterService} from "angular2-toaster";
import {CesFormInfosComponent} from "./ces-form-infos/ces-form-infos.component";

@Component({
  selector: 'ngx-ces-form',
  templateUrl: './ces-form.component.html',
  styleUrls: ['./ces-form.component.scss'],
})
export class CesFormComponent implements OnInit, AfterViewInit {

  constructor(private route: ActivatedRoute,
              private ceService: CeService,
              private router: Router) {
  }

  @ViewChild(CesFormInfosComponent) cesFormInfosComponent;

  id: number;
  filter: null;
  ce: CE;

  ngOnInit() {

  }
  ngAfterViewInit() {

  }
}
