import { Component, Input, Output, EventEmitter } from '@angular/core';

import { JobSet } from '../../models/job-set';
import { TitleAbbr } from '../../../shared/units/title-abbr';
import { Units } from '../../../shared/units/units';

interface JobItem extends JobSet {
	unitDef: TitleAbbr;
}

@Component({
	selector: 'div[app-project-job]',
	templateUrl: './project-job.component.html',
	styleUrls: ['./project-job.component.scss'],
})
export class ProjectJobComponent {
	@Input()
	set job(j: JobItem) {
		if (j) this._job = <JobItem>{ ...j, unitDef: Units.getTitle(j.unitName) };
	}
	get job(): JobItem {
		return this._job;
	}

	@Output() editJob: EventEmitter<void>;

	constructor() {
		this.editJob = new EventEmitter();
	}

	private _job: JobItem;
}
