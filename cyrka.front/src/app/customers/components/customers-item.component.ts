import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/observable/of';

import { CustomersApiService } from '../services/customers-api.service';
import { CustomerDefinition } from '../models/customer-definition.model';

enum ItemMode {
	Details = 'details',
	Change = 'change'
}

@Component({
	selector: 'app-customers-item',
	templateUrl: './customers-item.component.html',
	styleUrls: ['./customers-item.component.scss']
})
export class CustomersItemComponent implements OnInit {

	constructor(
		private _location: Location,
		private _router: Router,
		private _route: ActivatedRoute,
		private _customerApi: CustomersApiService
	) { }

	public mode: ItemMode;
	public customerDefinition: CustomerDefinition;

	public get inDetailsMode(): boolean {
		return this.mode === ItemMode.Details;
	}
	public get inChangeMode(): boolean {
		return this.mode === ItemMode.Change;
	}

	public ngOnInit() {
		console.log('item init');
		this._route.params
			.switchMap(p => Observable.of(<ItemMode>p['mode'] || ItemMode.Details))
			.distinctUntilChanged()
			.subscribe(mode => {
				this.mode = mode;
				console.log(`mode change subscription: ${mode}`);
			});
		this._route.params
			.switchMap(p => Observable.of(<string>p['customerId']))
			.distinctUntilChanged()
			.switchMap(id => this._customerApi.getById(id))
			.subscribe(c => {
				this.customerDefinition = <CustomerDefinition>{ id: c.id, name: c.name, description: c.description };
				console.log(`customer change subscription: ${c.id}`);
			});
	}

	public onBack() {
		this._location.back();
	}

	public onChange() {
		this._router.navigate([ItemMode.Change], { relativeTo: this._route.parent });
	}

	// public toggle() {
	// 	const m = this.mode === ItemMode.Details ? ItemMode.Change : ItemMode.Details;
	// 	this._router.navigate([m], { relativeTo: this._route.parent });
	// }
	// public toggleCustomer() {
	// 	const id = this.customerDefinition.id === '52c452bc-7009-4ec3-9282-5ce5f00299c0' ? '9f319df9-7452-4f39-bdfe-e1244cf2d060'
	// 		: '52c452bc-7009-4ec3-9282-5ce5f00299c0';
	// 	this._router.navigate([id, this.mode], { relativeTo: this._route.parent.parent });
	// }
}
