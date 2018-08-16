import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ToasterService} from 'angular2-toaster';
import {FormBuilder} from '@angular/forms';
import {CeService} from '../../../@core/data/services/ce.service';
import {ToastyService} from 'ng2-toasty';
import {CE} from '../../../@core/data/models/ce/ce';

@Component({
  selector: 'ngx-ce-view',
  templateUrl: './ce-view.component.html',
  styleUrls: ['./ce-view.component.scss']
})
export class CeViewComponent implements OnInit {

  id: number;
  ce: CE;

  constructor(private toasterService: ToasterService,
              private toastyService: ToastyService,
              private ceService: CeService,
              private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router) {
    route.params.subscribe(p => this.id = +p['id'] || 0);
  }

  ngOnInit() {
    if (this.id) {
      this.ceService.getById(this.id, null)
        .subscribe(res => {
            this.ce = res;
            console.log('thisce', this.ce);
          },
          err => {
            if (err.status === 404) {
              this.router.navigate(['ces/ce-list'])
            }
          });
    }
  }
}
