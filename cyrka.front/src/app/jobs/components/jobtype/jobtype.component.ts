import { Component, Output, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { withLatestFrom, switchMap, filter, map, tap } from 'rxjs/operators';
import { Store } from '@ngrx/store';

import { Jobtype } from '../../models/jobtype';
import { getJobtypeEntities } from '../../jobtype.store';
import { UpdateJobtype } from '../../store/jobtype.actions';
import { MenuLink } from '../../../shared/menu/menu-link';

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
		private _store: Store<{}>
	) {
		this.jobtypeItem_read$ = _store.select(getJobtypeEntities).pipe(
			withLatestFrom(_route.paramMap),
			switchMap(p =>
				of((p[1].has('jobtypeId')
					? p[0].find(jt => jt.id === p[1].get('jobtypeId')) || {}
					: {}) as Jobtype)
			),
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

		this.jobtypeItems_read$ = _store.select(getJobtypeEntities);
	}
}
