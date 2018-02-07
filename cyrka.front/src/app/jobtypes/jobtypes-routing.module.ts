import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { JobTypesListComponent } from './components/jobtypes-list.component';
import { JobTypesItemResolver } from './services/jobtypes-item-resolver.service';
import { JobTypesFormComponent } from './components/jobtypes-form.component';
import { JobTypesComponent } from './components/jobtypes.component';

const jobTypesRoutes: Routes = [
	{
		path: 'jobtypes', component: JobTypesComponent, children: [
			{ path: '', component: JobTypesListComponent, pathMatch: 'full' },
			{ path: ':jobTypeId', resolve: { jobType: JobTypesItemResolver }, component: JobTypesFormComponent }
		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(jobTypesRoutes)],
	exports: [RouterModule],
	providers: [JobTypesItemResolver]
})
export class JobTypesRoutingModule { }
