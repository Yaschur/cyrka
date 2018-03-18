import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { Effect, Actions, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { RouterNavigationAction, ROUTER_NAVIGATION } from '@ngrx/router-store';
import {
	filter,
	withLatestFrom,
	switchMap,
	map,
	catchError,
} from 'rxjs/operators';
import { of } from 'rxjs/observable/of';

import { CustomerState } from './customer.reducers';
import { CustomerApiService } from '../services/customer-api.service';
import {
	FindCustomers,
	GetCustomer,
	CustomerActionTypes,
	CustomerReceived,
	CustomersReceived,
	UpdateCustomer,
} from './customer.actions';

@Injectable()
export class CustomerEffects {
	@Effect()
	navigateCustomers$ = this._actions$.pipe(
		ofType<RouterNavigationAction<RouterStateSnapshot>>(ROUTER_NAVIGATION),
		filter(
			r =>
				this.isOnCustomers(r.payload.routerState.root.firstChild) &&
				!this.isWithCustomerId(r.payload.routerState.root.firstChild)
		),
		withLatestFrom(this._store$),
		filter(s => !s[1].customer.listLoaded),
		switchMap(() => of(new FindCustomers()))
	);

	@Effect()
	navigateCustomer$ = this._actions$.pipe(
		ofType<RouterNavigationAction<RouterStateSnapshot>>(ROUTER_NAVIGATION),
		filter(
			r =>
				this.isOnCustomers(r.payload.routerState.root.firstChild) &&
				this.isWithCustomerId(r.payload.routerState.root.firstChild)
		),
		map(r => r.payload.routerState.root.firstChild.paramMap.get('customerId')),
		withLatestFrom(this._store$),
		filter(s => !s[1].customer.customers.some(jt => jt.id === s[0])),
		switchMap(s => of(new GetCustomer(s[0])))
	);

	@Effect()
	fetchJobtypes$ = this._actions$.pipe(
		ofType<FindCustomers>(CustomerActionTypes.FIND_CUSTOMERS),
		switchMap(() => this._apiService.fetchAll()),
		map(res => new CustomersReceived(res)),
		catchError(e => {
			console.log('Network error', e);
			return of();
		})
	);

	@Effect()
	fetchJobtype$ = this._actions$.pipe(
		ofType<GetCustomer>(CustomerActionTypes.GET_CUSTOMER),
		switchMap(a => this._apiService.getById(a.customerId)),
		map(res => new CustomerReceived(res)),
		catchError(e => {
			console.log('Network error', e);
			return of();
		})
	);

	@Effect()
	updateJobtype$ = this._actions$.pipe(
		ofType<UpdateCustomer>(CustomerActionTypes.UPDATE_CUSTOMER),
		switchMap(
			a =>
				a.customer.id
					? this._apiService.change(a.customer.id, a.customer)
					: this._apiService.register(a.customer)
		),
		map(() => new FindCustomers()),
		catchError(e => {
			console.log('Network error', e);
			return of();
		})
	);

	constructor(
		private _actions$: Actions,
		private _store$: Store<{ customer: CustomerState }>,
		private _apiService: CustomerApiService
	) {}

	private isOnCustomers(r: ActivatedRouteSnapshot): boolean {
		return r.routeConfig.path.startsWith('customers');
	}
	private isWithCustomerId(r: ActivatedRouteSnapshot): boolean {
		return r.paramMap.has('customerId');
	}
}