import { Component } from '@angular/core';

import { Observable } from 'rxjs';
// use "../node_modules/clarity-icons/clarity-icons.min.js" in angular-cli/scripts if ClarityIcons will be removed from here
import { ClarityIcons } from '@clr/icons';
import { Store, Select } from '@ngxs/store';

import { Logout, CheckSession } from './auth/auth.actions';
import { AuthState } from './auth/auth.state';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
})
export class AppComponent {
	@Select(AuthState.isAuthenticated) isAuthenticated$: Observable<boolean>;

	public headerTitle = 'Cyrka Project';

	constructor(private _store: Store) {
		// tslint:disable-next-line:quotemark
		// tslint:disable-next-line:max-line-length
		ClarityIcons.add({
			'cyrka-logo':
				'<svg version="1.0" xmlns="http://www.w3.org/2000/svg" width="16.000000pt" height="16.000000pt" viewBox="0 0 16.000000 16.000000" preserveAspectRatio="xMidYMid meet"><g transform="translate(0.000000,16.000000) scale(0.100000,-0.100000)" fill="#000000" stroke="none"><path d="M44 107 c-43 -58 -52 -87 -25 -87 13 0 19 5 15 14 -7 17 34 99 45 91 15 -9 52 -86 46 -96 -4 -5 3 -9 14 -9 12 0 21 5 21 12 0 19 -63 118 -75 118 -5 0 -24 -20 -41 -43z"/><path d="M65 50 c0 -20 5 -30 15 -30 10 0 15 10 15 30 0 20 -5 30 -15 30 -10 0 -15 -10 -15 -30z"/></g></svg>',
		});
	}

	logout() {
		this._store.dispatch(Logout);
	}
	login() {
		this._store.dispatch(CheckSession);
	}
}
