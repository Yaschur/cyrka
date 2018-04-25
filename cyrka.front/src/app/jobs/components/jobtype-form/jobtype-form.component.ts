import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Observable, of } from 'rxjs';

import { Jobtype } from '../../models/jobtype';
import { Units } from '../../../shared/units/units';

@Component({
	selector: 'app-jobtype-form',
	templateUrl: './jobtype-form.component.html',
	styleUrls: ['./jobtype-form.component.scss'],
})
export class JobtypeFormComponent {
	form: FormGroup;
	formTitle: string;
	submitTitle: string;
	units: { key: string; title: string; abbr: string }[];

	jtChanges$: Observable<Jobtype>;

	constructor(private _formBuilder: FormBuilder) {
		this.form = this._formBuilder.group({
			id: '',
			name: ['', Validators.required],
			unit: '',
			rate: ['', Validators.required],
			description: '',
		});
		this.units = Units.all.map(u => {
			const t = Units.getTitle(u);
			return { key: u, title: t.title, abbr: t.abbrevation };
		});
	}

	selectJobtype(jt: Jobtype) {
		this.form.setValue({
			id: jt.id || '',
			name: jt.name || '',
			unit: jt.unit || Units.Undefined,
			rate: jt.rate || 0.0,
			description: jt.description || '',
		});
		this.formTitle = jt.id
			? 'изменение данных услуги'
			: 'добавление новой услуги';
		this.submitTitle = jt.id ? 'изменить' : 'добавить';
	}
	save() {
		if (this.form.invalid || this.form.pristine) {
			return;
		}
		this.jtChanges$ = of(this.form.value);
	}
	cancel() {
		this.jtChanges$ = of();
	}
}
