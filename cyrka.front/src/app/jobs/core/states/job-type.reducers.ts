import { Action } from '@ngrx/store';
import { JobTypeListState } from './job-type.states';
import { JobtypesActionTypes, JobtypesActions } from './job-type.actions';

export const initialState: JobTypeListState = {
	jobTypes: [],
};

export const todoReducer = (
	state: JobTypeListState = initialState,
	action: JobtypesActions
): JobTypeListState => {
	switch (action.type) {
		case JobtypesActionTypes.GET_JOBTYPES: {
			return state;
		}
		case JobtypesActionTypes.GET_JOBTYPES_SUCCESS: {
			return { jobTypes: action.payload };
		}
	}
};
