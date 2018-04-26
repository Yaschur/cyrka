import { Component, Output, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable } from 'rxjs';
import { filter, tap } from 'rxjs/operators';
import { Store } from '@ngxs/store';

import { Customer } from '../../models/customer';
import { MenuLink } from '../../../shared/menu/menu-link';
import { UpdateCustomer, FindCustomers } from '../../store/customer.actions';
import { CustomerState } from '../../store/customer.state';

@Component({
	selector: 'app-customer',
	templateUrl: './customer.component.html',
	styleUrls: ['./customer.component.scss'],
})
export class CustomerComponent {
	@Output() customerItem_read$: Observable<Customer>;

	@Output() customerItems_read$: Observable<Customer[]>;

	@Input()
	set customerItem_write$(csts: Observable<Customer>) {
		if (!csts) {
			return;
		}
		csts.subscribe({
			next: cst => this._store.dispatch(new UpdateCustomer(cst)),
			complete: () =>
				this._router.navigate(['..'], { relativeTo: this._route }),
		});
	}

	menuItems: MenuLink[] = [
		{
			linkUrl: '/customers',
			linkText: 'Список',
			linkTitle: 'список заказчиков',
		},
		{
			linkUrl: '/customers/register',
			linkText: 'Добавить',
			linkTitle: 'зарегистрировать нового заказчика',
		},
	];

	constructor(
		private _route: ActivatedRoute,
		private _router: Router,
		private _store: Store
	) {
		this._store.dispatch(FindCustomers);
		this.customerItem_read$ = _store.select(CustomerState.getCustomer).pipe(
			filter(cst => cst != null),
			tap(cst => {
				if (cst.id) {
					this.menuItems = [
						...this.menuItems.slice(0, 1),
						{
							linkText: 'Изменить',
							linkTitle: 'изменить данные заказчика',
							linkUrl: `/customers/${cst.id}/edit`,
						},
					];
				}
			})
		);
		this.customerItems_read$ = _store.select(CustomerState.getCustomers);
	}
}
