import { Component, Input, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';

import { Observable, of } from 'rxjs';
import { filter, map, take, concatMap } from 'rxjs/operators';
import { Store } from '@ngrx/store';

import { ProductSet } from '../../models/product-set';
import { Customer } from '../../models/customer';
import { Title } from '../../models/title';
import { getCustomerEntities, getProjectEntity } from '../../project.store';
import {
	FindCustomers,
	SetProduct,
	CreateProject,
} from '../../store/project.actions';

@Component({
	selector: 'app-project-product-form',
	templateUrl: './project-product-form.component.html',
	styleUrls: ['./project-product-form.component.scss'],
})
export class ProjectProductFormComponent {
	@Input() productSet: ProductSet;

	@Output() closeProductForm: EventEmitter<any>;

	get numberOfSeries() {
		if (this.form && this.form.get('title').value) {
			return (<Title>this.form.get('title').value).numberOfSeries;
		}
		return null;
	}
	form: FormGroup;
	customers$: Observable<Customer[]>;
	titles$: Observable<Title[]>;

	constructor(
		private _formBuilder: FormBuilder,
		private _store: Store<{}>,
		private _location: Location
	) {
		this.closeProductForm = new EventEmitter();
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

	saveProduct() {
		if (this.form.invalid) {
			return;
		}
		if (this.form.dirty) {
			const cust = <Customer>this.form.get('customer').value;
			const title = <Title>this.form.get('title').value;
			const productSet = <ProductSet>{
				customerId: cust.id,
				customerName: cust.name,
				titleId: title.id,
				titleName: title.name,
				episodeNumber: this.form.get('episodeNumber').value,
				episodeDuration: this.form.get('episodeDuration').value,
				totalEpisodes: title.numberOfSeries,
			};
			this._store
				.select(getProjectEntity)
				.pipe(
					concatMap(p => {
						if (p && p.id) {
							return of(p);
						} else {
							this._store.dispatch(new CreateProject());
							return this._store.select(getProjectEntity);
						}
					}),
					filter(p => p && !!p.id),
					take(1)
				)
				.subscribe(() => this._store.dispatch(new SetProduct(productSet)));
		}
		this.closeProductForm.emit();
	}
	cancel() {
		if (!this.productSet) {
			this._location.back();
			return;
		}
		this.closeProductForm.emit();
	}
}
