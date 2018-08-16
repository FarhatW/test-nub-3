import {Component, HostBinding, Input, OnInit} from '@angular/core';
import {CeSetup} from '../../../../@core/data/models/ce/ceSetup';
import {CeSetupService} from '../../../../@core/data/services/ceSetup.service';
import {ActivatedRoute, Router} from '@angular/router';
import {fadeInAnimation} from '../../../../@theme/animations/fadeIn';
import {CE} from '../../../../@core/data/models/ce/ce';
import {CeService} from '../../../../@core/data/services/ce.service';
import {CeFormService} from "../ce-form.service";

@Component({
  selector: 'ngx-ces-setup-form',
  templateUrl: './ces-setup-form.component.html',
  styleUrls: ['./ces-setup-form.component.scss'],
  animations: [fadeInAnimation],
})
export class CesSetupFormComponent implements OnInit {

  private readonly CeSetupQuery: boolean = true;
  query: any = { CeSetupQuery: this.CeSetupQuery};

  id: number;
  noCE: boolean = false;

  ceSetup: CeSetup = {
    id: 0,
    ceId: 0,
    isAgeGroupChoiceLimitation: false,
    isCeParticipation: false,
    isOrderConfirmationMail: false,
    isOrderConfirmationMail4Admin: false,
    isEmployeeParticipation: false,
    isGroupingAllowed: false,
    isHomeDelivery: false,
    welcomeMessage: '',
    createdBy: '',
    updatedBy: '',
  };

  constructor(private ceSetupService: CeSetupService,
              private route: ActivatedRoute,
              private ceService: CeService,
              private cef: CeFormService,
              private router: Router) {
    route.params.subscribe(p => this.id = +p['id'] || 0);
  }

  ngOnInit() {

    this.ceService.getById(this.id, this.query)
      .subscribe(ce => {
        if (ce.ceSetup) {
          this.setCeSetup(ce.ceSetup);

        }
          console.log('ce', ce);
        },
        err => console.log(err))
    this.cef.changeFormStep(2);

  }

  private setCeSetup(ceSetup: CeSetup) {
    console.log('cesetu', this)
    this.ceSetup.id = ceSetup.id;
    this.ceSetup.ceId = ceSetup.ceId;
    this.ceSetup.isAgeGroupChoiceLimitation = ceSetup.isAgeGroupChoiceLimitation;
    this.ceSetup.isCeParticipation = ceSetup.isCeParticipation;
    this.ceSetup.isOrderConfirmationMail = ceSetup.isOrderConfirmationMail;
    this.ceSetup.isOrderConfirmationMail4Admin = ceSetup.isOrderConfirmationMail4Admin;
    this.ceSetup.isEmployeeParticipation = ceSetup.isEmployeeParticipation;
    this.ceSetup.isGroupingAllowed = ceSetup.isGroupingAllowed;
    this.ceSetup.isHomeDelivery = ceSetup.isHomeDelivery;
    this.ceSetup.welcomeMessage = ceSetup.welcomeMessage;
  }

  submit() {
    this.ceSetup.ceId = this.id;
    this.ceSetup.createdBy = 'tamere';
    this.ceSetup.updatedBy = 'tamere';
    console.log('cesetup', this.ceSetup);
    const result$ = (this.ceSetup.id) ?
      this.ceSetupService.update(this.ceSetup) : this.ceSetupService.create(this.ceSetup);
    result$.subscribe(
      data => {
        console.log('data', data);
        this.ceSetup = data;
      },
      err => console.log(err),
      () => this.router.navigate(['ces/ce-form/catalogue/' + this.ceSetup.ceId]),
    );
  }
}
