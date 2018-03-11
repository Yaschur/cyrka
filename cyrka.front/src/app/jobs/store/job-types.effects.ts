import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { switchMap, map, catchError } from 'rxjs/operators';

import { pipe } from 'rxjs/Rx';
import { of } from 'rxjs/observable/of';
import { Actions, Effect, ofType } from '@ngrx/effects';

import { JobTypesApiService } from '../services/jobtypes-api.service';
import {
	JobTypesActionTypes,
	FindJobtypesSuccess,
	FindJobtypesError,
} from './job-types.actions';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class JobTypesEffects {
	@Effect()
	fetchJobTypes$ = this.actions$.pipe(
		ofType(JobTypesActionTypes.FIND_JOBTYPES),
		switchMap(() => this.apiService.fetchAll()),
		map(res => new FindJobtypesSuccess(res)),
		catchError(() => of(new FindJobtypesError()))
	);

	constructor(
		private actions$: Actions,
		private apiService: JobTypesApiService
	) {}
}
