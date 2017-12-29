import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

import { Project } from '../models/Project';
import { environment } from '../../../environments/environment';

@Component({
	selector: 'app-project-list',
	templateUrl: './project-list.component.html',
	styles: []
})
export class ProjectListComponent implements OnInit {
	projects: Project[];

	constructor(
		private _http: HttpClient,
		private _router: Router
	) { }

	ngOnInit() {
		this._http.get<Project[]>(environment.cyrkaApiBase + '/v1/projects')
			.subscribe(
			data => this.projects = data,
			err => console.log(err)
			);
	}

	onNew(): void {
		this._router.navigate(['projects', 'add']);
	}
	onEdit(id: string): void {
		this._router.navigate(['projects', id]);
	}

}
