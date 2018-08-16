import { Injectable} from '@angular/core';
import {BodyOutputType, Toast, ToasterService} from 'angular2-toaster';

@Injectable()
export class NotificationService {

  constructor() {
  }

  showSuccessToast(title: string, body: string) {
    const toast: Toast = {
      type: 'success',
      title: title,
      body: body,
      timeout: 5000,
      showCloseButton: true,
      bodyOutputType: BodyOutputType.TrustedHtml,
    };
    return toast;


  }

  showErrorToast(title: string, body: string) {
    const toast: Toast = {
      type: 'error',
      title: title,
      body: body,
      timeout: 5000,
      showCloseButton: true,
      bodyOutputType: BodyOutputType.TrustedHtml,
    };

    return toast;
  }

   showWarningToast(title: string, body: string) {
    const toast: Toast = {
      type: 'warning',
      title: title,
      body: body,
      timeout: 5000,
      showCloseButton: true,
      bodyOutputType: BodyOutputType.TrustedHtml,
    };
     return toast;

   }
}
