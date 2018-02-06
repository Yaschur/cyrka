import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { environment } from '../../../environments/environment';

import { JobTypePlain } from '../models/jobtype-plain';

@Injectable()
export class JobtypesApiService {

	constructor(private _httpClient: HttpClient) { }

	public getById(id: string): Observable<JobTypePlain> {
		return this._httpClient.get<JobTypePlain>(`${environment.cyrkaApi.baseUrl}/jobs/types/${id}`);
	}

	public getAll(): Observable<JobTypePlain[]> {
		return this._httpClient.get<JobTypePlain[]>(`${environment.cyrkaApi.baseUrl}/jobs/types`);
	}

	public register(jobType: {
		name: string,
		unit: string,
		rate: number,
		description?: string
	}): Observable<Object> {
		return this._httpClient.post(
			`${environment.cyrkaApi.baseUrl}/jobs/types`,
			jobType
		);
	}

	public change(jobTypeId: string, jobType: {
		name: string,
		unit: string,
		rate: number,
		description?: string
	}): Observable<Object> {
		return this._httpClient.put(
			`${environment.cyrkaApi.baseUrl}/jobs/types/${jobTypeId}`,
			jobType
		);
	}
}
