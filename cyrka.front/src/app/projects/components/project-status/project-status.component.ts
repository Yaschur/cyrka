import { Component, Input } from '@angular/core';

import { Store } from '@ngxs/store';

import { ProjectStatuses } from '../../../shared/projectStatuses/projectStatuses';
import { SetStatus } from '../../store/project.actions';

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

	constructor(private _store: Store) {
		this.availableActions = [];
	}

	setStatus(stat: ProjectStatuses) {
		this._store.dispatch(new SetStatus(stat));
	}
}
