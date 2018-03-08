import { Component } from '@angular/core';

import { Observable } from 'rxjs/Observable';

import { JobTypesApiService } from '../services/jobtypes-api.service';
import { JobTypePlain } from '../models/jobtype-plain';
import { UnitDescriptor } from '../../shared/units/unit-descriptor';
import { UnitService } from '../../shared/units/unit.service';

interface JobTypeWithUnit extends JobTypePlain {
	unitDescriptor: UnitDescriptor;
}

@Component({
	selector: 'app-jobtypes-list',
	templateUrl: './jobtypes-list.component.html',
	styleUrls: ['./jobtypes-list.component.scss'],
})
export class JobTypesListComponent {
	public jobTypes: Observable<JobTypeWithUnit[]>;

	constructor(
		private _jobTypesApi: JobTypesApiService,
		private _unitSrv: UnitService
	) {
		this.jobTypes = _jobTypesApi
			.getAll()
			.zip(this._unitSrv.getAll(), (jts, units) =>
				jts.map(jt => {
					const jtu = <JobTypeWithUnit>jt;
					jtu.unitDescriptor = units.find(u => u.key === jtu.unit) || units[0];
					return jtu;
				})
			);
	}
}
