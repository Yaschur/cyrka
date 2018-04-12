import { Injectable } from '@angular/core';

import { Store } from '@ngxs/store';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import * as auth0 from 'auth0-js';

import { environment } from '../../environments/environment';
import { UserProfile } from './user-profile';
import { LoginSuccess, LoginFailed } from './auth.actions';

@Injectable()
export class AuthService {
	accessToken: string;

	get loggedIn$(): Observable<boolean> {
		return this._loggedIn$.asObservable();
	}

	get authenticated(): boolean {
		const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
		return Date.now() < expiresAt && this._loggedIn;
	}

	constructor(private _store: Store) {
		this._auth0 = new auth0.WebAuth({
			clientID: environment.auth.clientID,
			domain: environment.auth.domain,
			responseType: 'token',
			redirectUri: environment.auth.redirect,
			audience: environment.auth.audience,
			scope: environment.auth.scope,
		});
		this._loggedIn = false;
		this._loggedIn$ = new BehaviorSubject<boolean>(this._loggedIn);
	}

	login() {
		this._auth0.authorize({ connection: 'google-oauth2' });
	}

	logout() {
		localStorage.removeItem('expires_at');
		this.accessToken = undefined;
		this._userProfile = undefined;
		this._setLoggedIn(false);
	}

	handleLoginCallback() {
		this._auth0.parseHash((err, authResult) => {
			if (authResult && authResult.accessToken) {
				window.location.hash = '';
				const expTime = authResult.expiresIn * 1000 + Date.now();
				this._store.dispatch(
					new LoginSuccess({
						expiresAt: expTime,
						token: authResult.accessToken,
					})
				);
			} else if (err) {
				this._store.dispatch(new LoginFailed(err.error));
			}
		});
	}

	// getUserInfo(authResult) {
	// 	this._auth0.client.userInfo(authResult.accessToken, (err, profile) => {
	// 		this._setSession(authResult, profile);
	// 	});
	// }

	private _auth0;
	private _userProfile: UserProfile;
	private _loggedIn: boolean;
	private _loggedIn$: BehaviorSubject<boolean>;

	private _setLoggedIn(value: boolean) {
		this._loggedIn$.next(value);
		this._loggedIn = value;
	}

	private _setSession(authResult, profile) {
		const expTime = authResult.expiresIn * 1000 + Date.now();
		localStorage.setItem('expires_at', JSON.stringify(expTime));
		this.accessToken = authResult.accessToken;
		this._userProfile = profile;
		this._setLoggedIn(true);
	}
}
