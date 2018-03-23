import { Component } from '@angular/core';

import { Observable } from 'rxjs/Observable';

import { ProjectsApiService } from '../services/projects-api.service';
import { ProjectPlain } from '../models/project-plain';

@Component({
	selector: 'app-projects-list',
	templateUrl: './projects-list.component.html',
	styleUrls: ['./projects-list.component.scss'],
})
export class ProjectsListComponent {
	public projects: Observable<ProjectPlain[]>;

	constructor(private _projectsApi: ProjectsApiService) {
		this.projects = this._projectsApi.getAll();
	}
}
