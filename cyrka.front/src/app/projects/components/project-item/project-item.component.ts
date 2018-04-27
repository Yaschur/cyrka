import { Component, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable } from 'rxjs';
import { filter } from 'rxjs/operators';
import { Store } from '@ngxs/store';

import { Project } from '../../models/project';
import { GetProject } from '../../store/project.actions';
import { ProjectState } from '../../store/project.state';

@Component({
	selector: 'app-project-item',
	templateUrl: './project-item.component.html',
	styleUrls: ['./project-item.component.scss'],
})
export class ProjectItemComponent {
	project$: Observable<Project>;

	productEditMode: boolean;

	constructor(
		private _route: ActivatedRoute,
		private _store: Store,
		private _router: Router
	) {
		this.productEditMode = false;
		if (this._route.snapshot.paramMap.has('projectId')) {
			this._store.dispatch(
				new GetProject(this._route.snapshot.paramMap.get('projectId'))
			);
		}

		this.project$ = this._store
			.select(ProjectState.getProject)
			.pipe(filter(p => !!p));
		this.project$.subscribe(p => this.setProductEdit(!p.product));
	}

	setProductEdit(mode: boolean) {
		this.productEditMode = mode;
	}
}
