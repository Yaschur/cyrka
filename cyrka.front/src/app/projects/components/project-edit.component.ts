import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

import { ProjectType } from '../models/ProjectType';
import { Project } from '../models/Project';

@Component({
	selector: 'app-project-edit',
	templateUrl: './project-edit.component.html',
	styles: []
})
export class ProjectEditComponent implements OnInit {
	projectForm: FormGroup;
	projectTypes: string[] = [];
	projectId: string;

	constructor(
		private _fb: FormBuilder,
		private _http: HttpClient,
		private _location: Location,
		private _route: ActivatedRoute
	) {
		this.projectForm = this._fb.group({
			'id': this.generateId(),
			'name': ['', Validators.required],
			'type': ['', Validators.required],
			'plannedFinish': '',
			'estimatedFinish': ''
		});
		for (const pt in ProjectType) {
			if (pt !== '0' && !Number(pt)) {
				this.projectTypes.push(pt);
			}
		}
	}

	ngOnInit() {
		this._route.params
			.switchMap(params => {
				const id = params['id'];
				return id == null ? null : this._http.get<Project>('http://localhost:5000/v1/projects/' + id);
			})
			.subscribe(project => {
				this.projectId = project ? project.id : null;
				if (project == null) {
					return;
				}
				this.projectForm.setValue({
					id: project.id,
					name: project.name,
					type: project.type,
					plannedFinish: project.plannedFinish ? project.plannedFinish.toString().substr(0, 10) : '',
					estimatedFinish: project.estimatedFinish ? project.estimatedFinish.toString().substr(0, 10) : ''
				});
			});
	}

	onSubmit(): void {
		this._http.post(
			'http://localhost:5000/v1/projects',
			this.projectForm.value
		).subscribe(() => this._location.back());
	}
	onCancel(): void {
		this._location.back();
	}
	onDelete(): void {
		this._http.delete(
			'http://localhost:5000/v1/projects/' + this.projectId
		).subscribe(() => this._location.back());
	}

	private generateId(): string {
		let text = '';
		const possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

		for (let i = 0; i < 8; i++) {
			text += possible.charAt(Math.floor(Math.random() * possible.length));
		}

		return text;
	}
}
