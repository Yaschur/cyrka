import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';

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
	UpdateJobtypes,
	FindJobtypes,
	GetJobtype,
	UpdateJobtype,
} from './jobtype.actions';
import { Jobtype } from '../models/jobtype';
import { JobtypeState } from './jobtype.reducers';

@Injectable()
export class JobtypeEffects {
	@Effect()
	navigateJobtypes$ = this._actions$.pipe(
		ofType<RouterNavigationAction<RouterStateSnapshot>>(ROUTER_NAVIGATION),
		filter(
			r =>
				this.isOnJobTypes(r.payload.routerState.root.firstChild) &&
				!this.isWithJobtypeId(r.payload.routerState.root.firstChild)
		),
		withLatestFrom(this._store$),
		filter(s => !s[1].jobtype.listLoaded),
		switchMap(() => of(new FindJobtypes()))
	);

	@Effect()
	navigateJobtype$ = this._actions$.pipe(
		ofType<RouterNavigationAction<RouterStateSnapshot>>(ROUTER_NAVIGATION),
		filter(
			r =>
				this.isOnJobTypes(r.payload.routerState.root.firstChild) &&
				this.isWithJobtypeId(r.payload.routerState.root.firstChild)
		),
		map(r => r.payload.routerState.root.firstChild.paramMap.get('jobtypeId')),
		withLatestFrom(this._store$),
		filter(s => !s[1].jobtype.jobtypes.some(jt => jt.id === s[0])),
		switchMap(s => of(new GetJobtype(s[0])))
	);

	@Effect()
	fetchJobtypes$ = this._actions$.pipe(
		ofType<FindJobtypes>(JobtypeActionTypes.FIND_JOBTYPES),
		switchMap(() => this._apiService.fetchAll()),
		map(res => new UpdateJobtypes(res)),
		catchError(e => {
			console.log('Network error', e);
			return of();
		})
	);

	@Effect()
	fetchJobtype$ = this._actions$.pipe(
		ofType<GetJobtype>(JobtypeActionTypes.GET_JOBTYPE),
		switchMap(a => this._apiService.getById(a.jobtypeId)),
		map(res => new UpdateJobtype(res)),
		catchError(e => {
			console.log('Network error', e);
			return of();
		})
	);

	constructor(
		private _actions$: Actions,
		private _store$: Store<{ jobtype: JobtypeState }>,
		private _apiService: JobtypeApiService
	) {}

	private isOnJobTypes(r: ActivatedRouteSnapshot): boolean {
		return r.routeConfig.path.startsWith('jobtypes');
	}
	private isWithJobtypeId(r: ActivatedRouteSnapshot): boolean {
		return r.paramMap.has('jobtypeId');
	}
}
