import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { environment } from '../../../environments/environment';
import { Project } from '../models/project';
import { ApiAnswer } from '../models/api-answer';
import { ProductSet } from '../models/product-set';

@Injectable()
export class ProjectsApiService {
	constructor(private _httpClient: HttpClient) { }

	public getById(id: string): Observable<Project> {
		return this._httpClient.get<Project>(`${environment.cyrkaApi.baseUrl}/projects/${id}`);
	}

	public register(): Observable<ApiAnswer> {
		return this._httpClient.post<ApiAnswer>(`${environment.cyrkaApi.baseUrl}/projects`, {});
	}

	public setCustomer(id: string, productSet: ProductSet): Observable<ApiAnswer> {
		return this._httpClient.post<ApiAnswer>(
			`${environment.cyrkaApi.baseUrl}/projects/${id}`,
			productSet
		);
	}
}
