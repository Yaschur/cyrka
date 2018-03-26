import { Action } from '@ngrx/store';
import { Project } from '../models/project';

export enum ProjectActionTypes {
	FIND_PROJECTS = '[project] FIND_PROJECTS',
	LOAD_PROJECTS = '[project] LOAD_PROJECTS',
	GET_PROJECT = '[project] GET_PROJECT',
	NEW_PROJECT = '[project] NEW_PROJECT',
	LOAD_PROJECT = '[project] LOAD_PROJECT',
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

export class GetProject implements Action {
	readonly type = ProjectActionTypes.GET_PROJECT;

	constructor(public payload: string) {}
}

export class NewProject implements Action {
	readonly type = ProjectActionTypes.NEW_PROJECT;
}

export class LoadProject implements Action {
	readonly type = ProjectActionTypes.LOAD_PROJECT;

	constructor(public payload: Project) {}
}

export type ProjectActions =
	| FindProjects
	| LoadProjects
	| GetProject
	| NewProject
	| LoadProject;
