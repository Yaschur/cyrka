import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { JobTypePlain } from '../models/jobtype-plain';

@Component({
	selector: 'app-jobtypes-form',
	templateUrl: './jobtypes-form.component.html',
	styleUrls: ['./jobtypes-form.component.scss']
})
export class JobTypesFormComponent implements OnInit {

	constructor(
		private _route: ActivatedRoute
	) {
		this.jt = <JobTypePlain>{};
	}

	public jt: JobTypePlain;

	public ngOnInit() {
		this._route.data
			.subscribe((data: {jobType: JobTypePlain}) => this.jt = data.jobType);
	}

}
