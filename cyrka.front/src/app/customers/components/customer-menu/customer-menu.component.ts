import { Component, Input } from '@angular/core';

import { Customer } from '../../models/customer';

interface MenuLink {
	linkTitle: string;
	linkText: string;
	linkUrl: string;
}

@Component({
	selector: 'app-customer-menu',
	templateUrl: './customer-menu.component.html',
	styleUrls: ['./customer-menu.component.scss'],
})
export class CustomerMenuComponent {
	@Input()
	set targetCustomer(cst: Customer) {
		this.changeMenu = {
			linkText: cst && cst.id ? 'Изменить' : 'Добавить',
			linkTitle:
				cst && cst.id
					? 'изменить данные заказчика'
					: 'зарегистрировать нового заказчика',
			linkUrl:
				cst && cst.id ? `/customers/${cst.id}/edit` : '/customers/register',
		};
	}

	changeMenu: MenuLink;

	constructor() {}
}
