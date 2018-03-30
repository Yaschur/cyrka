import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { JobSet } from '../../models/job-set';
import { TitleAbbr } from '../../../shared/units/title-abbr';
import { Units } from '../../../shared/units/units';
import { Store } from '@ngrx/store';
import { ChangeJob } from '../../store/project.actions';

interface JobItem extends JobSet {
	unitDef: TitleAbbr;
}

@Component({
	selector: 'div[app-project-job-form]',
	templateUrl: './project-job-form.component.html',
	styleUrls: ['./project-job-form.component.scss'],
})
export class ProjectJobFormComponent {
	@Input()
	set job(j: JobItem) {
		if (j) {
			this._job = <JobItem>{ ...j, unitDef: Units.getTitle(j.unitName) };
			this.form.setValue({
				amount: j.amount,
				ratePerUnit: j.ratePerUnit,
			});
		}
	}
	get job(): JobItem {
		return this._job;
	}

	@Output() closeJobForm: EventEmitter<void>;

	form: FormGroup;

	private _job: JobItem;

	constructor(private _formBuilder: FormBuilder, private _store: Store<{}>) {
		this.closeJobForm = new EventEmitter();
		this.form = this._formBuilder.group({
			amount: [null, Validators.required],
			ratePerUnit: [null, Validators.required],
		});
	}

	save() {
		if (this.form.invalid) {
			return;
		}
		this._store.dispatch(
			new ChangeJob({
				amount: this.form.get('amount').value,
				jobTypeId: this._job.jobTypeId,
				jobTypeName: this._job.jobTypeName,
				ratePerUnit: this.form.get('ratePerUnit').value,
				unitName: this._job.unitName,
			})
		);
		this.closeJobForm.emit();
	}
}
