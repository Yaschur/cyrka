import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { CustomersApiService } from '../services/customers-api.service';
import { CustomerPlain } from '../models/customer-plain.model';

@Component({
	selector: 'app-titles-add',
	templateUrl: './titles-add.component.html'
})
export class TitlesAddComponent implements OnInit {

	constructor(
		private _formBuilder: FormBuilder,
		private _customerApi: CustomersApiService,
	) {
		this.done = new EventEmitter<boolean>();
	}

	@Input()
	public customer: CustomerPlain;

	@Output()
	public done: EventEmitter<boolean>;

	public form: FormGroup;

	public ngOnInit() {
		this.form = this._formBuilder.group({
			'name': ['', Validators.required],
			'numberOfSeries': 1,
			'description': ''
		});
	}

	public onSubmit() {
		if (this.form.invalid) {
			return;
		}
		this._customerApi.addTitle(this.customer.id, this.form.value)
			.subscribe(() => {
				this.customer.titles.unshift(this.form.value);
				this.return();
			});
	}

	public return() {
		this.done.emit(true);
	}
}
