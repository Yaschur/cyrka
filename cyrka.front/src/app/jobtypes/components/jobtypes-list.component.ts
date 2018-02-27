import { Component } from '@angular/core';

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
		private _jobTypesApi: JobTypesApiService
	) {
		this.jobTypes = _jobTypesApi.getAll();
	}

	public jobTypes: Observable<JobTypePlain[]>;
}
