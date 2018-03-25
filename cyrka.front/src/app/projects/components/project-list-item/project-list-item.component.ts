import { Component, Input } from '@angular/core';
import { Project } from '../../models/project';

@Component({
	selector: 'tr[app-project-list-item]',
	templateUrl: './project-list-item.component.html',
	styleUrls: ['./project-list-item.component.scss'],
})
export class ProjectListItemComponent {
	@Input() project: Project;

	constructor() {}
}
