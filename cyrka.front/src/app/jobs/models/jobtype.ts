import { Units } from '../../shared/units/units';

export interface Jobtype {
	id: string;
	name: string;
	unit: Units;
	rate: number;
	description: string;
}
