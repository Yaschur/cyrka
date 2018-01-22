import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { environment } from '../../../environments/environment';

import { CustomerPlain } from '../models/customer-plain.model';

@Injectable()
export class CustomersApiService {
	constructor(private _httpClient: HttpClient) { }

	public getById(id: string): Observable<CustomerPlain> {
		return this._httpClient.get<CustomerPlain>(`${environment.cyrkaApi.baseUrl}/customers/${id}`);
	}

	public getAll(): Observable<CustomerPlain[]> {
		return this._httpClient.get<CustomerPlain[]>(`${environment.cyrkaApi.baseUrl}/customers`);
	}

	public register(customer: { name: string, description?: string }): Observable<Object> {
		return this._httpClient.post(
			`${environment.cyrkaApi.baseUrl}/customers`,
			customer
		);
	}

	public change(customerId: string, customer: { name: string, description?: string }): Observable<Object> {
		return this._httpClient.put(
			`${environment.cyrkaApi.baseUrl}/customers/${customerId}`,
			customer
		);
	}

	public addTitle(customerId: string, title: { name: string, numberOfSeries: number, description?: string }): Observable<Object> {
		return this._httpClient.post(
			`${environment.cyrkaApi.baseUrl}/customers/${customerId}/titles`,
			title
		);
	}
}
