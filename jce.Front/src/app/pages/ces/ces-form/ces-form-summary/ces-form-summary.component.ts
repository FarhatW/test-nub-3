import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, NavigationEnd, Router} from '@angular/router';
import {CeService} from '../../../../@core/data/services/ce.service';
import {CE} from '../../../../@core/data/models/ce/ce';
import {CeFormService} from "../ce-form.service";

@Component({
  selector: 'ngx-ces-form-summary',
  templateUrl: './ces-form-summary.component.html',
  styleUrls: ['./ces-form-summary.component.scss']
})
export class CesFormSummaryComponent implements OnInit {

  id: number;
  filter: null;
  ce: CE;

  routeEvents$: any;

  constructor(private route: ActivatedRoute,
              private cef: CeFormService,
              private router: Router) {
    this.route.params.subscribe(p => this.id = +p['id'] || 0);

  }

  ngOnInit() {
      this.cef.currentCe.subscribe(ce => this.ce = ce);
    }
}
