import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { Store } from '@ngrx/store';

import { ProductSet } from '../../models/product-set';
import { Customer } from '../../models/customer';
import { Title } from '../../models/title';
import { getCustomerEntities } from '../../project.store';
import { FindCustomers } from '../../store/project.actions';
import {
	filter,
	switchMap,
	merge,
	map,
	switchMapTo,
	mergeMap,
} from 'rxjs/operators';

@Component({
	selector: 'app-project-product-form',
	templateUrl: './project-product-form.component.html',
	styleUrls: ['./project-product-form.component.scss'],
})
export class ProjectProductFormComponent {
	@Input() productSet: ProductSet;

	get numberOfSeries() {
		if (this.form && this.form.get('title').value) {
			return (<Title>this.form.get('title').value).numberOfSeries;
		}
		return null;
	}
	form: FormGroup;
	customers$: Observable<Customer[]>;
	titles$: Observable<Title[]>;

	constructor(private _formBuilder: FormBuilder, private _store: Store<{}>) {
		// Ask for customers in state
		this._store.dispatch(new FindCustomers());
		// Observe all customers
		this.customers$ = this._store.select(getCustomerEntities);
		// Build form
		this.form = this._formBuilder.group({
			customer: [null, Validators.required],
			title: [null, Validators.required],
			episodeNumber: [null, Validators.required],
			episodeDuration: [null, Validators.required],
		});
		// Observe all titles
		this.titles$ = this.form
			.get('customer')
			.valueChanges.pipe(map(c => (c ? (<Customer>c).titles : [])));

		// Set form values
		this.customers$.subscribe(csts => {
			const patch = {
				customer: <Customer>null,
				title: <Title>null,
				episodeNumber: null,
				episodeDuration: null,
			};
			if (this.productSet && this.productSet.customerId) {
				patch.customer = csts.find(c => c.id === this.productSet.customerId);
				patch.title = this.productSet.titleId
					? patch.customer.titles.find(t => t.id === this.productSet.titleId)
					: null;
			}
			if (this.productSet) {
				patch.episodeNumber = this.productSet.episodeNumber || null;
				patch.episodeDuration = this.productSet.episodeDuration || null;
			}
			this.form.patchValue(patch);
		});
		this.titles$.subscribe(t => this.form.patchValue({ title: null }));
	}
}
