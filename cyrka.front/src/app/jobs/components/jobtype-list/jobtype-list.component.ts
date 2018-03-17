import { Component } from '@angular/core';

import { map } from 'rxjs/operators';

import { Jobtype } from '../../models/jobtype';
import { Units } from '../../../shared/units/units';

interface JobtypeItem extends Jobtype {
	unitLabel: string;
}

@Component({
	selector: 'app-jobtypes-list',
	templateUrl: './jobtype-list.component.html',
	styleUrls: ['./jobtype-list.component.scss'],
})
export class JobtypeListComponent {
	public jobtypes: JobtypeItem[];

	constructor() {}

	selectJobtypes(jts: Jobtype[]) {
		this.jobtypes = jts.map(
			jt => <JobtypeItem>{ ...jt, unitLabel: Units.getTitle(jt.unit).title }
		);
	}
}
