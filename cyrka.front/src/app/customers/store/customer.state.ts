import { State, Action, StateContext, Selector } from '@ngxs/store';
import { map, catchError } from 'rxjs/operators';

import { CustomerStateModel } from './customer-model.state';
import {
	FindCustomers,
	LoadCustomers,
	SelectCustomer,
	UpdateCustomer,
	UpdateTitle,
} from './customer.actions';
import { CustomerApiService } from '../services/customer-api.service';
import { of } from 'rxjs';

@State<CustomerStateModel>({
	name: 'customer',
	defaults: {
		customers: [],
		selectedCustomer: null,
	},
})
export class CustomerState {
	@Selector()
	static getCustomers(state: CustomerStateModel) {
		return state.customers;
	}

	@Selector()
	static getCustomer(state: CustomerStateModel) {
		return state.customers.find(cs => cs.id === state.selectedCustomer);
	}

	constructor(private readonly _customerApi: CustomerApiService) {}

	@Action(FindCustomers)
	findCustomers(sc: StateContext<CustomerStateModel>) {
		if (sc.getState().customers.length > 0) {
			return;
		}
		this._customerApi.fetchAll().pipe(
			map(res => sc.dispatch(new LoadCustomers(res))),
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}

	@Action(LoadCustomers)
	getCustomer(sc: StateContext<CustomerStateModel>, a: LoadCustomers) {
		sc.patchState({
			customers: a.payload,
		});
	}

	@Action(SelectCustomer)
	selectCustomer(sc: StateContext<CustomerStateModel>, a: SelectCustomer) {
		sc.patchState({
			selectedCustomer: a.payload,
		});
	}

	@Action(UpdateCustomer)
	updateCustomer(sc: StateContext<CustomerStateModel>, a: UpdateCustomer) {
		(a.payload.id
			? this._customerApi.change(a.payload.id, a.payload)
			: this._customerApi.register(a.payload)
		).pipe(
			map(() => sc.patchState({ customers: [] })),
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}

	@Action(UpdateTitle)
	updateTitle(sc: StateContext<CustomerStateModel>, a: UpdateTitle) {
		(a.payload.title.id
			? this._customerApi.changeTitle(
					a.payload.customerId,
					a.payload.title.id,
					a.payload.title
			  )
			: this._customerApi.addTitle(a.payload.customerId, a.payload.title)
		).pipe(
			map(() => sc.patchState({ customers: [] })),
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}
}
