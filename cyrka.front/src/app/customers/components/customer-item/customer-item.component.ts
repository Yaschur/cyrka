import { Component } from '@angular/core';
import { Customer } from '../../models/customer';

@Component({
	selector: 'app-customer-item',
	templateUrl: './customer-item.component.html',
	styleUrls: ['./customer-item.component.scss'],
})
export class CustomerItemComponent {
	customer: Customer;

	constructor() {}

	selectCustomer(cst: Customer) {
		this.customer = cst;
	}
}
