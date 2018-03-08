import { Action } from '@ngrx/store';
import { JobType, JobTypeState } from './job-type.states';

export enum JobtypesActionTypes {
	GET_JOBTYPES = '[JobTypes] GET_JOBTYPES',
	GET_JOBTYPES_SUCCESS = '[JobTypes] GET_JOBTYPES_SUCCESS',
	GET_JOBTYPES_ERROR = '[JobTypes] GET_JOBTYPES_ERROR',
}

export class GetJobtypes implements Action {
	readonly type = JobtypesActionTypes.GET_JOBTYPES;
}

export class GetJobtypesSuccess implements Action {
	readonly type = JobtypesActionTypes.GET_JOBTYPES_SUCCESS;

	constructor(public payload: JobTypeState[]) {}
}

export class GetJobtypesError implements Action {
	readonly type = JobtypesActionTypes.GET_JOBTYPES_ERROR;
}

export type JobtypesActions =
	| GetJobtypes
	| GetJobtypesSuccess
	| GetJobtypesError;
