import { Component } from '@angular/core';

import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';

import { Project } from '../../models/project';
import { getProjectEntities } from '../../project.store';
import { ListProjects } from '../../store/project.actions';

@Component({
	selector: 'app-project-list',
	templateUrl: './project-list.component.html',
	styleUrls: ['./project-list.component.scss'],
})
export class ProjectListComponent {
	projects: Observable<Project[]>;

	constructor(private _store: Store<{}>) {
		this._store.dispatch(new ListProjects());
		this.projects = this._store.select(getProjectEntities);
	}
}
