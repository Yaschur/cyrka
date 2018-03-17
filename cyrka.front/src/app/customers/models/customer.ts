import { Title } from './title.';

export interface Customer {
	id: string;
	name: string;
	description: string;
	titles: Title[];
}
