import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { JobTypesListComponent } from './components/jobtypes-list.component';

const routes: Routes = [
	{
		path: 'jobtypes',
		component: JobTypesListComponent,
		pathMatch: 'full',
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class JobsRoutingModule {}
