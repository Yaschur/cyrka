import { createFeatureSelector, createSelector } from '@ngrx/store';
import { CustomerState } from './store/customer.reducers';

export const getCustomerFeatureState = createFeatureSelector<CustomerState>(
	'customer'
);

export const getCustomerEntities = createSelector(
	getCustomerFeatureState,
	(state: CustomerState) => state.customers
);
