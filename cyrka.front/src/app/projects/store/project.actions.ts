import { Action } from '@ngrx/store';
import { Project } from '../models/project';
import { Customer } from '../models/customer';
import { ProductSet } from '../models/product-set';

export enum ProjectActionTypes {
	// fetch projects from data service
	FIND_PROJECTS = '[project] FIND_PROJECTS',
	// update state with projects payload
	LOAD_PROJECTS = '[project] LOAD_PROJECTS',
	LIST_PROJECTS = '[project] LIST_PROJECTS',
	// update state with project selected
	GET_PROJECT = '[project] GET_PROJECT',
	LOAD_PROJECT = '[project] LOAD_PROJECT',
	CREATE_PROJECT = '[project] CREATE_PROJECT',
	SET_PRODUCT = '[project] SET_PRODUCT',
	// FIND_CUSTOMERS = '[customer] FIND_CUSTOMERS',
	// CUSTOMERS_RECEIVED = '[customer] CUSTOMERS_RECEIVED',
	// GET_CUSTOMER = '[customer] GET_CUSTOMER',
	// CUSTOMER_RECEIVED = '[customer] CUSTOMER_RECEIVED',
	// UPDATE_CUSTOMER = '[customer] UPDATE_CUSTOMER',
	// UPDATE_TITLE = '[customer] UPDATE_TITLE',
	FIND_CUSTOMERS = '[project] FIND_CUSTOMERS',
	LOAD_CUSTOMERS = '[project] LOAD_CUSTOMERS',
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

export class ListProjects implements Action {
	readonly type = ProjectActionTypes.LIST_PROJECTS;
}

export class LoadProject implements Action {
	readonly type = ProjectActionTypes.LOAD_PROJECT;

	constructor(public payload: Project) {}
}

export class CreateProject implements Action {
	readonly type = ProjectActionTypes.CREATE_PROJECT;
}

export class SetProduct implements Action {
	readonly type = ProjectActionTypes.SET_PRODUCT;

	constructor(public payload: ProductSet) {}
}

export class FindCustomers implements Action {
	readonly type = ProjectActionTypes.FIND_CUSTOMERS;
}

export class LoadCustomers implements Action {
	readonly type = ProjectActionTypes.LOAD_CUSTOMERS;

	constructor(public payload: Customer[]) {}
}

export type ProjectActions =
	| FindProjects
	| LoadProjects
	| GetProject
	| ListProjects
	| LoadProject
	| CreateProject
	| SetProduct
	| FindCustomers
	| LoadCustomers;
