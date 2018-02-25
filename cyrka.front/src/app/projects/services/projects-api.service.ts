import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { environment } from '../../../environments/environment';
import {Project} from '../models/project';

@Injectable()
export class ProjectsApiService {
	constructor(private _httpClient: HttpClient) { }

	public getById(id: string): Observable<Project> {
		return this._httpClient.get<Project>(`${environment.cyrkaApi.baseUrl}/projects/${id}`);
	}
}
