import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';

import { UnitDescriptor } from './unit-descriptor';

const UNITS: UnitDescriptor[] = [
	{ key: 'Undefined', title: 'Не определено', short: '?' },
	{ key: 'Piece', title: 'Штука/ единица', short: 'ед.' },
	{ key: 'Minute', title: 'Минута', short: 'мин.' },
	{ key: 'Character', title: 'Персонаж', short: 'перс.' },
	{ key: 'Symbol', title: 'Символ (текста)', short: 'симв.' },
	{ key: 'Gigabyte', title: 'Гигабайт', short: 'Гб.' },
];

@Injectable()
export class UnitService {
	public getAll(): Observable<UnitDescriptor[]> {
		return Observable.of(UNITS);
	}

	public getByName(unitName: string): Observable<UnitDescriptor> {
		const res = UNITS.find(u => u.key === unitName);
		return Observable.of(res || UNITS[0]);
	}
}