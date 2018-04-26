import { Customer } from '../models/customer';
import { Title } from '../models/title';

export class FindCustomers {
	static readonly type = '[Customer] FindCustomers';
}

export class LoadCustomers {
	static readonly type = '[Customer] LoadCustomers';
	constructor(public readonly payload: Customer[]) {}
}

export class SelectCustomer {
	static readonly type = '[Customer] SelectCustomer';
	constructor(public readonly payload: string) {}
}

export class UpdateCustomer {
	static readonly type = '[Customer] UpdateCustomer';
	constructor(public readonly payload: Customer) {}
}

export class UpdateTitle {
	static readonly type = '[Customer] UpdateTitle';
	constructor(public readonly payload: { customerId: string; title: Title }) {}
}
