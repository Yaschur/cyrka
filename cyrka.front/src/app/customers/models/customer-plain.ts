import { TitlePlain } from './title-plain';

export interface CustomerPlain {
	id: string;
	name: string;
	description: string;
	titles: TitlePlain[];
}
