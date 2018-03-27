import { Component, Input } from '@angular/core';

import { ProductSet } from '../../models/product-set';

@Component({
	selector: 'app-project-product',
	templateUrl: './project-product.component.html',
	styleUrls: ['./project-product.component.scss'],
})
export class ProjectProductComponent {
	@Input() productSet: ProductSet;

	constructor() {}
}
