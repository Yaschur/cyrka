import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { JobtypeListComponent } from './components/jobtype-list/jobtype-list.component';
import { JobtypeItemComponent } from './components/jobtype-item/jobtype-item.component';
import { JobtypeFormComponent } from './components/jobtype-form/jobtype-form.component';

const routes: Routes = [
	{
		path: 'jobtypes',
		component: JobtypeListComponent,
		pathMatch: 'full',
	},
	{
		path: 'jobtypes/register',
		component: JobtypeFormComponent,
		pathMatch: 'full',
	},
	{
		path: 'jobtypes/:jobtypeId',
		component: JobtypeItemComponent,
		pathMatch: 'full',
	},
	{
		path: 'jobtypes/:jobtypeId/edit',
		component: JobtypeFormComponent,
		pathMatch: 'full',
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class JobsRoutingModule {}
