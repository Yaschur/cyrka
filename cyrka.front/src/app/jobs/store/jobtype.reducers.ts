import { Action } from '@ngrx/store';
import { Jobtype } from '../models/jobtype';
import { JobtypeActions, JobtypeActionTypes } from './jobtype.actions';

export interface JobtypeState {
	jobtypes: Jobtype[];
	loading: boolean;
}

export const initialState: JobtypeState = {
	jobtypes: [],
	loading: false,
};

export function jobtypeReducer(
	state: JobtypeState = initialState,
	action: JobtypeActions
): JobtypeState {
	switch (action.type) {
		case JobtypeActionTypes.FIND_JOBTYPES: {
			return { ...state, loading: true };
		}
		case JobtypeActionTypes.FIND_JOBTYPES_SUCCESS: {
			return { ...state, jobtypes: action.jobtypes, loading: false };
		}
		default:
			return state;
	}
}
