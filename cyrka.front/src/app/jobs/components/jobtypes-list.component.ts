import { Component, OnInit } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operators';
import { pipe } from 'rxjs/Rx';
import { Store } from '@ngrx/store';

import { JobTypesApiService } from '../services/jobtypes-api.service';
import { UnitDescriptor } from '../../shared/units/unit-descriptor';
import { UnitService } from '../../shared/units/unit.service';
import { JobType } from '../models/job-type';
import { JobTypesState } from '../store/job-types.reducers';
import { getJobTypeEntities } from '../job-types.store';
import { FindJobtypes } from '../store/job-types.actions';
import { Units } from '../../shared/units/units';

interface JobTypeItem extends JobType {
	unitLabel: string;
}

@Component({
	selector: 'app-jobtypes-list',
	templateUrl: './jobtypes-list.component.html',
	styleUrls: ['./jobtypes-list.component.scss'],
})
export class JobTypesListComponent implements OnInit {
	public jobTypes$: Observable<JobTypeItem[]>;

	constructor(private _store: Store<JobTypesState>) {
		this.jobTypes$ = _store.select(getJobTypeEntities).pipe(
			map(jts =>
				jts.map(
					jt =>
						<JobTypeItem>{
							...jt,
							unitLabel: Units.getTitle(jt.unit).title,
						}
				)
			)
		);
	}

	ngOnInit() {
		// this._store.dispatch(new FindJobtypes());
	}
}
