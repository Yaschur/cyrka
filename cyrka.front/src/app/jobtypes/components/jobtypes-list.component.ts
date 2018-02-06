import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { Observable } from 'rxjs/Observable';

import { JobTypesApiService } from '../services/jobtypes-api.service';
import { JobTypePlain } from '../models/jobtype-plain';

@Component({
	selector: 'app-jobtypes-list',
	templateUrl: './jobtypes-list.component.html',
	styleUrls: ['./jobtypes-list.component.scss']
})
export class JobTypesListComponent {

	constructor(
		private _jobTypesApi: JobTypesApiService,
		private _router: Router
	) {
		this.jobTypes = _jobTypesApi.getAll();
	}

	public jobTypes: Observable<JobTypePlain[]>;
}
