import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
	selector: 'app-customers-register',
	templateUrl: './customers-register.component.html',
	styleUrls: ['./customers-register.component.scss']
})
export class CustomersRegisterComponent implements OnInit {

	constructor(
		private _formBuilder: FormBuilder
	) { }

	public form: FormGroup;

	public ngOnInit() {
		this.form = this._formBuilder.group({
			'name': ['', Validators.required],
			'description': ''
		});
	}

	public onSubmit() {

	}
}
