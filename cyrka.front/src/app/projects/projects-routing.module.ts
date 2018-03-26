import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProjectsComponent } from './components/projects.component';
import { ProjectsFormComponent } from './components/projects-form.component';
import { ProjectsListComponent } from './components/projects-list.component';
import { ProjectListComponent } from './components/project-list/project-list.component';
import { ProjectFormComponent } from './components/project-form/project-form.component';

const projectRoutes: Routes = [
	{ path: 'projects', component: ProjectListComponent, pathMatch: 'full' },
	{
		path: 'projects/:projectId',
		component: ProjectFormComponent,
		pathMatch: 'full',
	},
	{
		path: 'projects',
		component: ProjectsComponent,
		children: [
			{ path: '', component: ProjectsListComponent, pathMatch: 'full' },
			{ path: 'register', component: ProjectsFormComponent },
			{ path: ':projectId', component: ProjectsFormComponent },
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(projectRoutes)],
	exports: [RouterModule],
})
export class ProjectsRoutingModule {}
