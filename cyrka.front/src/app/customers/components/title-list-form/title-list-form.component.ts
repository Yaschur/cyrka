import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { Observable, of } from 'rxjs';

import { Title } from '../../models/title';

@Component({
	selector: 'app-title-list-form',
	templateUrl: './title-list-form.component.html',
	styleUrls: ['./title-list-form.component.scss'],
})
export class TitleListFormComponent {
	@Input()
	set title(t: Title) {
		this.setValue(t);
	}

	@Output() titleChanged: EventEmitter<Title>;
	@Output() editCancelled: EventEmitter<void>;

	public form: FormGroup;
	public submitTitle: string;

	constructor(private _formBuilder: FormBuilder) {
		this.form = this._formBuilder.group({
			id: '',
			name: ['', Validators.required],
			numberOfSeries: 1,
			description: '',
		});
		this.setValue(<Title>{});
		this.titleChanged = new EventEmitter();
		this.editCancelled = new EventEmitter();
	}

	save() {
		if (this.form.invalid || this.form.pristine) {
			return;
		}
		this.titleChanged.emit(this.form.value);
	}
	cancel() {
		this.editCancelled.emit();
	}

	private setValue(title: Title) {
		const t = title || <Title>{};
		this.form.setValue({
			id: t.id || '',
			name: t.name || '',
			numberOfSeries: t.numberOfSeries || 1,
			description: t.description || '',
		});
		this.submitTitle = t.id ? 'изменить' : 'добавить';
	}
}
