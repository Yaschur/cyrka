import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { of } from 'rxjs/observable/of';
import { switchMap, map, filter, take } from 'rxjs/operators';
import { Store } from '@ngrx/store';

import { MenuLink } from '../../../shared/menu/menu-link';
import { FindProjects } from '../../store/project.actions';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';
import { getProjectEntity } from '../../project.store';

@Component({
	selector: 'app-project',
	templateUrl: './project.component.html',
	styleUrls: ['./project.component.scss'],
})
export class ProjectComponent {
	menuItems: Observable<MenuLink[]>;

	constructor(private _store: Store<{}>, private _route: ActivatedRoute) {
		this._store.dispatch(new FindProjects());

		this.menuItems = this._store.select(getProjectEntity).pipe(
			switchMap(p =>
				of(
					[
						{
							linkUrl: '/projects',
							linkText: 'Список',
							linkTitle: 'список проектов',
						},
						{
							linkText: p && p.id ? 'Изменить' : 'Создать',
							linkTitle:
								p && p.id ? 'изменить данные проекта' : 'создать новый проект',
							linkUrl: p && p.id ? `/projects/${p.id}` : '/projects/register',
						},
					].slice(0, p ? 2 : 1)
				)
			)
		);
	}
}
