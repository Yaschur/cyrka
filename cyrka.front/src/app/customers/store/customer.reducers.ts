import { Customer } from '../models/customer';
import { CustomerActions, CustomerActionTypes } from './customer.actions';

export interface CustomerState {
	customers: Customer[];
	listLoaded: boolean;
}

export const inititalState: CustomerState = {
	customers: [],
	listLoaded: false,
};

export function customerReducer(
	state: CustomerState = inititalState,
	action: CustomerActions
) {
	switch (action.type) {
		case CustomerActionTypes.CUSTOMERS_RECEIVED: {
			return { ...state, customers: action.customers, listLodaded: true };
		}
		case CustomerActionTypes.CUSTOMER_RECEIVED: {
			const exInd = state.customers.findIndex(c => c.id === action.customer.id);
			const newInd = exInd < 0 ? state.customers.length : exInd;
			return {
				...state,
				customers: [
					...state.customers.slice(0, newInd),
					action.customer,
					...state.customers.slice(newInd + 1),
				],
			};
		}
		case CustomerActionTypes.FIND_CUSTOMERS:
		case CustomerActionTypes.GET_CUSTOMER:
		default:
			return state;
	}
}
