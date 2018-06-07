import { Injectable } from '@angular/core';
import {
	CanActivate,
	ActivatedRouteSnapshot,
	RouterStateSnapshot,
} from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Store, Select } from '@ngxs/store';

import { CheckSession } from './auth.actions';
import { AuthState } from './auth.state';

@Injectable()
export class AuthGuard implements CanActivate {
	constructor(private _store: Store) {}

	canActivate(_: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
		return this._isAuthenticated$.pipe(
			map(a => {
				if (a) {
					return true;
				}
				this._store.dispatch(new CheckSession(state.url));
				return false;
			})
		);
	}

	@Select(AuthState.isAuthenticated)
	private _isAuthenticated$: Observable<boolean>;
}
