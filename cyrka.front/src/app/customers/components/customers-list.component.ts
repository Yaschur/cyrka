import { Component } from '@angular/core';
import { Router } from '@angular/router';

import '@clr/icons/shapes/essential-shapes';

import { CustomersApiService } from '../services/customers-api.service';
import { Observable } from 'rxjs/Observable';
import { CustomerPlain } from '../models/customer-plain.model';

@Component({
	templateUrl: './customers-list.component.html',
	styleUrls: ['./customers-list.component.scss']
})
export class CustomersListComponent {

	constructor(
		private _customerApi: CustomersApiService,
		private _router: Router
	) {
		this.customers = _customerApi.getAll();
	}

	public customers: Observable<CustomerPlain[]>;

	public details(id: string): void {
		this._router.navigate(['customers', id, 'details']);
	}
}
