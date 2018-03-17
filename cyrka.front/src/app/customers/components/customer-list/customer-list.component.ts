import { Component } from '@angular/core';

import { Customer } from '../../models/customer';

@Component({
	selector: 'app-customer-list',
	templateUrl: './customer-list.component.html',
	styleUrls: ['./customer-list.component.scss'],
})
export class CustomerListComponent {
	public customers: Customer[];

	constructor() {}

	selectCustomers(csts: Customer[]) {
		this.customers = csts;
	}
}
