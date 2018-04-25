import { Component, Input } from '@angular/core';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Store } from '@ngrx/store';

import { JobSet } from '../../models/job-set';
import { Jobtype } from '../../models/job-type';
import { getJobtypeEntities } from '../../project.store';
import { FindJobtypes } from '../../store/project.actions';

@Component({
	selector: 'div[app-project-job-list]',
	templateUrl: './project-job-list.component.html',
	styleUrls: ['./project-job-list.component.scss'],
})
export class ProjectJobListComponent {
	@Input() jobs: JobSet[];

	editId: string;
	availableJobtypes$: Observable<Jobtype[]>;
	readonly NEW_JOB_ID = '___NEW___';

	constructor(private _store: Store<{}>) {
		this.clearEditMode();
	}

	setEditModeTo(id: string) {
		this.editId = id;
		if (id === this.NEW_JOB_ID) {
			this.availableJobtypes$ = this._store
				.select(getJobtypeEntities)
				.pipe(
					map(jts =>
						jts.filter(jt => !this.jobs.some(j => j.jobTypeId === jt.id))
					)
				);
			this._store.dispatch(new FindJobtypes());
		}
	}
	clearEditMode() {
		this.editId = '';
	}
}
