import { Component, Output } from '@angular/core';

import { MenuLink } from '../../../shared/menu/menu-link';
import { Observable } from 'rxjs/Observable';
import { Project } from '../../models/project';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { switchMap, withLatestFrom, map, filter, take } from 'rxjs/operators';
import { getProjectEntities } from '../../project.store';
import { of } from 'rxjs/observable/of';

@Component({
	selector: 'app-project-item',
	templateUrl: './project-item.component.html',
	styleUrls: ['./project-item.component.scss'],
})
export class ProjectItemComponent {
	@Output() project_read$: Observable<Project>;

	menuItems: MenuLink[] = [
		{
			linkUrl: '/projects',
			linkText: 'Список',
			linkTitle: 'список проектов',
		},
		// {
		// 	linkUrl: '/projects/register',
		// 	linkText: 'Создать',
		// 	linkTitle: 'создать новый проект',
		// },
	];

	constructor(private _route: ActivatedRoute, private _store: Store<{}>) {
		const project$ = _route.paramMap.pipe(
			switchMap(params => params.get('projectId')),
			withLatestFrom(_store.select(getProjectEntities)),
			map(x => x[1].find(p => p.id === x[0]))
		);
		project$.pipe(
			take(1)
		).subscribe(p => {
			if (!p) {
				
			}
		})
	}
}
