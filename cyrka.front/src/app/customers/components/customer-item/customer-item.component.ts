import { Component } from '@angular/core';
import { Customer } from '../../models/customer';
import { Title } from '../../models/title';

@Component({
	selector: 'app-customer-item',
	templateUrl: './customer-item.component.html',
	styleUrls: ['./customer-item.component.scss'],
})
export class CustomerItemComponent {
	customer: Customer;
	selectedTitle: Title;

	constructor() {}

	selectCustomer(cst: Customer) {
		this.customer = cst;
	}

	selectTitle(title: Title) {
		this.selectedTitle = title || <Title>{};
	}
}
