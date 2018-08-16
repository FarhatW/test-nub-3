import { Component, OnInit } from '@angular/core';
import {ProcessConsentResult} from "../../Models/ProcessConsentResult";
import {Consent} from "../../Models/Consent";
import {ActivatedRoute, Router, UrlSerializer, UrlTree} from "@angular/router";
import {ScopesModel} from "../../Models/ScopesModel";
import {User} from "../../Models/user";
import {ConsentService} from "../../services/consent.service";
import {ToastyService} from "ng2-toasty";

@Component({
  selector: 'app-consent',
  templateUrl: './consent.component.html',
  styleUrls: ['./consent.component.css']
})
export class ConsentComponent implements OnInit {

    processConsentResult: ProcessConsentResult;
    
    consent: Consent = {
        allowRememberConsent : false,
        clientLogoUrl : '',
        clientName : '',
        clientUrl : '',
        identityScopes : [],
        resourceScopes :[]
    };
    
    url: UrlTree;
   
    returnUrl : any;
  
    inputModel: ScopesModel = {
        RememberConsent : false,
        button : '',
        returnUrl : '',
        scopesConsented : []
    };
   
    
  constructor(
      private  consentService: ConsentService,
      private router: Router,
      private urlSerializer: UrlSerializer,
      private toastyService: ToastyService,
      private activatedRoute: ActivatedRoute,
      
  ) { }

  ngOnInit() {


     // get return url from route parameters or default to '/'
     this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';
      
      this.consentService.getConsent(this.returnUrl).subscribe(c => {
        console.log("consent", c);
        this.consent = c;
      } );
  }

  sendConsent(btnValue: any) {
      this.getScopesConsented();
      this.inputModel.returnUrl = this.returnUrl;
      this.inputModel.button = btnValue;
      this.inputModel.RememberConsent = this.consent.allowRememberConsent;

      this.consentService.postConsent(this.inputModel).subscribe(result => {

          this.processConsentResult = result;
          console.log(this.processConsentResult);

          if (this.processConsentResult.isRedirect) {
              const tree: UrlTree = this.router.parseUrl(this.processConsentResult.redirectUri);

              window.location.href = this.processConsentResult.redirectUri;

          }

//          if (this.processConsentResult.hasValidationError) {
//              console.log('HasValidationError', this.processConsentResult.hasValidationError);
//              this.toastyService.error({
//                  title: 'Error : ',
//                  msg: this.processConsentResult.validationError,
//                  theme: 'bootstrap',
//                  showClose: true,
//                  timeout: 5000
//              });
//          }
//
//          if (this.processConsentResult.showView) {
//              console.log('ViewModel.clientUrl]', this.processConsentResult.viewModel.clientUrl);
//              this.router.navigate([this.processConsentResult.viewModel.clientUrl]);
//          }
      });
  }

  // private helper methods
  toQueryString(obj: any) {
      const parts = [''];

      for (const property in obj) {
          const value = obj[property];
          if (value != null && value !== undefined) {
              parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
          }
      }

      return parts.join('&');
  }

  getScopesConsented() {
      this.inputModel.scopesConsented = [];
      this.consent.identityScopes.forEach(item => {
          this.inputModel.scopesConsented.push(item.name);
      });

      this.consent.resourceScopes.forEach(item => {

          this.inputModel.scopesConsented.push(item.name);
      });
  }

   

}
