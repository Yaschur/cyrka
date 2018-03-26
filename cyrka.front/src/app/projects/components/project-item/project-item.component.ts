import { Component, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { Store } from '@ngrx/store';
import { switchMap, withLatestFrom, map, filter, take } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';

import { MenuLink } from '../../../shared/menu/menu-link';
import { Project } from '../../models/project';
import { getProjectEntity } from '../../project.store';
import { GetProject } from '../../store/project.actions';

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
		this._store.dispatch(
			new GetProject(this._route.snapshot.paramMap.get('projectId'))
		);
		this.project_read$ = this._store
			.select(getProjectEntity)
			.pipe(
				
			);
		// const project$ = _route.paramMap.pipe(
		// 	switchMap(params => params.get('projectId')),
		// 	withLatestFrom(_store.select(getProjectEntities)),
		// 	map(x => x[1].find(p => p.id === x[0]))
		// );
		// project$.pipe(
		// 	take(1)
		// ).subscribe(p => {
		// 	if (!p) {

		// 	}
		// })
	}
}
