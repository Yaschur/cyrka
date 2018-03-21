import { Component, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
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
	}

	save() {}
	cancel() {}

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
