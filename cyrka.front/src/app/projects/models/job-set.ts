import { Units } from '../../shared/units/units';

export interface JobSet {
	jobTypeId: string;
	jobTypeName: string;
	unitName: Units;
	ratePerUnit: number;
	amount: number;
}
