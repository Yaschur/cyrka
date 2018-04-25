import { State } from '@ngxs/store';

import { CustomerStateModel } from './customer-model.state';

@State<CustomerStateModel>({
	name: 'customer',
	defaults: {
		customers: [],
		selectedCustomer: null,
	},
})
export class CustomerState {
	
}
