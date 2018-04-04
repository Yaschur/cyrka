import { Component, Input } from '@angular/core';

import { ProjectStatuses } from '../../../shared/projectStatuses/projectStatuses';

@Component({
	selector: 'div[app-project-status]',
	templateUrl: './project-status.component.html',
	styleUrls: ['./project-status.component.scss'],
})
export class ProjectStatusComponent {
	@Input()
	set status(stat: ProjectStatuses) {
		if (stat) {
			this.availableActions = ProjectStatuses.getActions(stat);
			this.statusName = ProjectStatuses.allStatuses.get(stat);
		}
	}

	availableActions;
	statusName;

	constructor() {
		this.availableActions = [];
	}

	setStatus(stat: ProjectStatuses) {
		this.status = stat;
	}
}
