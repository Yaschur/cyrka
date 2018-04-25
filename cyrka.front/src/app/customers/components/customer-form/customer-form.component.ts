import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { Observable, of } from 'rxjs';

import { Customer } from '../../models/customer';

@Component({
	selector: 'app-customer-form',
	templateUrl: './customer-form.component.html',
	styleUrls: ['./customer-form.component.scss'],
})
export class CustomerFormComponent {
	form: FormGroup;
	formTitle: string;
	submitTitle: string;

	cstChanges$: Observable<Customer>;

	constructor(private _formBuilder: FormBuilder) {
		this.form = this._formBuilder.group({
			id: '',
			name: ['', Validators.required],
			description: '',
		});
	}

	selectCustomer(cst: Customer) {
		this.form.setValue({
			id: cst.id || '',
			name: cst.name || '',
			description: cst.description || '',
		});
		this.formTitle = cst.id
			? 'изменение данных заказчика'
			: 'добавление нового заказчика';
		this.submitTitle = cst.id ? 'изменить' : 'добавить';
	}
	save() {
		if (this.form.invalid || this.form.pristine) {
			return;
		}
		this.cstChanges$ = of(this.form.value);
	}
	cancel() {
		this.cstChanges$ = of();
	}
}
