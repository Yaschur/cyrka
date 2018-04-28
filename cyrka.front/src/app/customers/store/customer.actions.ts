import { Customer } from '../models/customer';
import { Title } from '../models/title';

export class FindCustomers {
	static readonly type = '[Customer] FindCustomers';
}

export class LoadCustomers {
	constructor(public readonly payload: Customer[]) {}
	static readonly type = '[Customer] LoadCustomers';
}

export class SelectCustomer {
	constructor(public readonly payload?: string) {}
	static readonly type = '[Customer] SelectCustomer';
}

export class UpdateCustomer {
	constructor(public readonly payload: Customer) {}
	static readonly type = '[Customer] UpdateCustomer';
}

export class UpdateTitle {
	constructor(public readonly payload: { customerId: string; title: Title }) {}
	static readonly type = '[Customer] UpdateTitle';
}
