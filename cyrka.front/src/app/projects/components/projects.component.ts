import { Component } from '@angular/core';

import { ClrWizard } from '@clr/angular';

@Component({
	selector: 'app-projects',
	templateUrl: './projects.component.html',
	styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent {

	constructor() {
		this.wzOpen = false;
	}

	public wzOpen: boolean;
}
