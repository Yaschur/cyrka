import { Action } from '@ngrx/store';
import { Jobtype } from '../models/jobtype';

export enum JobtypeActionTypes {
	FIND_JOBTYPES = '[JobTypes] FIND_JOBTYPES',
	FIND_JOBTYPES_SUCCESS = '[JobTypes] FIND_JOBTYPES_SUCCESS',
	FIND_JOBTYPES_ERROR = '[JobTypes] FIND_JOBTYPES_ERROR',
}

export class FindJobtypes implements Action {
	readonly type = JobtypeActionTypes.FIND_JOBTYPES;
}

export class FindJobtypesSuccess implements Action {
	readonly type = JobtypeActionTypes.FIND_JOBTYPES_SUCCESS;

	constructor(public jobtypes: Jobtype[]) {}
}

export class FindJobtypesError implements Action {
	readonly type = JobtypeActionTypes.FIND_JOBTYPES_ERROR;
}

export type JobtypeActions =
	| FindJobtypes
	| FindJobtypesSuccess
	| FindJobtypesError;
