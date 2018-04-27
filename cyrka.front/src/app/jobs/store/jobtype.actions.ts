import { Jobtype } from '../models/jobtype';

export class FindJobtypes {
	static readonly type = '[Job] FindJobtypes';
}

export class LoadJobtypes {
	constructor(public readonly payload: Jobtype[]) {}
	static readonly type = '[Job] LoadJobtypes';
}

export class SelectJobtype {
	constructor(public readonly payload: string) {}
	static readonly type = '[Job] SelectJobtype';
}

export class UpdateJobtype {
	constructor(public readonly payload: Jobtype) {}
	static readonly type = '[Job] UpdateJobtype';
}
