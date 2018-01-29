import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { CustomersApiService } from '../services/customers-api.service';
import { CustomerPlain } from '../models/customer-plain.model';
import { TitlePlain } from '../models/title-plain.model';

@Component({
	selector: 'app-titles-form',
	templateUrl: './titles-form.component.html'
})
export class TitlesFormComponent implements OnInit {

	constructor(
		private _formBuilder: FormBuilder
	) {
		this.close = new EventEmitter();
		this.save = new EventEmitter();
	}

	@Input()
	public title: TitlePlain;

	@Output()
	public save: EventEmitter<void>;
	@Output()
	public close: EventEmitter<void>;

	public form: FormGroup;
	public formTitle: string;
	public submitTitle: string;

	public ngOnInit() {
		this.form = this._formBuilder.group({
			'name': [this.title.name, Validators.required],
			'numberOfSeries': this.title.numberOfSeries,
			'description': this.title.description
		});
		this.formTitle = this.title.id ? 'изменение данных продукта' : 'добавление нового продукта';
		this.submitTitle = this.title.id ? 'изменить' : 'добавить';
	}

	public onSubmit() {
		this.form.updateValueAndValidity();
		if (this.form.invalid || this.form.pristine) {
			return;
		}
		this.title.name = this.form.value['name'];
		this.title.numberOfSeries = this.form.value['numberOfSeries'];
		this.title.description = this.form.value['description'];
		this.save.emit();
	}

	public onClose() {
		this.close.emit();
	}
}
