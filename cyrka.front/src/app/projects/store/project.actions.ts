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
	static readonly type = '[Project] LoadProjects';
	constructor(public readonly payload: Project[]) {}
}

export class GetProject {
	static readonly type = '[Project] GetProject';
	constructor(public readonly payload: string) {}
}

export class LoadProject {
	static readonly type = '[Project] LoadProject';
	constructor(public readonly payload: Project) {}
}

export class CreateProject {
	static readonly type = '[Project] CreateProject';
}

export class SetProduct {
	static readonly type = '[Project] SetProduct';
	constructor(public readonly payload: ProductSet) {}
}

export class SetJob {
	static readonly type = '[Project] SetJob';
	constructor(public readonly payload: JobSet) {}
}

export class SetStatus {
	static readonly type = '[Project] SetStatus';
	constructor(public readonly payload: ProjectStatuses) {}
}

export class ChangeJob {
	static readonly type = '[Project] ChangeJob';
	constructor(public readonly payload: JobSet) {}
}

export class SetPayments {
	static readonly type = '[Project] SetPayments';
	constructor(public readonly payload: Payments) {}
}

export class FindCustomers {
	static readonly type = '[Project] FindCustomers';
}

export class LoadCustomers {
	static readonly type = '[Project] LoadCustomers';
	constructor(public readonly payload: Customer[]) {}
}

export class FindJobtypes {
	static readonly type = '[Project] FindJobtypes';
}

export class LoadJobtypes {
	static readonly type = '[Project] LoadJobtypes';
	constructor(public readonly payload: Jobtype[]) {}
}
