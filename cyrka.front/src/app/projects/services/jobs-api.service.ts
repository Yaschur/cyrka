import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { environment } from '../../../environments/environment';
import { JobType } from '../models/job-type';

@Injectable()
export class JobsApiService {

	constructor(private _httpClient: HttpClient) { }

	public getAll(): Observable<JobType[]> {
		return this._httpClient.get<JobType[]>(`${environment.cyrkaApi.baseUrl}/jobs/types`);
	}
}
