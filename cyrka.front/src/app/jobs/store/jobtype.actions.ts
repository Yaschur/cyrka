import { Jobtype } from '../models/jobtype';

export class FindJobtypes {
	static readonly type = '[Job] FindJobtypes';
}

export class LoadJobtypes {
	static readonly type = '[Job] LoadJobtypes';
	constructor(public readonly payload: Jobtype[]) {}
}
