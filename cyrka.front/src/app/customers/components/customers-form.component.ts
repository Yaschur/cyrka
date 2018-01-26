import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { CustomerDefinition } from '../models/customer-definition.model';

@Component({
	selector: 'app-customers-change',
	templateUrl: './customers-change.component.html'
})
export class CustomerFormComponent implements OnInit {

	constructor(
		private _formBuilder: FormBuilder
	) {
		this.close = new EventEmitter();
		this.submit = new EventEmitter();
	}

	@Input()
	public customer: CustomerDefinition;

	@Output()
	public submit: EventEmitter<void>;
	@Output()
	public close: EventEmitter<void>;

	public form: FormGroup;
	public formTitle: string;

	public ngOnInit() {
		this.form = this._formBuilder.group({
			'name': [this.customer.name, Validators.required],
			'description': this.customer.description
		});
		this.formTitle = this.customer.id ? 'регистрация нового заказчика' : 'изменение данных заказчика';
	}

	public onSubmit() {
		this.form.updateValueAndValidity();
		if (this.form.invalid) {
			// this.form.markAsDirty()
			return;
		}
		if (!this.form.pristine) {
			this.customer.name = this.form.value['name'];
			this.customer.description = this.form.value['description'];
			this.submit.emit();
		}
		this.close.emit();
	}

	public onCancel() {
		this.close.emit();
	}
}
