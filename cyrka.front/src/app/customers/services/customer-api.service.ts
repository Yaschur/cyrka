import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Customer } from '../models/customer';
import { environment } from '../../../environments/environment';

@Injectable()
export class CustomerApiService {
	constructor(private _httpClient: HttpClient) {}

	public getById(id: string): Observable<Customer> {
		return this._httpClient.get<Customer>(
			`${environment.cyrkaApi.baseUrl}/customers/${id}`
		);
	}

	public fetchAll(): Observable<Customer[]> {
		return this._httpClient.get<Customer[]>(
			`${environment.cyrkaApi.baseUrl}/customers`
		);
	}

	public register(customer: {
		name: string;
		description?: string;
	}): Observable<Object> {
		return this._httpClient.post(
			`${environment.cyrkaApi.baseUrl}/customers`,
			customer
		);
	}

	public retire(customerId: string) {
		return this._httpClient.delete(
			`${environment.cyrkaApi.baseUrl}/customers/${customerId}`
		);
	}

	public change(
		customerId: string,
		customer: { name: string; description?: string }
	): Observable<Object> {
		return this._httpClient.put(
			`${environment.cyrkaApi.baseUrl}/customers/${customerId}`,
			customer
		);
	}

	public addTitle(
		customerId: string,
		title: { name: string; numberOfSeries: number; description?: string }
	): Observable<Object> {
		return this._httpClient.post(
			`${environment.cyrkaApi.baseUrl}/customers/${customerId}/titles`,
			title
		);
	}

	public changeTitle(
		customerId: string,
		titleId: string,
		title: { name: string; numberOfSeries: number; description?: string }
	): Observable<Object> {
		return this._httpClient.put(
			`${
				environment.cyrkaApi.baseUrl
			}/customers/${customerId}/titles/${titleId}`,
			title
		);
	}

	public removeTitle(customerId: string, titleId: string): Observable<Object> {
		return this._httpClient.delete(
			`${
				environment.cyrkaApi.baseUrl
			}/customers/${customerId}/titles/${titleId}`
		);
	}
}
