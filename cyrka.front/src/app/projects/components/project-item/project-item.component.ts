import { Component, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { filter } from 'rxjs/operators';
import { Store } from '@ngrx/store';

import { Project } from '../../models/project';
import { getProjectEntity } from '../../project.store';
import { GetProject } from '../../store/project.actions';

@Component({
	selector: 'app-project-item',
	templateUrl: './project-item.component.html',
	styleUrls: ['./project-item.component.scss'],
})
export class ProjectItemComponent {
	@Output() project$: Observable<Project>;

	constructor(private _route: ActivatedRoute, private _store: Store<{}>) {
		if (this._route.snapshot.paramMap.has('projectId')) {
			this._store.dispatch(
				new GetProject(this._route.snapshot.paramMap.get('projectId'))
			);
		}

		this.project$ = this._store.select(getProjectEntity).pipe(filter(p => !!p));
	}
}
