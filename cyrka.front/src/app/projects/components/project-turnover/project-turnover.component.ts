import { Component, Input } from '@angular/core';

import { Project } from '../../models/project';

@Component({
	selector: 'div[app-project-turnover]',
	templateUrl: './project-turnover.component.html',
	styleUrls: ['./project-turnover.component.scss'],
})
export class ProjectTurnoverComponent {
	@Input() project: Project;

	paymentsEditMode: boolean;

	constructor() {
		this.paymentsEditMode = false;
	}
}
