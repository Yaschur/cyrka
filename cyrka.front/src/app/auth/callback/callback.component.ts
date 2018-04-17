import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { Observable } from 'rxjs/Observable';

import { AuthService } from '../auth.service';
import { Store } from '@ngxs/store';

@Component({
	selector: 'app-callback',
	templateUrl: './callback.component.html',
	styleUrls: ['./callback.component.scss'],
})
export class CallbackComponent {
	message$: Observable<string>;

	constructor(private _auth: AuthService, private _store: Store) {
		_auth.handleLoginCallback();
		this.message$ = _store.select(state => state.auth.message);
	}
}
