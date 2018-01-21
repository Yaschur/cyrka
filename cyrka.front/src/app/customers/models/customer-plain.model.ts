import { TitlePlain } from './title-plain.model';

export interface CustomerPlain {
	id: string;
	name: string;
	description: string;
	titles: TitlePlain[];
}
