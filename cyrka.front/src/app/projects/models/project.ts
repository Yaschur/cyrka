import { ProductSet } from './product-set';
import { JobSet } from './job-set';
import { Payments } from './payments';
import { ProjectStatuses } from '../../shared/projectStatuses/projectStatuses';

export interface Project {
	id: string;
	status: ProjectStatuses;
	product: ProductSet;
	jobs: JobSet[];
	payments: Payments;
	money: {
		income: number;
		expenses: number;
	};
}
