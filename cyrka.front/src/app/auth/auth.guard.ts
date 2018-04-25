import { Injectable } from '@angular/core';
import {
	Router,
	CanActivate,
	ActivatedRoute,
	ActivatedRouteSnapshot,
	RouterStateSnapshot,
} from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Store, Select } from '@ngxs/store';

import { AuthService } from './auth.service';
import { CheckSession } from './auth.actions';
import { AuthState } from './auth.state';

@Injectable()
export class AuthGuard implements CanActivate {
	@Select(AuthState.isAuthenticated)
	private _isAuthenticated$: Observable<boolean>;

	constructor(private _auth: AuthService, private _store: Store) {}

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
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
}
