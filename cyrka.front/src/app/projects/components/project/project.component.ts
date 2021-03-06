import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Store } from '@ngxs/store';

import { MenuLink } from '../../../shared/menu/menu-link';
import { FindProjects } from '../../store/project.actions';
import { ProjectState } from '../../store/project.state';

@Component({
	selector: 'app-project',
	templateUrl: './project.component.html',
	styleUrls: ['./project.component.scss'],
})
export class ProjectComponent {
	menuItems$: Observable<MenuLink[]>;

	constructor(private _store: Store, private _route: ActivatedRoute) {
		_store.dispatch(FindProjects);

		this.menuItems$ = _store.select(ProjectState.getProject).pipe(
			switchMap(p =>
				of(
					[
						{
							linkUrl: '/projects',
							linkText: 'Список',
							linkTitle: 'список проектов',
						},
						{
							linkText: p && p.id ? p.id : 'Создать',
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
