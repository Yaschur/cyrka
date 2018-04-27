import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { Observable } from 'rxjs';
import { Store } from '@ngxs/store';

import { JobSet } from '../../models/job-set';
import { TitleAbbr } from '../../../shared/units/title-abbr';
import { Units } from '../../../shared/units/units';
import { ChangeJob, SetJob } from '../../store/project.actions';
import { Jobtype } from '../../models/job-type';

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
		if (!j) {
			return;
		}
		this.newJobMode = this.newJobMode || !j.jobTypeId;
		this._job = <JobItem>{ ...j, unitDef: Units.getTitle(j.unitName) };
		this.form.patchValue({
			amount: j.amount || null,
			ratePerUnit: j.ratePerUnit || null,
		});
		if (this.newJobMode && !this.form.get('jobtype')) {
			this.form.addControl(
				'jobtype',
				this._formBuilder.control(null, Validators.required)
			);
			this.form.get('jobtype').valueChanges.subscribe((jt: Jobtype) => {
				this.job = <JobItem>{
					jobTypeId: jt ? jt.id : null,
					jobTypeName: jt ? jt.name : null,
					unitName: jt.unit,
					ratePerUnit: jt.rate,
				};
			});
		}
	}
	get job(): JobItem {
		return this._job;
	}

	@Input() jobTypes$: Observable<Jobtype[]>;

	@Output() closeJobForm: EventEmitter<void>;

	form: FormGroup;
	newJobMode: boolean;

	constructor(private _formBuilder: FormBuilder, private _store: Store) {
		this.newJobMode = false;
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
			this.newJobMode
				? new SetJob({
						amount: this.form.get('amount').value,
						jobTypeId: this._job.jobTypeId,
						jobTypeName: this._job.jobTypeName,
						ratePerUnit: this.form.get('ratePerUnit').value,
						unitName: this._job.unitName,
				  })
				: new ChangeJob({
						amount: this.form.get('amount').value,
						jobTypeId: this._job.jobTypeId,
						jobTypeName: this._job.jobTypeName,
						ratePerUnit: this.form.get('ratePerUnit').value,
						unitName: this._job.unitName,
				  })
		);
		this.closeJobForm.emit();
	}

	private _job: JobItem;
}
