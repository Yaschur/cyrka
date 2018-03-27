import { Component, Input, Output, EventEmitter } from '@angular/core';

import { ProductSet } from '../../models/product-set';

@Component({
	selector: 'app-project-product',
	templateUrl: './project-product.component.html',
	styleUrls: ['./project-product.component.scss'],
})
export class ProjectProductComponent {
	@Input() productSet: ProductSet;
	@Output() changeProduct: EventEmitter<void>;

	constructor() {
		this.changeProduct = new EventEmitter();
	}

	setEdit() {
		this.changeProduct.emit();
	}
}
