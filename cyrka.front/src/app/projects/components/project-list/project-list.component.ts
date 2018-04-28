import { Component } from '@angular/core';

import { Observable } from 'rxjs';
import { Store } from '@ngxs/store';

import { Project } from '../../models/project';
import { ProjectState } from '../../store/project.state';
import { ClearProjectSelection } from '../../store/project.actions';

@Component({
	selector: 'app-project-list',
	templateUrl: './project-list.component.html',
	styleUrls: ['./project-list.component.scss'],
})
export class ProjectListComponent {
	projects$: Observable<Project[]>;

	constructor(private _store: Store) {
		this._store.dispatch(ClearProjectSelection);
		this.projects$ = this._store.select(ProjectState.getProjects);
	}
}
