import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

import { CustomersApiService } from '../services/customers-api.service';

@Component({
	selector: 'app-customers-register',
	templateUrl: './customers-register.component.html'
})
export class CustomersRegisterComponent implements OnInit {

	constructor(
		private _formBuilder: FormBuilder,
		private _customerApi: CustomersApiService,
		private _route: ActivatedRoute,
		private _location: Location
	) { }

	public form: FormGroup;

	public ngOnInit() {
		this.form = this._formBuilder.group({
			'name': ['', Validators.required],
			'description': ''
		});
	}

	public onSubmit() {
		if (this.form.invalid) {
			return;
		}
		this._customerApi.register(this.form.value)
			.subscribe(() => this.return());
	}

	public return() {
		this._location.back();
	}
}
