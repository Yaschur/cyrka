import { Component } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Project } from '../../models/project';
import { Observable } from 'rxjs/Observable';

@Component({
	selector: 'app-project-form',
	templateUrl: './project-form.component.html',
	styleUrls: ['./project-form.component.scss'],
})
export class ProjectFormComponent {
	form: FormGroup;
	formTitle: string;
	submitTitle: string;

	prjChanges$: Observable<Project>;

	constructor(private _formBuilder: FormBuilder) {
		
	}
}
