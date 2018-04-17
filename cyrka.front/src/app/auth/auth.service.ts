import { Injectable } from '@angular/core';

import { Store } from '@ngxs/store';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import * as auth0 from 'auth0-js';

import { environment } from '../../environments/environment';
import { UserProfile } from './user-profile';
import { LoginSuccess, LoginFailed, Login } from './auth.actions';
import { AuthStateModel, AuthResult, AuthError } from './auth.model';
import { take } from 'rxjs/operators';

@Injectable()
export class AuthService {
	constructor(private _store: Store) {
		this._auth0 = new auth0.WebAuth({
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
		this._auth0.logout();
	}

	handleLoginCallback(
		succeedCalback: (authResult: AuthResult) => void,
		failedCallback: (authError: AuthError) => void
	) {
		this._auth0.parseHash((err, authResult) => {
			if (authResult && authResult.accessToken) {
				window.location.hash = '';
				succeedCalback(authResult);
			} else if (err) {
				window.location.hash = '';
				failedCallback(err);
				// this._store.dispatch(
				// 	new LoginFailed(`${err.error} : ${err.errorDescription}`)
				// );
			}
		});
	}

	checkSession() {
		this._auth0.checkSession({}, (err, authResult) => {
			if (authResult && authResult.accessToken) {
				this.handleAuthResult(authResult);
			} else if (err) {
				console.log('cant restore session');
				console.log(err);
				this._store.dispatch(Login);
			}
		});
	}

	private handleAuthResult(authResult: any) {
		const expTime = authResult.expiresIn * 1000 + Date.now();
		this._store.dispatch(
			new LoginSuccess({
				expiresAt: expTime,
				token: authResult.accessToken,
			})
		);
	}

	private _auth0;
}
