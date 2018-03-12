import { Component, OnInit } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operators';
import { Store } from '@ngrx/store';

import { JobtypeApiService } from '../../services/jobtype-api.service';
import { UnitDescriptor } from '../../../shared/units/unit-descriptor';
import { UnitService } from '../../../shared/units/unit.service';
import { Jobtype } from '../../models/jobtype';
import { JobtypeState } from '../../store/jobtype.reducers';
import { getJobtypeEntities } from '../../jobtype.store';
import { FindJobtypes } from '../../store/jobtype.actions';
import { Units } from '../../../shared/units/units';

interface JobtypeItem extends Jobtype {
	unitLabel: string;
}

@Component({
	selector: 'app-jobtypes-list',
	templateUrl: './jobtype-list.component.html',
	styleUrls: ['./jobtype-list.component.scss'],
})
export class JobtypeListComponent implements OnInit {
	public jobtypes$: Observable<JobtypeItem[]>;

	constructor(private _store: Store<JobtypeState>) {
		this.jobtypes$ = _store.select(getJobtypeEntities).pipe(
			map(jts =>
				jts.map(
					jt =>
						<JobtypeItem>{
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
