import { Component, Input } from '@angular/core';
import { Project } from '../../models/project';
import { ProjectStatuses } from '../../../shared/projectStatuses/projectStatuses';

@Component({
	selector: 'tr[app-project-list-item]',
	templateUrl: './project-list-item.component.html',
	styleUrls: ['./project-list-item.component.scss'],
})
export class ProjectListItemComponent {
	@Input() project: Project;

	constructor() {}

	getStatusName(stat: ProjectStatuses): string {
		return ProjectStatuses.allStatuses.get(stat);
	}
}
