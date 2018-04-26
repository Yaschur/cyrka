import { Project } from '../models/project';
import { Customer } from '../models/customer';
import { Jobtype } from '../models/job-type';

export interface ProjectStateModel {
	projects: Project[];
	selectedProject: string;
	customers: Customer[];
	jobtypes: Jobtype[];
}
