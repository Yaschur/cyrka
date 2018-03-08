import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { CustomerDefinition } from '../models/customer-definition';

@Component({
	selector: 'app-customers-form',
	templateUrl: './customers-form.component.html',
})
export class CustomerFormComponent implements OnInit {
	@Input() public customer: CustomerDefinition;

	@Output() public save: EventEmitter<void>;
	@Output() public close: EventEmitter<void>;

	public form: FormGroup;
	public formTitle: string;
	public submitTitle: string;

	constructor(private _formBuilder: FormBuilder) {
		this.close = new EventEmitter();
		this.save = new EventEmitter();
	}

	public ngOnInit() {
		this.form = this._formBuilder.group({
			name: [this.customer.name, Validators.required],
			description: this.customer.description,
		});
		this.formTitle = this.customer.id
			? 'изменение данных заказчика'
			: 'регистрация нового заказчика';
		this.submitTitle = this.customer.id ? 'изменить' : 'зарегистрировать';
	}

	public onSubmit() {
		this.form.updateValueAndValidity();
		if (this.form.invalid || this.form.pristine) {
			return;
		}
		this.customer.name = this.form.value['name'];
		this.customer.description = this.form.value['description'];
		this.save.emit();
	}

	public onClose() {
		this.close.emit();
	}
}
