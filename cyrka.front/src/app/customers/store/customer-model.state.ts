import { Customer } from '../models/customer';

export interface CustomerStateModel {
	customers: Customer[];
	selectedCustomer: string;
}
