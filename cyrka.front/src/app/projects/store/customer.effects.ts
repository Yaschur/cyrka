import { Injectable } from '@angular/core';

import { Effect, Actions, ofType } from '@ngrx/effects';
import { switchMap, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import { CustomerApiService } from '../services/customer-api.service';
import {
	FindCustomers,
	ProjectActionTypes,
	LoadCustomers,
} from './project.actions';

@Injectable()
export class CustomerEffects {
	@Effect()
	findCustomers$ = this._actions$.pipe(
		ofType<FindCustomers>(ProjectActionTypes.FIND_CUSTOMERS),
		switchMap(() => this._apiService.fetchAll()),
		map(res => new LoadCustomers(res)),
		catchError(e => {
			console.log('Network error', e);
			return of();
		})
	);

	constructor(
		private _actions$: Actions,
		private _apiService: CustomerApiService
	) {}
}
