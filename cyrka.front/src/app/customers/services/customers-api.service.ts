import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { environment } from '../../../environments/environment';

import { CustomerPlain } from '../models/customer-plain.model';

@Injectable()
export class CustomersApiService {
	constructor(private _httpClient: HttpClient) { }

	public getAll(): Observable<CustomerPlain[]> {
		return this._httpClient.get<CustomerPlain[]>(environment.cyrkaApi.baseUrl + '/customers');
	}

	public register(customer: { name: string, description?: string }): Observable<Object> {
		return this._httpClient.post(
			environment.cyrkaApi.baseUrl + '/customers',
			customer
		);
	}
}
