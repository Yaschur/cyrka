import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Store } from '@ngrx/store';
import { Observable } from 'rxjs/Observable';
import { Jobtype } from '../../models/jobtype';
import { withLatestFrom, switchMap, map, filter } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';

import { getJobtypeEntities } from '../../jobtype.store';
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
