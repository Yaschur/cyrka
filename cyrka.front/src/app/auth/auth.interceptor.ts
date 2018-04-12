import { Injectable } from '@angular/core';
import {
	HttpInterceptor,
	HttpRequest,
	HttpHandler,
	HttpResponse,
} from '@angular/common/http';

import { AuthService } from './auth.service';
import { tap } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
	constructor(private _auth: AuthService) {}

	intercept(req: HttpRequest<any>, next: HttpHandler) {
		const authReq = req.clone({
			setHeaders: { Authorization: `Bearer ${this._auth.accessToken}` },
		});

		return next.handle(authReq);
		// .pipe(tap(event => {
		//   // There may be other events besides the response.
		//   if (event instanceof HttpResponse) {
		//     if (<)
		//   }
		// });
	}
}
