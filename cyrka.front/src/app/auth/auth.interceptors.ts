import { Injectable } from '@angular/core';
import {
	HttpInterceptor,
	HttpRequest,
	HttpHandler,
	HttpResponse,
} from '@angular/common/http';

import { Store } from '@ngxs/store';
import { tap } from 'rxjs/operators';

import { AuthService } from './auth.service';
// import { AuthState } from './auth.state';
import { AuthStateModel } from './auth.model';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
	constructor(private _store: Store) {}

	intercept(req: HttpRequest<any>, next: HttpHandler) {
		const token = this._store.selectSnapshot<string>(
			(state: { auth: AuthStateModel }) => state.auth.accessToken
		);
		const authReq = req.clone({
			setHeaders: { Authorization: `Bearer ${token}` },
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
