import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { JobTypesListComponent } from './components/jobtypes-list.component';

const jobTypesRoutes: Routes = [
	{ path: 'jobtypes', component: JobTypesListComponent }
];

@NgModule({
	imports: [RouterModule.forChild(jobTypesRoutes)],
	exports: [RouterModule]
})
export class JobTypesRoutingModule { }
