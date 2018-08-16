import {Injectable} from '@angular/core';
import {SupplierFormService} from "../../pages/suppliers/suppliers-list/suppliers-list-form/shared/supplier-form.service";
import {ToasterService} from "angular2-toaster";
import {FormBuilder} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {Title} from "@angular/platform-browser";

@Injectable()
export class TitleService {

  constructor(private titleService: Title,
              private route: ActivatedRoute) {
  }


  title() {
    this.route.data.subscribe(t => {
        console.log('t', t.title);
        this.titleService.setTitle(t.title)
      }
    )
  }
}
