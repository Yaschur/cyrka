import { Component, Output, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable } from 'rxjs';
import { filter, tap } from 'rxjs/operators';
import { Store } from '@ngxs/store';

import { Jobtype } from '../../models/jobtype';
import {
	UpdateJobtype,
	SelectJobtype,
	FindJobtypes,
} from '../../store/jobtype.actions';
import { MenuLink } from '../../../shared/menu/menu-link';
import { JobtypeState } from '../../store/jobtype.state';

@Component({
	selector: 'app-jobtype',
	templateUrl: './jobtype.component.html',
	styleUrls: ['./jobtype.component.scss'],
})
export class JobtypeComponent {
	@Output() jobtypeItem_read$: Observable<Jobtype>;

	@Output() jobtypeItems_read$: Observable<Jobtype[]>;

	@Input()
	set jobtypeItem_write$(jts: Observable<Jobtype>) {
		if (!jts) {
			return;
		}
		jts.subscribe({
			next: jt => this._store.dispatch(new UpdateJobtype(jt)),
			complete: () =>
				this._router.navigate(['..'], { relativeTo: this._route }),
		});
	}

	menuItems: MenuLink[] = [
		{
			linkUrl: '/jobtypes',
			linkText: 'Список',
			linkTitle: 'список услуг',
		},
		{
			linkUrl: '/jobtypes/register',
			linkText: 'Добавить',
			linkTitle: 'зарегистрировать новую услугу',
		},
	];

	constructor(
		private _route: ActivatedRoute,
		private _router: Router,
		private _store: Store
	) {
		_store.dispatch(FindJobtypes);
		const id = _route.snapshot.params['jobtypeId'];
		_store.dispatch(new SelectJobtype(id));
		this.jobtypeItem_read$ = _store.select(JobtypeState.getJobtype).pipe(
			filter(jt => jt != null),
			tap(jt => {
				if (jt.id) {
					this.menuItems = [
						...this.menuItems.slice(0, 1),
						{
							linkText: 'Изменить',
							linkTitle: 'изменить данные услуги',
							linkUrl: `/jobtypes/${jt.id}/edit`,
						},
					];
				}
			})
		);
		this.jobtypeItems_read$ = _store.select(JobtypeState.getJobtypes);
	}
}
