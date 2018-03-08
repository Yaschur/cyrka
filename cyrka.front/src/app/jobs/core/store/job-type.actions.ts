import { Action } from '@ngrx/store';
import { JobType, JobTypeState } from '../models/job-type.model';

export const GET_JOBTYPES = 'GET_JOBTYPES';
export const GET_JOBTYPES_SUCCESS = 'GET_JOBTYPES_SUCCESS';
export const GET_JOBTYPES_ERROR = 'GET_JOBTYPES_ERROR';

export class GetJobtypes implements Action {
	readonly type = GET_JOBTYPES;
}

export class GetJobtypesSuccess implements Action {
	readonly type = GET_JOBTYPES_SUCCESS;

	constructor(public payload: JobTypeState[]) {}
}

export class GetJobtypesError implements Action {
	readonly type = GET_JOBTYPES_ERROR;
}
