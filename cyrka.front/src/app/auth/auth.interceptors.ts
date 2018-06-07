import { Injectable } from '@angular/core';
import {
	HttpInterceptor,
	HttpRequest,
	HttpHandler,
	HttpErrorResponse,
} from '@angular/common/http';
import { Router } from '@angular/router';

import { Store } from '@ngxs/store';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

import { AuthStateModel } from './auth.model';
import { CheckSession } from './auth.actions';

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
	}
}

@Injectable()
export class UnauthInterceptor implements HttpInterceptor {
	constructor(private _store: Store, private _router: Router) {}

	intercept(req: HttpRequest<any>, next: HttpHandler) {
		return next.handle(req).pipe(
			catchError(error => {
				if (
					error instanceof HttpErrorResponse &&
					(<HttpErrorResponse>error).status === 401
				) {
					this._store.dispatch(new CheckSession(this._router.url));
				}

				return throwError(error);
			})
		);
	}
}
