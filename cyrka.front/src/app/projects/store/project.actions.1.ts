import { Action } from '@ngrx/store';

import { Project } from '../models/project';
import { Customer } from '../models/customer';
import { ProductSet } from '../models/product-set';
import { JobSet } from '../models/job-set';
import { Jobtype } from '../models/job-type';
import { ProjectStatuses } from '../../shared/projectStatuses/projectStatuses';
import { Payments } from '../models/payments';

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
	SET_JOB = '[project] SET_JOB',
	SET_STATUS = '[project] SET_STATUS',
	CHANGE_JOB = '[project] CHANGE_JOB',
	SET_PAYMENTS = '[project] SET_PAYMENTS',
	FIND_CUSTOMERS = '[project] FIND_CUSTOMERS',
	LOAD_CUSTOMERS = '[project] LOAD_CUSTOMERS',
	FIND_JOBTYPES = '[project] FIND_JOBTYPES',
	LOAD_JOBTYPES = '[project] LOAD_JOBTYPES',
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

export class SetJob implements Action {
	readonly type = ProjectActionTypes.SET_JOB;

	constructor(public payload: JobSet) {}
}

export class SetStatus implements Action {
	readonly type = ProjectActionTypes.SET_STATUS;

	constructor(public payload: ProjectStatuses) {}
}

export class ChangeJob implements Action {
	readonly type = ProjectActionTypes.CHANGE_JOB;

	constructor(public payload: JobSet) {}
}

export class SetPayments implements Action {
	readonly type = ProjectActionTypes.SET_PAYMENTS;

	constructor(public payload: Payments) {}
}

export class FindCustomers implements Action {
	readonly type = ProjectActionTypes.FIND_CUSTOMERS;
}

export class LoadCustomers implements Action {
	readonly type = ProjectActionTypes.LOAD_CUSTOMERS;

	constructor(public payload: Customer[]) {}
}

export class FindJobtypes implements Action {
	readonly type = ProjectActionTypes.FIND_JOBTYPES;
}

export class LoadJobtypes implements Action {
	readonly type = ProjectActionTypes.LOAD_JOBTYPES;

	constructor(public payload: Jobtype[]) {}
}

export type ProjectActions =
	| FindProjects
	| LoadProjects
	| GetProject
	| ListProjects
	| LoadProject
	| CreateProject
	| SetProduct
	| SetJob
	| SetStatus
	| ChangeJob
	| SetPayments
	| FindCustomers
	| LoadCustomers
	| FindJobtypes
	| LoadJobtypes;
