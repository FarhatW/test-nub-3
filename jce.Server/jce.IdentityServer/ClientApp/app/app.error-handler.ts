
import { ErrorHandler, Inject } from "@angular/core";
import { ToastyService } from 'ng2-toasty';


export class AppErrorHndler implements ErrorHandler {

    

    constructor(
        //  private ngZone : NgZone,
        @Inject(ToastyService) private toastyService  : ToastyService){

    }

    handleError(error : any):void {
        console.log("error", error);
        // Raven.captureException(error.originalError || error)
      
        // this.ngZone.run(() => {
        //     this.toastyService.error({
        //       title: 'Error',
        //       msg: 'An unexpected error happened.',
        //       theme: 'bootstrap',
        //       showClose: true,
        //       timeout: 5000
        //     });
        // });

    }

}