import { Component, Output, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { Store } from '@ngrx/store';

import { Jobtype } from '../../models/jobtype';
import { getJobtypeEntities } from '../../jobtype.store';
import { withLatestFrom, switchMap, filter, map } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { UpdateJobtype } from '../../store/jobtype.actions';

@Component({
	selector: 'app-jobtype',
	templateUrl: './jobtype.component.html',
	styleUrls: ['./jobtype.component.scss'],
})
export class JobtypeComponent {
	@Output() jobTypeItem_read$: Observable<Jobtype>;
	@Output() jobTypeItems_read$: Observable<Jobtype[]>;

	@Input()
	set jobTypeItem_write$(jts: Observable<Jobtype>) {
		if (!jts) {
			return;
		}
		jts.subscribe({
			next: jt => this._store.dispatch(new UpdateJobtype(jt)),
			complete: () =>
				this._router.navigate(['..'], { relativeTo: this._route }),
		});
	}

	constructor(
		private _route: ActivatedRoute,
		private _router: Router,
		private _store: Store<{}>
	) {
		this.jobTypeItem_read$ = _store
			.select(getJobtypeEntities)
			.pipe(
				withLatestFrom(_route.paramMap),
				switchMap(p =>
					of((p[1].has('jobtypeId')
						? p[0].find(jt => jt.id === p[1].get('jobtypeId')) || {}
						: {}) as Jobtype)
				),
				filter(jt => jt != null)
			);

		this.jobTypeItems_read$ = _store.select(getJobtypeEntities);
	}
}
