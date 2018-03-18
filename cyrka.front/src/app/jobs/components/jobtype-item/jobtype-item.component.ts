import { Component } from '@angular/core';

import { Jobtype } from '../../models/jobtype';

import { Units } from '../../../shared/units/units';
import { TitleAbbr } from '../../../shared/units/title-abbr';

interface JobtypeItem extends Jobtype {
	unitLabels: TitleAbbr;
}

@Component({
	selector: 'app-jobtype-item',
	templateUrl: './jobtype-item.component.html',
	styleUrls: ['./jobtype-item.component.scss'],
})
export class JobtypeItemComponent {
	jobtype: JobtypeItem;

	constructor() {}

	selectJobtype(jt: Jobtype) {
		this.jobtype = <JobtypeItem>{ ...jt, unitLabels: Units.getTitle(jt.unit) };
	}
}
