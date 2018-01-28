import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CustomerDefinition } from '../models/customer-definition.model';

@Component({
	selector: 'app-customers-details',
	templateUrl: './customers-details.component.html',
	styleUrls: ['./customers-details.component.scss']
})
export class CustomersDetailsComponent {

	constructor() {
		this.close = new EventEmitter();
		this.change = new EventEmitter();
		this.delete = new EventEmitter();
	}

	@Input()
	public customer: CustomerDefinition;

	@Output()
	public close: EventEmitter<void>;
	@Output()
	public change: EventEmitter<void>;
	@Output()
	public delete: EventEmitter<void>;
}
