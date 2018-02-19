import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProjectsComponent } from './components/projects.component';

const projectRoutes: Routes = [
	{
		path: 'projects', component: ProjectsComponent
	}
];

@NgModule({
	imports: [RouterModule.forChild(projectRoutes)],
	exports: [RouterModule]
})
export class ProjectsRoutingModule { }
