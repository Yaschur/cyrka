import { State, Action, StateContext } from '@ngxs/store';

import { AuthStateModel } from './auth.model';
import { Login, LoginSuccess, LoadProfile } from './auth.actions';
import { AuthService } from './auth.service';

@State<AuthStateModel>({
	name: 'auth',
	defaults: {
		user: null,
		accessToken: null,
		expiresAt: null,
	},
})
export class AuthState {
	constructor(private _authService: AuthService) {}

	@Action(Login)
	login() {
		this._authService.login();
	}

	@Action(LoginSuccess)
	loginSuccess(
		{ getState, setState, dispatch }: StateContext<AuthStateModel>,
		{ payload }: LoginSuccess
	) {
		const state = getState();
		setState({
			...state,
			accessToken: payload.token,
			expiresAt: payload.expiresAt,
		});
		return dispatch(LoadProfile);
	}
}
