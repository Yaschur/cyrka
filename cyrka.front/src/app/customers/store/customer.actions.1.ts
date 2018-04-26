import { Action } from '@ngrx/store';
import { Customer } from '../models/customer';
import { Title } from '../models/title';

export enum CustomerActionTypes {
	FIND_CUSTOMERS = '[customer] FIND_CUSTOMERS',
	CUSTOMERS_RECEIVED = '[customer] CUSTOMERS_RECEIVED',
	GET_CUSTOMER = '[customer] GET_CUSTOMER',
	CUSTOMER_RECEIVED = '[customer] CUSTOMER_RECEIVED',
	UPDATE_CUSTOMER = '[customer] UPDATE_CUSTOMER',
	UPDATE_TITLE = '[customer] UPDATE_TITLE',
}

export class FindCustomers implements Action {
	readonly type = CustomerActionTypes.FIND_CUSTOMERS;
}

export class CustomersReceived implements Action {
	readonly type = CustomerActionTypes.CUSTOMERS_RECEIVED;

	constructor(public customers: Customer[]) {}
}

export class GetCustomer implements Action {
	readonly type = CustomerActionTypes.GET_CUSTOMER;

	constructor(public customerId: string) {}
}

export class CustomerReceived implements Action {
	readonly type = CustomerActionTypes.CUSTOMER_RECEIVED;

	constructor(public customer: Customer) {}
}

export class UpdateCustomer implements Action {
	readonly type = CustomerActionTypes.UPDATE_CUSTOMER;

	constructor(public customer: Customer) {}
}

export class UpdateTitle implements Action {
	readonly type = CustomerActionTypes.UPDATE_TITLE;

	constructor(public customerId: string, public title: Title) {}
}

export type CustomerActions =
	| FindCustomers
	| CustomersReceived
	| GetCustomer
	| CustomerReceived
	| UpdateCustomer
	| UpdateTitle;
