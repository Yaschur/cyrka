import { ProductSet } from './product-set';
import { JobSet } from './job-set';
import { Payments } from './payments';
import { ProjectStatuses } from '../../shared/projectStatuses/projectStatuses';
import { Money } from './money';

export interface Project {
	id: string;
	status: ProjectStatuses;
	product: ProductSet;
	jobs: JobSet[];
	payments: Payments;
	money: Money;
}
