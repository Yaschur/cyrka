import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

const newKey = 'new';

@Component({
	selector: 'app-projects',
	templateUrl: './projects.component.html',
	styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {

	constructor(
		private _route: ActivatedRoute
	) {
		this.wzOpen = false;
	}

	public wzOpen: boolean;

	public ngOnInit(): void {
		this._route.fragment
			.subscribe(fragment => this.wzOpen = fragment === newKey);
	}

}
