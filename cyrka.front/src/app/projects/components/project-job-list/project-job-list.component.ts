import { Component, Input } from '@angular/core';

import { JobSet } from '../../models/job-set';

@Component({
	selector: 'div[app-project-job-list]',
	templateUrl: './project-job-list.component.html',
	styleUrls: ['./project-job-list.component.scss'],
})
export class ProjectJobListComponent {
	@Input() jobs: JobSet[];

	editId: string;

	constructor() {
		this.clearEditMode();
	}

	setEditModeTo(id: string) {
		this.editId = id;
	}
	clearEditMode() {
		this.editId = '';
	}
}
