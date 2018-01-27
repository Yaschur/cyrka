import { Component } from '@angular/core';
import { Location } from '@angular/common';

import { CustomersApiService } from '../services/customers-api.service';
import { CustomerDefinition } from '../models/customer-definition.model';

@Component({
	selector: 'app-customers-register',
	templateUrl: './customers-register.component.html'
})
export class CustomersRegisterComponent {

	constructor(
		private _customerApi: CustomersApiService,
		private _location: Location
	) {
		this.customer = <CustomerDefinition>{};
	}

	public customer: CustomerDefinition;

	public onSubmit() {
		this._customerApi.register(this.customer)
			.subscribe(() => {
				this.onClose();
			});
	}

	public onClose() {
		this._location.back();
	}
}
