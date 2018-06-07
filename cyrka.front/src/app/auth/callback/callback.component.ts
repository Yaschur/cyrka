import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';
import { Store } from '@ngxs/store';

// import { AuthService } from '../auth.service';
import { Callback } from '../auth.actions';

@Component({
	selector: 'app-callback',
	templateUrl: './callback.component.html',
	styleUrls: ['./callback.component.scss'],
})
export class CallbackComponent {
	message$: Observable<string>;

	constructor(private _store: Store) {
		_store.dispatch(Callback);
		this.message$ = this._store.select(state => state.auth.message);
	}
}
