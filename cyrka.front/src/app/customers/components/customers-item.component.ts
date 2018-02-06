import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/observable/of';

import { CustomersApiService } from '../services/customers-api.service';
import { CustomerDefinition } from '../models/customer-definition';
import { TitlePlain } from '../models/title-plain';

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
	public titles: TitlePlain[];
	public titleToEdit: TitlePlain;

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
				this.titles = c.titles;
				console.log(`customer change subscription: ${c.id}`);
			});
	}

	public onBack() {
		this._location.back();
	}

	public onChange() {
		this._router.navigate([ItemMode.Change], { relativeTo: this._route.parent });
	}

	public onSave() {
		this._customerApi.change(this.customerDefinition.id, this.customerDefinition)
			.subscribe(() => this.onBack());
	}

	public onRetire() {
		this._customerApi.retire(this.customerDefinition.id)
			.subscribe(() => this._router.navigate(['customers']));
	}

	public onNewTitle() {
		this.titleToEdit = <TitlePlain>{ numberOfSeries: 1 };
	}

	public onTitleSelect(title: TitlePlain) {
		this.titleToEdit = title;
	}

	public onTitleFormClose() {
		this.titleToEdit = null;
	}

	public onTitleFormSave() {
		(this.titleToEdit.id ?
			this._customerApi.changeTitle(this.customerDefinition.id, this.titleToEdit.id, this.titleToEdit)
			: this._customerApi.addTitle(this.customerDefinition.id, this.titleToEdit)
		).subscribe(() => {
			this.titleToEdit = null;
			this.reloadTitles();
		});
	}

	public onTitleFormDelete() {
		this._customerApi.removeTitle(this.customerDefinition.id, this.titleToEdit.id)
			.subscribe(() => {
				this.titleToEdit = null;
				this.reloadTitles();
			});
	}

	private reloadTitles() {
		this._customerApi.getById(this.customerDefinition.id)
			.subscribe(c => this.titles = c.titles);
	}
}
