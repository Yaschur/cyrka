import { Injectable } from '@angular/core';

import { Effect, Actions, ofType } from '@ngrx/effects';
import { switchMap, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';

import {
	ProjectActionTypes,
	FindJobtypes,
	LoadJobtypes,
} from './project.actions';
import { JobApiService } from '../services/job-api.service';

@Injectable()
export class JobtypeEffects {
	@Effect()
	findJobtypes$ = this._actions$.pipe(
		ofType<FindJobtypes>(ProjectActionTypes.FIND_JOBTYPES),
		switchMap(() => this._apiService.fetchAll()),
		map(res => new LoadJobtypes(res)),
		catchError(e => {
			console.log('Network error', e);
			return of();
		})
	);

	constructor(private _actions$: Actions, private _apiService: JobApiService) {}
}
