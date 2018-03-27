import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';

import { ProductSet } from '../../models/product-set';
import { Customer } from '../../models/customer';
import { Title } from '../../models/title';

@Component({
	selector: 'app-project-product-form',
	templateUrl: './project-product-form.component.html',
	styleUrls: ['./project-product-form.component.scss'],
})
export class ProjectProductFormComponent {
	@Input() productSet: ProductSet;

	form: FormGroup;
	customers$: Observable<Customer[]>;
	titles$: Observable<Title[]>;

	constructor(private _formBuilder: FormBuilder) {
		// this.customers$ = of();
		this.form = this._formBuilder.group({
			customer: [null, Validators.required],
			title: [null, Validators.required],
			episodeNumber: [null, Validators.required],
			episodeDuration: [null, Validators.required],
		});
	}
}
