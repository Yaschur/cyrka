import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Store } from '@ngrx/store';
import { Observable } from 'rxjs/Observable';
import { Jobtype } from '../../models/jobtype';
import { withLatestFrom, switchMap } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';

import { getJobtypeEntities } from '../../jobtype.store';

@Component({
	selector: 'app-jobtype-item',
	templateUrl: './jobtype-item.component.html',
	styleUrls: ['./jobtype-item.component.scss'],
})
export class JobtypeItemComponent {
	jobtype$: Observable<Jobtype>;

	constructor(private route: ActivatedRoute, private store: Store<{}>) {
		this.jobtype$ = store
			.select(getJobtypeEntities)
			.pipe(
				withLatestFrom(route.paramMap),
				switchMap(p => of(p[0].find(jt => jt.id === p[1].get('jobtypeId'))))
			);
	}
}
