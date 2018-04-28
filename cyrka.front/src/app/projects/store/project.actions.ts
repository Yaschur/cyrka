import { Project } from '../models/project';
import { ProductSet } from '../models/product-set';
import { JobSet } from '../models/job-set';
import { ProjectStatuses } from '../../shared/projectStatuses/projectStatuses';
import { Payments } from '../models/payments';
import { Customer } from '../models/customer';
import { Jobtype } from '../models/job-type';

export class FindProjects {
	static readonly type = '[Project] FindProjects';
}

export class LoadProjects {
	constructor(public readonly payload: Project[]) {}
	static readonly type = '[Project] LoadProjects';
}

export class GetProject {
	constructor(public readonly payload: string) {}
	static readonly type = '[Project] GetProject';
}

export class LoadProject {
	constructor(public readonly payload: Project) {}
	static readonly type = '[Project] LoadProject';
}

export class ClearProjectSelection {
	static readonly type = '[Project] ClearProjectSelection';
}

export class CreateProject {
	static readonly type = '[Project] CreateProject';
}

export class SetProduct {
	constructor(public readonly payload: ProductSet) {}
	static readonly type = '[Project] SetProduct';
}

export class SetJob {
	constructor(public readonly payload: JobSet) {}
	static readonly type = '[Project] SetJob';
}

export class SetStatus {
	constructor(public readonly payload: ProjectStatuses) {}
	static readonly type = '[Project] SetStatus';
}

export class ChangeJob {
	constructor(public readonly payload: JobSet) {}
	static readonly type = '[Project] ChangeJob';
}

export class SetPayments {
	constructor(public readonly payload: Payments) {}
	static readonly type = '[Project] SetPayments';
}

export class FindCustomers {
	static readonly type = '[Project] FindCustomers';
}

export class LoadCustomers {
	constructor(public readonly payload: Customer[]) {}
	static readonly type = '[Project] LoadCustomers';
}

export class FindJobtypes {
	static readonly type = '[Project] FindJobtypes';
}

export class LoadJobtypes {
	constructor(public readonly payload: Jobtype[]) {}
	static readonly type = '[Project] LoadJobtypes';
}
