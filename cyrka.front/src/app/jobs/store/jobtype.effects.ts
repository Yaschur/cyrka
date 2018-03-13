import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import {
	switchMap,
	map,
	catchError,
	filter,
	withLatestFrom,
} from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { Store } from '@ngrx/store';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { ROUTER_NAVIGATION, RouterNavigationAction } from '@ngrx/router-store';

import { JobtypeApiService } from '../services/jobtype-api.service';
import {
	JobtypeActionTypes,
	FindJobtypesSuccess,
	FindJobtypesError,
	FindJobtypes,
} from './jobtype.actions';
import { Jobtype } from '../models/jobtype';
import { JobtypeState } from './jobtype.reducers';

@Injectable()
export class JobtypeEffects {
	@Effect()
	navigateJobTypes$ = this._actions$.pipe(
		ofType<RouterNavigationAction>(ROUTER_NAVIGATION),
		filter(r =>
			r.payload.routerState.root.firstChild.routeConfig.path.startsWith(
				'jobtypes'
			)
		),
		withLatestFrom(this._store$),
		filter(s => !s[1].jobtype.loaded),
		map(b => new FindJobtypes())
	);

	@Effect()
	fetchJobTypes$ = this._actions$.pipe(
		ofType<FindJobtypes>(JobtypeActionTypes.FIND_JOBTYPES),
		switchMap(() => this._apiService.fetchAll()),
		map(res => new FindJobtypesSuccess(res)),
		catchError(() => of(new FindJobtypesError()))
	);

	constructor(
		private _actions$: Actions,
		private _store$: Store<{ jobtype: JobtypeState }>,
		private _apiService: JobtypeApiService
	) {}
}
