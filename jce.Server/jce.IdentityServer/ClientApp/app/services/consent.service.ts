import { Injectable } from '@angular/core';
import {Http} from '@angular/http';

import { HelperService } from './helper.service';
import {HttpClient} from "@angular/common/http";
import {Consent} from "../Models/Consent";
import {ProcessConsentResult} from "../Models/ProcessConsentResult";

@Injectable()
export class ConsentService {

  constructor(private http: HttpClient, private helper: HelperService) { }


    getConsent(returnUrl: string) {
        return this.http.post<Consent>('http://localhost:5000/api/consent/',  {returnUrl: returnUrl });
    }

    postConsent(model: {}) {
        return this.http.post<ProcessConsentResult>('http://localhost:5000/api/consent/process',  model);
    }

  

}
