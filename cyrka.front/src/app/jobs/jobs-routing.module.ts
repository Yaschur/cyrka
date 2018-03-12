import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { JobtypeListComponent } from './components/jobtype-list/jobtype-list.component';

const routes: Routes = [
	{
		path: 'jobtypes',
		component: JobtypeListComponent,
		pathMatch: 'full',
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class JobsRoutingModule {}
