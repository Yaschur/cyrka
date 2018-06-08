import { Injectable } from '@angular/core';

import { WebAuth } from 'auth0-js';

import { environment } from '../../environments/environment';
import { AuthResult, AuthError } from './auth.model';

@Injectable()
export class AuthService {
	constructor() {
		this._auth0 = new WebAuth({
			clientID: environment.auth.clientID,
			domain: environment.auth.domain,
			responseType: 'token',
			redirectUri: environment.auth.redirect,
			audience: environment.auth.audience,
			scope: environment.auth.scope,
		});
	}

	login() {
		this._auth0.authorize({ connection: 'google-oauth2' });
	}

	logout() {
		this._auth0.logout({ returnTo: environment.auth.redirect });
	}

	handleLoginCallback(
		succeedCalback: (authResult: AuthResult) => void,
		failedCallback: (authError: AuthError) => void
	) {
		this._auth0.parseHash((err, authResult) => {
			if (authResult && authResult.accessToken) {
				window.location.hash = '';
				succeedCalback({
					accessToken: authResult.accessToken,
					expiresIn: authResult.expiresIn,
				});
			} else if (err) {
				window.location.hash = '';
				failedCallback({
					error: err.error,
					errorDescription: err.errorDescription,
				});
			} else {
				failedCallback({
					error: 'нет доступа',
					errorDescription: 'Вы вышли из системы',
				});
			}
		});
	}

	checkSession(
		succeedCalback: (authResult: AuthResult) => void,
		failedCallback: (authError: AuthError) => void
	) {
		this._auth0.checkSession({}, (err, authResult) => {
			if (authResult && authResult.accessToken) {
				succeedCalback(authResult);
			} else if (err) {
				console.log('cant restore session');
				console.log(err);
				failedCallback({
					error: err.error,
					errorDescription: err.errorDescription,
				});
			}
		});
	}

	private _auth0: WebAuth;
}
