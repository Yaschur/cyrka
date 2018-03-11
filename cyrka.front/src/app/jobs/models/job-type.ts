import { Units } from '../../shared/units/units';

export interface JobType {
	id: string;
	name: string;
	unit: Units;
	rate: number;
	description: string;
}
