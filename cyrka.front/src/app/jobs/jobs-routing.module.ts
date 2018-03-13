import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { JobtypeListComponent } from './components/jobtype-list/jobtype-list.component';
import { JobtypeItemComponent } from './components/jobtype-item/jobtype-item.component';

const routes: Routes = [
	{
		path: 'jobtypes',
		component: JobtypeListComponent,
		pathMatch: 'full',
	},
	{
		path: 'jobtypes/:jobTypeId',
		component: JobtypeItemComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class JobsRoutingModule {}
