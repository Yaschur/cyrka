import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { JobTypePlain } from '../models/jobtype-plain';
import { JobTypesApiService } from '../services/jobtypes-api.service';
import { Observable } from 'rxjs/Observable';

@Component({
	selector: 'app-jobtypes-form',
	templateUrl: './jobtypes-form.component.html',
	styleUrls: ['./jobtypes-form.component.scss']
})
export class JobTypesFormComponent implements OnInit {

	constructor(
		private _formBuilder: FormBuilder,
		private _api: JobTypesApiService,
		private _route: ActivatedRoute,
		private _router: Router
	) {
		this.form = this._formBuilder.group({
			'name': ['', Validators.required],
			'unit': '',
			'rate': ['', Validators.required],
			'description': ''
		});
	}

	public form: FormGroup;
	public formTitle: string;
	public submitTitle: string;

	public units: { key: string, value: string }[] = [
		{ key: 'Undefined', value: 'Не определено' },
		{ key: 'Piece', value: 'Штука/ единица' },
		{ key: 'Minute', value: 'Минута' },
		{ key: 'Character', value: 'Персонаж' },
		{ key: 'Symbol', value: 'Символ (текста)' },
		{ key: 'Gigabyte', value: 'Гигабайт' }
	];

	public ngOnInit() {
		this._route.params
			.switchMap(p => p['jobTypeId'] ? this._api.getById(p['jobTypeId']) : Observable.of(<JobTypePlain>{}))
			.subscribe(jt => {
				this.form.setValue({
					'name': jt.name || '',
					'unit': jt.unit || this.units[0].key,
					'rate': jt.rate || 0.00,
					'description': jt.description || ''
				});
				this._id = jt.id;
				this.formTitle = this._id ? 'изменение данных услуги' : 'добавление новой услуги';
				this.submitTitle = this._id ? 'изменить' : 'добавить';
			});
	}

	public onSave() {
		this.form.updateValueAndValidity();
		if (this.form.invalid || this.form.pristine) {
			return;
		}
		(this._id ? this._api.change(this._id, this.form.value) : this._api.register(this.form.value))
			.subscribe(() => this.onCancel());
	}

	public onCancel() {
		this._router.navigate(['..'], { relativeTo: this._route });
	}

	private _id: string;
}
