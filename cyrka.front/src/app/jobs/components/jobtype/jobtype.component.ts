import { Component, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { Store } from '@ngrx/store';

import { Jobtype } from '../../models/jobtype';
import { getJobtypeEntities } from '../../jobtype.store';
import { withLatestFrom, switchMap, filter, map } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';

@Component({
	selector: 'app-jobtype',
	templateUrl: './jobtype.component.html',
	styleUrls: ['./jobtype.component.scss'],
})
export class JobtypeComponent {
	@Output() jobTypeItem$: Observable<Jobtype>;
	@Output() jobTypeItems$: Observable<Jobtype[]>;

	constructor(private route: ActivatedRoute, private store: Store<{}>) {
		this.jobTypeItem$ = store
			.select(getJobtypeEntities)
			.pipe(
				withLatestFrom(route.paramMap),
				switchMap(p =>
					of((p[1].has('jobtypeId')
						? p[0].find(jt => jt.id === p[1].get('jobtypeId')) || {}
						: {}) as Jobtype)
				),
				filter(jt => jt != null)
			);

		this.jobTypeItems$ = store.select(getJobtypeEntities);
	}
}
