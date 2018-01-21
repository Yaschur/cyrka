import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

import 'rxjs/add/operator/switchMap';

import { CustomerPlain } from '../models/customer-plain.model';
import { CustomersApiService } from '../services/customers-api.service';

@Component({
	selector: 'app-customers-details',
	templateUrl: './customers-details.component.html',
	styleUrls: ['./customers-details.component.scss']
})
export class CustomersDetailsComponent implements OnInit {
	constructor(
		private _router: Router,
		private _route: ActivatedRoute,
		private _customerApi: CustomersApiService
	) { }

	public customer: CustomerPlain;

	public ngOnInit() {
		this._route.params
			.switchMap(params => this._customerApi.getById(params['id']))
			.subscribe(c => this.customer = c);
	}

}
