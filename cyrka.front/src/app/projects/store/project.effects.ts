import { Injectable } from '@angular/core';
import { Effect, Actions, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { ProjectApiService } from '../services/project-api.service';
import {
	FindProjects,
	ProjectActionTypes,
	LoadProjects,
} from './project.actions';
import { switchMap, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';

@Injectable()
export class ProjectEffects {
	@Effect()
	findProjects$ = this._actions$.pipe(
		ofType<FindProjects>(ProjectActionTypes.FIND_PROJECTS),
		switchMap(() => this._apiService.fetchAll()),
		map(res => new LoadProjects(res)),
		catchError(e => {
			console.log('Network error', e);
			return of();
		})
	);

	constructor(
		private _actions$: Actions,
		// private _store$: Store<{ project: CustomerState }>,
		private _apiService: ProjectApiService
	) {}
}
