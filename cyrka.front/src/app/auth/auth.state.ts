import { State, Action, StateContext, Selector, Store } from '@ngxs/store';
import { Router } from '@angular/router';

import { AuthStateModel, AuthResult, AuthError } from './auth.model';
import {
	Login,
	LoginSuccess,
	LoadProfile,
	Logout,
	CheckSession,
	LoginFailed,
	Callback,
} from './auth.actions';
import { AuthService } from './auth.service';

const RETURN_URL_KEY = 'return-url';

@State<AuthStateModel>({
	name: 'auth',
	defaults: {
		user: null,
		accessToken: null,
		expiresAt: -1000,
		message: 'аутентификация...',
	},
})
export class AuthState {
	constructor(
		private _store: Store,
		private _authService: AuthService,
		private _router: Router
	) {}

	@Selector()
	static isAuthenticated(state: AuthStateModel) {
		return state.accessToken && state.expiresAt && Date.now() < state.expiresAt;
	}

	@Action(Login)
	login(sc: StateContext<AuthStateModel>, { payload }: Login) {
		if (payload) {
			localStorage.setItem(RETURN_URL_KEY, payload);
		}
		this._authService.login();
	}

	@Action(Callback)
	callback() {
		this._authService.handleLoginCallback(this.handleAuthResult, this.handleError);
	}

	@Action(CheckSession)
	checkSession(sc: StateContext<AuthStateModel>, { payload }: Login) {
		if (payload) {
			localStorage.setItem(RETURN_URL_KEY, payload);
		}
		this._authService.checkSession();
	}

	@Action(LoginSuccess)
	loginSuccess(
		{ getState, patchState, dispatch }: StateContext<AuthStateModel>,
		{ payload }: LoginSuccess
	) {
		const state = getState();
		patchState({
			accessToken: payload.token,
			expiresAt: payload.expiresAt,
		});
		const url = localStorage.getItem(RETURN_URL_KEY) || '';
		this._router.navigateByUrl(url);
		localStorage.removeItem(RETURN_URL_KEY);
		return dispatch(LoadProfile);
	}

	@Action(Logout)
	logout({ patchState }: StateContext<AuthStateModel>) {
		patchState({
			accessToken: null,
			expiresAt: -1000,
			user: null,
		});
		this._authService.logout();
	}

	@Action(LoginFailed)
	loginFailed(
		{ patchState }: StateContext<AuthStateModel>,
		{ payload }: LoginFailed
	) {
		patchState({ message: payload });
		// this._router.navigate(['/calback', { message: payload }]);
	}

	private handleAuthResult(authResult: AuthResult) {
		const expTime = authResult.expiresIn * 1000 + Date.now();
		this._store.dispatch(
			new LoginSuccess({
				expiresAt: expTime,
				token: authResult.accessToken,
			})
		);
	}

	private handleError(error: AuthError) {
		this._store.dispatch(
			new LoginFailed(`${error.error} : ${error.errorDescription}`)
		);
	}
}
