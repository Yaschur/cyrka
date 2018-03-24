import { Action } from '@ngrx/store';
import { Project } from '../models/project';

export enum ProjectActionTypes {
	FIND_PROJECTS = '[project] FIND_PROJECTS',
	LOAD_PROJECTS = '[project] LOAD_PROJECTS',
	// FIND_CUSTOMERS = '[customer] FIND_CUSTOMERS',
	// CUSTOMERS_RECEIVED = '[customer] CUSTOMERS_RECEIVED',
	// GET_CUSTOMER = '[customer] GET_CUSTOMER',
	// CUSTOMER_RECEIVED = '[customer] CUSTOMER_RECEIVED',
	// UPDATE_CUSTOMER = '[customer] UPDATE_CUSTOMER',
	// UPDATE_TITLE = '[customer] UPDATE_TITLE',
}

export class FindProjects implements Action {
	readonly type = ProjectActionTypes.FIND_PROJECTS;
}

export class LoadProjects implements Action {
	readonly type = ProjectActionTypes.LOAD_PROJECTS;

	constructor(public payload: Project[]) {}
}

export type ProjectActions = FindProjects | LoadProjects;
