import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';

import {
	switchMap,
	map,
	catchError,
	filter,
	withLatestFrom,
	take,
} from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { Store } from '@ngrx/store';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { ROUTER_NAVIGATION, RouterNavigationAction } from '@ngrx/router-store';
import { Store as StoreX } from '@ngxs/store';

import { JobtypeApiService } from '../services/jobtype-api.service';
import {
	JobtypeActionTypes,
	JobtypesReceived,
	FindJobtypes,
	GetJobtype,
	JobtypeReceived,
	UpdateJobtype,
} from './jobtype.actions';
import { Jobtype } from '../models/jobtype';
import { JobtypeState } from './jobtype.reducers';
import { AuthStateModel } from '../../auth/auth.model';

@Injectable()
export class JobtypeEffects {
	@Effect()
	navigateJobtypes$ = this._actions$.pipe(
		ofType<RouterNavigationAction<RouterStateSnapshot>>(ROUTER_NAVIGATION),
		filter(() =>
			this._storeX.selectSnapshot<boolean>(
				(state: { auth: AuthStateModel }) => !!state.auth.accessToken
			)
		),
		map(r => r.payload.routerState.root.firstChild),
		filter(r => this.isOnJobtypes(r) && !this.isWithJobtypeId(r)),
		take(1),
		map(() => new FindJobtypes())
	);

	@Effect()
	navigateJobtype$ = this._actions$.pipe(
		ofType<RouterNavigationAction<RouterStateSnapshot>>(ROUTER_NAVIGATION),
		filter(() =>
			this._storeX.selectSnapshot<boolean>(
				(state: { auth: AuthStateModel }) => !!state.auth.accessToken
			)
		),
		map(r => r.payload.routerState.root.firstChild),
		filter(r => this.isOnJobtypes(r) && this.isWithJobtypeId(r)),
		map(r => r.paramMap.get('jobtypeId')),
		withLatestFrom(this._store$),
		filter(s => !s[1].jobtype.jobtypes.some(jt => jt.id === s[0])),
		switchMap(s => of(new GetJobtype(s[0])))
	);

	@Effect()
	fetchJobtypes$ = this._actions$.pipe(
		ofType<FindJobtypes>(JobtypeActionTypes.FIND_JOBTYPES),
		switchMap(() => this._apiService.fetchAll()),
		map(res => new JobtypesReceived(res)),
		catchError(e => {
			console.log('Network error', e);
			return of();
		})
	);

	@Effect()
	fetchJobtype$ = this._actions$.pipe(
		ofType<GetJobtype>(JobtypeActionTypes.GET_JOBTYPE),
		switchMap(a => this._apiService.getById(a.jobtypeId)),
		map(res => new JobtypeReceived(res)),
		catchError(e => {
			console.log('Network error', e);
			return of();
		})
	);

	@Effect()
	updateJobtype$ = this._actions$.pipe(
		ofType<UpdateJobtype>(JobtypeActionTypes.UPDATE_JOBTYPE),
		switchMap(
			a =>
				a.jobtype.id
					? this._apiService.change(a.jobtype.id, a.jobtype)
					: this._apiService.register(a.jobtype)
		),
		map(() => new FindJobtypes()),
		catchError(e => {
			console.log('Network error', e);
			return of();
		})
	);

	constructor(
		private _actions$: Actions,
		private _store$: Store<{ jobtype: JobtypeState }>,
		private _apiService: JobtypeApiService,
		private _storeX: StoreX
	) {}

	private isOnJobtypes(r: ActivatedRouteSnapshot): boolean {
		return r.routeConfig.path.startsWith('jobtypes');
	}
	private isWithJobtypeId(r: ActivatedRouteSnapshot): boolean {
		return r.paramMap.has('jobtypeId');
	}
}
