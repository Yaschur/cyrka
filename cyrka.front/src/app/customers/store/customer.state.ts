import { State, Action, StateContext } from '@ngxs/store';
import { map, catchError } from 'rxjs/operators';

import { CustomerStateModel } from './customer-model.state';
import {
	FindCustomers,
	LoadCustomers,
	SelectCustomer,
	UpdateCustomer,
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
	updateCustomer(sc: StateContext<CustomerStateModel>, a: UpdateCustomer) {}
}
