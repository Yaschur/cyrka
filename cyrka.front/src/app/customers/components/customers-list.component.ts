import { Component } from '@angular/core';

import 'clarity-icons/shapes/essential-shapes';

import { CustomersApiService } from '../services/customers-api.service';
import { Observable } from 'rxjs/Observable';
import { CustomerPlain } from '../models/customer-plain.model';

@Component({
	templateUrl: './customers-list.component.html',
	styleUrls: ['./customers-list.component.scss']
})
export class CustomersListComponent {

	constructor(private _customerApi: CustomersApiService) {
		this.customers = _customerApi.getAll();
	}

	public customers: Observable<CustomerPlain[]>;
}
