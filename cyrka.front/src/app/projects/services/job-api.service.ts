import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Jobtype } from '../models/job-type';

@Injectable()
export class JobApiService {
	constructor(private _httpClient: HttpClient) {}

	public fetchAll(): Observable<Jobtype[]> {
		return this._httpClient.get<Jobtype[]>(
			`${environment.cyrkaApi.baseUrl}/jobs/types`
		);
	}
}
