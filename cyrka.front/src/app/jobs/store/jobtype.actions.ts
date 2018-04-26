import { Jobtype } from '../models/jobtype';

export class FindJobtypes {
	static readonly type = '[Job] FindJobtypes';
}

export class LoadJobtypes {
	static readonly type = '[Job] LoadJobtypes';
	constructor(public readonly payload: Jobtype[]) {}
}

export class SelectJobtype {
	static readonly type = '[Job] SelectJobtype';
	constructor(public readonly payload: string) {}
}

export class UpdateJobtype {
	static readonly type = '[Job] UpdateJobtype';
	constructor(public readonly payload: Jobtype) {}
}
