import { Action } from '@ngrx/store';
import { JobType } from '../models/job-type';

export enum JobTypesActionTypes {
	FIND_JOBTYPES = '[JobTypes] FIND_JOBTYPES',
	FIND_JOBTYPES_SUCCESS = '[JobTypes] FIND_JOBTYPES_SUCCESS',
	FIND_JOBTYPES_ERROR = '[JobTypes] FIND_JOBTYPES_ERROR',
}

export class FindJobtypes implements Action {
	readonly type = JobTypesActionTypes.FIND_JOBTYPES;
}

export class FindJobtypesSuccess implements Action {
	readonly type = JobTypesActionTypes.FIND_JOBTYPES_SUCCESS;

	constructor(public jobTypes: JobType[]) {}
}

export class FindJobtypesError implements Action {
	readonly type = JobTypesActionTypes.FIND_JOBTYPES_ERROR;
}

export type JobTypesActions =
	| FindJobtypes
	| FindJobtypesSuccess
	| FindJobtypesError;
