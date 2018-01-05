import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { environment } from '../../../environments/environment';

import { CustomerPlain } from '../models/customer-plain.model';

@Injectable()
export class CustomersApiService {
	constructor(private _http: HttpClient) { }

	public getAll(): Observable<CustomerPlain[]> {
		return this._http.get<CustomerPlain[]>(environment.cyrkaApi.baseUrl + '/customers');
	}
}
