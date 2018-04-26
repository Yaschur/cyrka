import { Jobtype } from '../models/jobtype';

export interface JobtypeStateModel {
	jobtypes: Jobtype[];
	selectedJobtype: string;
}
