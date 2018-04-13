import { Injectable } from '@angular/core';
import {
	Router,
	CanActivate,
	ActivatedRoute,
	ActivatedRouteSnapshot,
	RouterStateSnapshot,
} from '@angular/router';

import { Store } from '@ngxs/store';

import { AuthService } from './auth.service';
import { CheckSession } from './auth.actions';

@Injectable()
export class AuthGuard implements CanActivate {
	constructor(private _auth: AuthService, private _store: Store) {}

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
		if (this._auth.isAuthenticated) {
			return true;
		}
		this._store.dispatch(new CheckSession(state.url));
		return false;
	}
}
