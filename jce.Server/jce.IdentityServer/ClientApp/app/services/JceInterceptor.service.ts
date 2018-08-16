import {Injectable, Injector} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class JceInterceptor implements HttpInterceptor {

    constructor(private injector: Injector) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        request = request.clone({
            // setHeaders: {
            //     Authorization: `Bearer ${this.authService.getAccessToken()}`
            // }
        });
        return next.handle(request);
    }
    // protected get authService(): OAuthService {
    //     return this.injector.get(OAuthService);
    // }
}