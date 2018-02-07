import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/take';


import { JobTypePlain } from '../models/jobtype-plain';
import { JobTypesApiService } from './jobtypes-api.service';

@Injectable()
export class JobTypesItemResolver implements Resolve<JobTypePlain> {

	constructor(private _jobTypesApi: JobTypesApiService) { }

	public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<JobTypePlain> {
		const id = route.paramMap.get('jobTypeId');
		return id ? this._jobTypesApi.getById(id).take(1) : null;
	}
}
