import { ProductSet } from './product-set';
import { JobSet } from './job-set';
import { ProjectStatuses } from '../../shared/projectStatuses/projectStatuses';

export interface Project {
	id: string;
	status: ProjectStatuses;
	product: ProductSet;
	jobs: JobSet[];
	income: number;
	expenses: number;
}
