import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { switchMap, map, catchError, tap, filter } from 'rxjs/operators';
import { pipe } from 'rxjs/Rx';
import { of } from 'rxjs/observable/of';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { ROUTER_NAVIGATION, RouterNavigationAction } from '@ngrx/router-store';

import { JobTypesApiService } from '../services/jobtypes-api.service';
import {
	JobTypesActionTypes,
	FindJobtypesSuccess,
	FindJobtypesError,
	FindJobtypes,
} from './job-types.actions';

@Injectable()
export class JobTypesEffects {
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
		ofType<FindJobtypes>(JobTypesActionTypes.FIND_JOBTYPES),
		switchMap(() => this.apiService.fetchAll()),
		map(res => new FindJobtypesSuccess(res)),
		catchError(() => of(new FindJobtypesError()))
	);
	
	constructor(
		private actions$: Actions,
		private apiService: JobTypesApiService
	) {}
}
