import { map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import { State, Action, StateContext, Selector } from '@ngxs/store';

import { JobtypeStateModel } from './jobtype-model.state';
import { JobtypeApiService } from '../services/jobtype-api.service';
import {
	FindJobtypes,
	LoadJobtypes,
	SelectJobtype,
	UpdateJobtype,
} from './jobtype.actions';

@State<JobtypeStateModel>({
	name: 'jobtype',
	defaults: {
		jobtypes: [],
		selectedJobtype: null,
	},
})
export class JobtypeState {
	@Selector()
	static getJobtypes(state: JobtypeStateModel) {
		return state.jobtypes;
	}

	@Selector()
	static getJobtype(state: JobtypeStateModel) {
		return state.jobtypes.find(jt => jt.id === state.selectedJobtype);
	}

	constructor(private readonly _jobtypeApi: JobtypeApiService) {}

	@Action(FindJobtypes)
	findJobtypes(sc: StateContext<JobtypeStateModel>) {
		if (sc.getState().jobtypes.length > 0) {
			return;
		}
		this._jobtypeApi.fetchAll().pipe(
			map(res => sc.dispatch(new LoadJobtypes(res))),
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}

	@Action(LoadJobtypes)
	loadJobtypes(sc: StateContext<JobtypeStateModel>, a: LoadJobtypes) {
		sc.patchState({
			jobtypes: a.payload,
		});
	}

	@Action(SelectJobtype)
	selectJobtype(sc: StateContext<JobtypeStateModel>, a: SelectJobtype) {
		sc.patchState({
			selectedJobtype: a.payload,
		});
	}

	@Action(UpdateJobtype)
	updateJobtype(sc: StateContext<JobtypeStateModel>, a: UpdateJobtype) {
		(a.payload.id
			? this._jobtypeApi.change(a.payload.id, a.payload)
			: this._jobtypeApi.register(a.payload)
		).pipe(
			map(() => sc.patchState({ jobtypes: [] })),
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}
}
