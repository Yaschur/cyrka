import { Component, OnInit, Input } from '@angular/core';

import { CustomerDefinition } from '../models/customer-definition.model';

@Component({
	selector: 'app-customers-change',
	templateUrl: './customers-change.component.html'
})
export class CustomerChangeComponent implements OnInit {

	constructor() { }

	@Input()
	public customer: CustomerDefinition;

	public ngOnInit() { }
}
