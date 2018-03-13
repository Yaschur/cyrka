import { Action } from '@ngrx/store';
import { Jobtype } from '../models/jobtype';
import { JobtypeActions, JobtypeActionTypes } from './jobtype.actions';

export interface JobtypeState {
	jobtypes: Jobtype[];
	loaded: boolean;
}

export const initialState: JobtypeState = {
	jobtypes: [],
	loaded: false,
};

export function jobtypeReducer(
	state: JobtypeState = initialState,
	action: JobtypeActions
): JobtypeState {
	switch (action.type) {
		case JobtypeActionTypes.FIND_JOBTYPES: {
			return { ...state, jobtypes: [], loaded: false };
		}
		case JobtypeActionTypes.FIND_JOBTYPES_SUCCESS: {
			return { ...state, jobtypes: action.jobtypes, loaded: true };
		}
		default:
			return state;
	}
}
