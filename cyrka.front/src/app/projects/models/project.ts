import { ProductSet } from './product-set';
import { JobSet } from './job-set';

export interface Project {
	id: string;
	status: string;
	product: ProductSet;
	jobs: JobSet[];
}
