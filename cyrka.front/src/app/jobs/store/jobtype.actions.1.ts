import { Action } from '@ngrx/store';
import { Jobtype } from '../models/jobtype';

export enum JobtypeActionTypes {
	FIND_JOBTYPES = '[jobtype] FIND_JOBTYPES',
	JOBTYPES_RECEIVED = '[jobtype] JOBTYPES_RECEIVED',
	GET_JOBTYPE = '[jobtype] GET_JOBTYPE',
	JOBTYPE_RECEIVED = '[jobtype] JOBTYPE_RECEIVED',
	UPDATE_JOBTYPE = '[jobtype] UPDATE_JOBTYPE',
}

export class FindJobtypes implements Action {
	readonly type = JobtypeActionTypes.FIND_JOBTYPES;
}

export class JobtypesReceived implements Action {
	readonly type = JobtypeActionTypes.JOBTYPES_RECEIVED;

	constructor(public jobtypes: Jobtype[]) {}
}

export class GetJobtype implements Action {
	readonly type = JobtypeActionTypes.GET_JOBTYPE;

	constructor(public jobtypeId: string) {}
}

export class JobtypeReceived implements Action {
	readonly type = JobtypeActionTypes.JOBTYPE_RECEIVED;

	constructor(public jobtype: Jobtype) {}
}

export class UpdateJobtype implements Action {
	readonly type = JobtypeActionTypes.UPDATE_JOBTYPE;

	constructor(public jobtype: Jobtype) {}
}

export type JobtypeActions =
	| FindJobtypes
	| JobtypesReceived
	| GetJobtype
	| JobtypeReceived
	| UpdateJobtype;
