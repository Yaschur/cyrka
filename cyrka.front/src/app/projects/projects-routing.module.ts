import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProjectListComponent } from './components/project-list/project-list.component';
import { ProjectComponent } from './components/project/project.component';
import { ProjectItemComponent } from './components/project-item/project-item.component';

const projectRoutes: Routes = [
	{
		path: 'projects',
		component: ProjectComponent,
		children: [
			{ path: '', component: ProjectListComponent, pathMatch: 'full' },
			{ path: 'register', component: ProjectItemComponent },
			{ path: ':projectId', component: ProjectItemComponent },
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(projectRoutes)],
	exports: [RouterModule],
})
export class ProjectsRoutingModule {}
