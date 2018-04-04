import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { environment } from '../../../environments/environment';
import { Project } from '../models/project';
import { ApiAnswer } from '../../shared/api/api-answer';
import { ProductSet } from '../models/product-set';
import { JobSet } from '../models/job-set';
import { JobChange } from '../models/job-change';
import { ProjectStatuses } from '../../shared/projectStatuses/projectStatuses';

@Injectable()
export class ProjectApiService {
	constructor(private _httpClient: HttpClient) {}

	public fetchAll(): Observable<Project[]> {
		return this._httpClient.get<Project[]>(
			`${environment.cyrkaApi.baseUrl}/projects`
		);
	}

	public getById(id: string): Observable<Project> {
		return this._httpClient.get<Project>(
			`${environment.cyrkaApi.baseUrl}/projects/${id}`
		);
	}

	public register(): Observable<ApiAnswer> {
		return this._httpClient.post<ApiAnswer>(
			`${environment.cyrkaApi.baseUrl}/projects`,
			{}
		);
	}

	public setProduct(id: string, productSet: ProductSet): Observable<ApiAnswer> {
		return this._httpClient.post<ApiAnswer>(
			`${environment.cyrkaApi.baseUrl}/projects/${id}/product`,
			productSet
		);
	}

	public setJob(id: string, jobSet: JobSet): Observable<ApiAnswer> {
		return this._httpClient.post<ApiAnswer>(
			`${environment.cyrkaApi.baseUrl}/projects/${id}/job`,
			jobSet
		);
	}

	public setStatus(id: string, status: ProjectStatuses): Observable<ApiAnswer> {
		return this._httpClient.post<ApiAnswer>(
			`${environment.cyrkaApi.baseUrl}/projects/${id}/status`,
			{ status: status }
		);
	}

	public changeJob(
		id: string,
		jobId: string,
		jobChange: JobChange
	): Observable<ApiAnswer> {
		return this._httpClient.put<ApiAnswer>(
			`${environment.cyrkaApi.baseUrl}/projects/${id}/jobs/${jobId}`,
			jobChange
		);
	}
}
