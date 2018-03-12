import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { switchMap, map, catchError, filter } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { ROUTER_NAVIGATION, RouterNavigationAction } from '@ngrx/router-store';

import { JobtypeApiService } from '../services/jobtype-api.service';
import {
	JobtypeActionTypes,
	FindJobtypesSuccess,
	FindJobtypesError,
	FindJobtypes,
} from './jobtype.actions';

@Injectable()
export class JobtypeEffects {
	@Effect()
	navigateJobTypes$ = this.actions$.pipe(
		ofType<RouterNavigationAction>(ROUTER_NAVIGATION),
		filter(
			r => r.payload.routerState.root.firstChild.routeConfig.path === 'jobtypes'
		),
		map(() => new FindJobtypes())
	);

	@Effect()
	fetchJobTypes$ = this.actions$.pipe(
		ofType<FindJobtypes>(JobtypeActionTypes.FIND_JOBTYPES),
		switchMap(() => this.apiService.fetchAll()),
		map(res => new FindJobtypesSuccess(res)),
		catchError(() => of(new FindJobtypesError()))
	);

	constructor(
		private actions$: Actions,
		private apiService: JobtypeApiService
	) {}
}
