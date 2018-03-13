import { Action } from '@ngrx/store';
import { Jobtype } from '../models/jobtype';
import { JobtypeActions, JobtypeActionTypes } from './jobtype.actions';

export interface JobtypeState {
	jobtypes: Jobtype[];
	listLoaded: boolean;
}

export const initialState: JobtypeState = {
	jobtypes: [],
	listLoaded: false,
};

export function jobtypeReducer(
	state: JobtypeState = initialState,
	action: JobtypeActions
): JobtypeState {
	switch (action.type) {
		case JobtypeActionTypes.FIND_JOBTYPES: {
			return { ...state, jobtypes: [], listLoaded: false };
		}
		case JobtypeActionTypes.UPDATE_JOBTYPES: {
			return { ...state, jobtypes: action.jobtypes, listLoaded: true };
		}
		case JobtypeActionTypes.UPDATE_JOBTYPE: {
			const exInd = state.jobtypes.findIndex(jt => jt.id === action.jobtype.id);
			const newInd = exInd < 0 ? state.jobtypes.length : exInd;
			return {
				...state,
				jobtypes: [
					...state.jobtypes.slice(0, newInd),
					action.jobtype,
					...state.jobtypes.slice(newInd + 1),
				],
			};
		}

		case JobtypeActionTypes.GET_JOBTYPE:
		default:
			return state;
	}
}
