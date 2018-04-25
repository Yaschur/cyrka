import { Component, Output, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Store } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { withLatestFrom, switchMap, filter, tap } from 'rxjs/operators';

import { Customer } from '../../models/customer';
import { UpdateCustomer } from '../../store/customer.actions';
import { getCustomerEntities } from '../../customer.store';
import { MenuLink } from '../../../shared/menu/menu-link';

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
		private _store: Store<{}>
	) {
		this.customerItem_read$ = _store.select(getCustomerEntities).pipe(
			withLatestFrom(_route.paramMap),
			switchMap(p =>
				of((p[1].has('customerId')
					? p[0].find(cst => cst.id === p[1].get('customerId')) || {}
					: {}) as Customer)
			),
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

		this.customerItems_read$ = _store.select(getCustomerEntities);
	}
}
