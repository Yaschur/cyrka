import { Injectable } from '@angular/core';
import { Effect, Actions, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { ProjectApiService } from '../services/project-api.service';
import {
	FindProjects,
	ProjectActionTypes,
	LoadProjects,
	GetProject,
	LoadProject,
} from './project.actions';
import { switchMap, map, catchError, withLatestFrom } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { ProjectState } from './project.reducers';
import { getProjectEntity } from '../project.store';

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

	@Effect()
	getProject$ = this._actions$.pipe(
		ofType<GetProject>(ProjectActionTypes.GET_PROJECT),
		withLatestFrom(this._store$.select(getProjectEntity)),
		switchMap(pair => {
			if (pair[1]) {
				return of(null);
			}
			return this._apiService.getById(pair[0].payload);
		}),
		map(p => {
			if (p) {
				return new LoadProject(p);
			}
		}),
		catchError(e => {
			console.log('Network error', e);
			return of();
		})
	);

	constructor(
		private _actions$: Actions,
		private _store$: Store<{}>,
		private _apiService: ProjectApiService
	) {}
}
