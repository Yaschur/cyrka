import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

import { Jobtype } from '../models/jobtype';
import { ApiAnswer } from '../../shared/api/api-answer';

@Injectable()
export class JobtypeApiService {
	constructor(private _httpClient: HttpClient) {}

	public getById(id: string): Observable<Jobtype> {
		return this._httpClient.get<Jobtype>(
			`${environment.cyrkaApi.baseUrl}/jobs/types/${id}`
		);
	}

	public fetchAll(): Observable<Jobtype[]> {
		return this._httpClient.get<Jobtype[]>(
			`${environment.cyrkaApi.baseUrl}/jobs/types`
		);
	}

	public register(jobType: {
		name: string;
		unit: string;
		rate: number;
		description?: string;
	}): Observable<ApiAnswer> {
		return this._httpClient.post<ApiAnswer>(
			`${environment.cyrkaApi.baseUrl}/jobs/types`,
			jobType
		);
	}

	public change(
		jobtypeId: string,
		jobtype: {
			name: string;
			unit: string;
			rate: number;
			description?: string;
		}
	): Observable<ApiAnswer> {
		return this._httpClient.put<ApiAnswer>(
			`${environment.cyrkaApi.baseUrl}/jobs/types/${jobtypeId}`,
			jobtype
		);
	}
}
