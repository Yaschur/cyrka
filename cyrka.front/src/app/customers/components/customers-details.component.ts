import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

import 'rxjs/add/operator/switchMap';

import { CustomerPlain } from '../models/customer-plain.model';
import { CustomersApiService } from '../services/customers-api.service';

@Component({
	selector: 'app-customers-details',
	templateUrl: './customers-details.component.html'
})
export class CustomersDetailsComponent implements OnInit {
	constructor(
		private _router: Router,
		private _route: ActivatedRoute,
		private _customerApi: CustomersApiService
	) {
		this.noEdit = true;
	}

	public customer: CustomerPlain;
	public noEdit: boolean;

	public ngOnInit() {
		this._route.params
			.switchMap(params => this._customerApi.getById(params['customerId']))
			.subscribe(c => this.customer = c);
	}

	public addTitle() {
		this.noEdit = false;
	}
	public doneTitle(done: boolean) {
		this.noEdit = done;
	}

}
