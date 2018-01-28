import { Component, Input } from '@angular/core';
import { CustomerDefinition } from '../models/customer-definition.model';

@Component({
	selector: 'app-customers-details',
	templateUrl: './customers-details.component.html'
})
export class CustomersDetailsComponent {

	@Input()
	public customer: CustomerDefinition;
}
