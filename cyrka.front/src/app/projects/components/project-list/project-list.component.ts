import { Component } from '@angular/core';
import { MenuLink } from '../../../shared/menu/menu-link';

import { Observable } from 'rxjs/Observable';
import { Store } from '@ngrx/store';

import { Project } from '../../models/project';
import { getProjectEntities } from '../../project.store';
import { take } from 'rxjs/operators';
import { FindProjects } from '../../store/project.actions';

@Component({
	selector: 'app-project-list',
	templateUrl: './project-list.component.html',
	styleUrls: ['./project-list.component.scss'],
})
export class ProjectListComponent {
	projects: Observable<Project[]>;

	menuItems: MenuLink[] = [
		{
			linkUrl: '/projects',
			linkText: 'Список',
			linkTitle: 'список проектов',
		},
		{
			linkUrl: '/projects/register',
			linkText: 'Создать',
			linkTitle: 'создать новый проект',
		},
	];

	constructor(private _store: Store<{}>) {
		this.projects = this._store.select(getProjectEntities);

		this.projects.pipe(take(1)).subscribe(prjs => {
			if (prjs.length == 0) {
				_store.dispatch(new FindProjects());
			}
		});
	}
}
