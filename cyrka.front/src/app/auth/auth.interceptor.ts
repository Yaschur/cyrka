import { Injectable } from '@angular/core';
import {
	HttpInterceptor,
	HttpRequest,
	HttpHandler,
} from '@angular/common/http';

import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
	constructor(private _auth: AuthService) {}

	intercept(req: HttpRequest<any>, next: HttpHandler) {
		const authReq = req.clone({
			setHeaders: { Authorization: `Bearer ${this._auth.accessToken}` },
		});

		return next.handle(authReq);
	}
}
