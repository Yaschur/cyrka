import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable } from 'rxjs/Observable';

import { Project } from '../models/project';
import { CustomersApiService } from '../services/customers-api.service';
import { ProjectsApiService } from '../services/projects-api.service';

@Component({
	selector: 'app-projects-form',
	templateUrl: './projects-form.component.html',
	styleUrls: ['./projects-form.component.scss']
})
export class ProjectsFormComponent implements OnInit {

	constructor(
		private _formBuilder: FormBuilder,
		private _projectApi: ProjectsApiService,
		private _customerApi: CustomersApiService,
		private _route: ActivatedRoute,
		private _router: Router
	) {
		this.form = this._formBuilder.group({
			'product': this._formBuilder.group({
				'customer': ['', Validators.required],
				'title': ['', Validators.required],
				'episodeNumber': [0, Validators.required],
				'episodeDuration': [0, Validators.required]
			})
		});
	}

	public form: FormGroup;
	public formTitle: string;
	public submitTitle: string;

	public ngOnInit() {
		this._route.params
			.switchMap(p => p['projectId'] ? this._projectApi.getById(p['projectId']) : Observable.of(<Project>{}))
			.subscribe(proj => {
				this._id = proj.id;
				this.formTitle = this._id ? 'изменение данных проекта' : 'создание нового проекта';
				this.submitTitle = this._id ? 'изменить' : 'создать';
			});
	}

	public onSave() {
		// this.form.updateValueAndValidity();
		// if (this.form.invalid || this.form.pristine) {
		// 	return;
		// }
		// (this._id ? this._api.change(this._id, this.form.value) : this._api.register(this.form.value))
		// 	.subscribe(() => this.onCancel());
	}

	public onCancel() {
		this._router.navigate(['..'], { relativeTo: this._route });
	}

	private _id: string;

}
