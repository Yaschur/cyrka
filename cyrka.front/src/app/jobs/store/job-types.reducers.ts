import { Action } from '@ngrx/store';
import { JobType } from '../models/job-type';
import { JobTypesActions, JobTypesActionTypes } from './job-types.actions';

export interface JobTypesState {
	jobTypes: JobType[];
	loading: boolean;
}

export const initialState: JobTypesState = {
	jobTypes: [],
	loading: false,
};

export function jobTypesReducer(
	state: JobTypesState = initialState,
	action: JobTypesActions
): JobTypesState {
	switch (action.type) {
		case JobTypesActionTypes.FIND_JOBTYPES: {
			return { ...state, loading: true };
		}
		case JobTypesActionTypes.FIND_JOBTYPES_SUCCESS: {
			return { ...state, jobTypes: action.jobTypes, loading: false };
		}
		default:
			return state;
	}
}
