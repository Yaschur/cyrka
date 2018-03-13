import { Action } from '@ngrx/store';
import { Jobtype } from '../models/jobtype';

export enum JobtypeActionTypes {
	FIND_JOBTYPES = '[jobtype] FIND_JOBTYPES',
	UPDATE_JOBTYPES = '[jobtype] UPDATE_JOBTYPES',
	GET_JOBTYPE = '[jobtype] GET_JOBTYPE',
	UPDATE_JOBTYPE = '[jobtype] UPDATE_JOBTYPE',
}

export class FindJobtypes implements Action {
	readonly type = JobtypeActionTypes.FIND_JOBTYPES;
}

export class UpdateJobtypes implements Action {
	readonly type = JobtypeActionTypes.UPDATE_JOBTYPES;

	constructor(public jobtypes: Jobtype[]) {}
}

export class GetJobtype implements Action {
	readonly type = JobtypeActionTypes.GET_JOBTYPE;

	constructor(public jobtypeId: string) {}
}

export class UpdateJobtype implements Action {
	readonly type = JobtypeActionTypes.UPDATE_JOBTYPE;

	constructor(public jobtype: Jobtype) {}
}

export type JobtypeActions =
	| FindJobtypes
	| UpdateJobtypes
	| GetJobtype
	| UpdateJobtype;
