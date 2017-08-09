import { ProjectType } from './ProjectType';

export interface Project {
	id: string;
	name: string;
	type: ProjectType;
	plannedFinish: Date;
	estimatedFinish: Date;
}
