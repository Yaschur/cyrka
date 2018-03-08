import { Action } from '@ngrx/store';

export const GET_JOBTYPES = 'GET_JOBTYPES';
export const GET_JOBTYPES_SUCCESS = 'GET_JOBTYPES_SUCCESS';
export const GET_JOBTYPES_ERROR = 'GET_JOBTYPES_ERROR';

export class GetJobtypes implements Action {
	readonly type = GET_JOBTYPES;
}
