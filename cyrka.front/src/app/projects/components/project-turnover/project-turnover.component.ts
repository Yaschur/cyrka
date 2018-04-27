import { Component, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { Store } from '@ngxs/store';

import { Project } from '../../models/project';
import { SetPayments } from '../../store/project.actions';

@Component({
	selector: 'div[app-project-turnover]',
	templateUrl: './project-turnover.component.html',
	styleUrls: ['./project-turnover.component.scss'],
})
export class ProjectTurnoverComponent {
	@Input() project: Project;

	paymentsEditMode: boolean;
	form: FormGroup;

	constructor(private _formBuilder: FormBuilder, private _store: Store) {
		this.paymentsEditMode = false;
	}

	editPayments() {
		this.form = this._formBuilder.group({
			editorPayment: [0, Validators.required],
			translatorPayment: [0, Validators.required],
		});
		this.paymentsEditMode = true;
		if (this.project.payments) {
			this.form.setValue({
				editorPayment: this.project.payments.editorPayment,
				translatorPayment: this.project.payments.translatorPayment,
			});
		}
	}

	savePayments() {
		if (this.form.invalid) {
			return;
		}
		if (this.form.dirty) {
			this._store.dispatch(new SetPayments(this.form.value));
		}
		this.viewPayments();
	}
	viewPayments() {
		this.paymentsEditMode = false;
		this.form = null;
	}
}
