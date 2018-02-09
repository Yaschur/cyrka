import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { JobTypePlain } from '../models/jobtype-plain';
import { JobTypesApiService } from '../services/jobtypes-api.service';
import { Observable } from 'rxjs/Observable';

@Component({
	selector: 'app-jobtypes-form',
	templateUrl: './jobtypes-form.component.html',
	styleUrls: ['./jobtypes-form.component.scss']
})
export class JobTypesFormComponent implements OnInit {

	constructor(
		private _route: ActivatedRoute,
		private _api: JobTypesApiService
	) { }

	public ngOnInit() {
		this._route.params
			.switchMap(p => p['jobTypesId'] ? Observable.of(<JobTypePlain>{}) : this._api.getById(p['jobTypesId']))
			.subscribe(jt => { });
	}

}
