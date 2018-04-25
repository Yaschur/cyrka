import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Customer } from '../models/customer';

@Injectable()
export class CustomerApiService {
	constructor(private _httpClient: HttpClient) {}

	public fetchAll(): Observable<Customer[]> {
		return this._httpClient.get<Customer[]>(
			`${environment.cyrkaApi.baseUrl}/customers`
		);
	}
}
