import { Injectable } from '@angular/core';

import { Effect, Actions, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { of } from 'rxjs/observable/of';
import {
	switchMap,
	map,
	catchError,
	withLatestFrom,
	mapTo,
	mergeMap,
} from 'rxjs/operators';

import { ProjectApiService } from '../services/project-api.service';
import {
	FindProjects,
	ProjectActionTypes,
	LoadProjects,
	GetProject,
	LoadProject,
	SetProduct,
	CreateProject,
	ChangeJob,
	SetJob,
} from './project.actions';
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
				return of();
			}
			return this._apiService.getById(pair[0].payload).pipe(
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
		})
	);

	@Effect()
	createProject$ = this._actions$.pipe(
		ofType<CreateProject>(ProjectActionTypes.CREATE_PROJECT),
		mergeMap(() =>
			this._apiService.register().pipe(
				map(r => {
					return new GetProject(r.resourceId);
				}),
				catchError(e => {
					console.log('Network error', e);
					return of();
				})
			)
		)
	);

	@Effect()
	setProduct$ = this._actions$.pipe(
		ofType<SetProduct>(ProjectActionTypes.SET_PRODUCT),
		withLatestFrom(this._store$.select(getProjectEntity)),
		mergeMap(p =>
			this._apiService.setProduct(p[1].id, p[0].payload).pipe(
				mapTo({ type: 'NO_ACTION' }),
				catchError(e => {
					console.log('Network error', e);
					return of();
				})
			)
		)
	);

	@Effect()
	setJob$ = this._actions$.pipe(
		ofType<SetJob>(ProjectActionTypes.SET_JOB),
		withLatestFrom(this._store$.select(getProjectEntity)),
		mergeMap(p =>
			this._apiService.setJob(p[1].id, p[0].payload).pipe(
				mapTo({ type: 'NO_ACTION' }),
				catchError(e => {
					console.log('Network error', e);
					return of();
				})
			)
		)
	);

	@Effect()
	changeJob$ = this._actions$.pipe(
		ofType<ChangeJob>(ProjectActionTypes.CHANGE_JOB),
		withLatestFrom(this._store$.select(getProjectEntity)),
		mergeMap(p =>
			this._apiService
				.changeJob(p[1].id, p[0].payload.jobTypeId, p[0].payload)
				.pipe(
					mapTo({ type: 'NO_ACTION' }),
					catchError(e => {
						console.log('Network error', e);
						return of();
					})
				)
		)
	);

	constructor(
		private _actions$: Actions,
		private _store$: Store<{}>,
		private _apiService: ProjectApiService
	) {}
}
