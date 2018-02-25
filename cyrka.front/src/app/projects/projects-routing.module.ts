import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProjectsComponent } from './components/projects.component';
import { ProjectsFormComponent } from './components/projects-form.component';

const projectRoutes: Routes = [
	{
		path: 'projects', component: ProjectsComponent, children: [
			// { path: '', component: JobTypesListComponent, pathMatch: 'full' },
			{ path: 'register', component: ProjectsFormComponent },
			// { path: ':jobTypeId', component: JobTypesFormComponent }
		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(projectRoutes)],
	exports: [RouterModule]
})
export class ProjectsRoutingModule { }
