import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { JobTypesListComponent } from './components/jobtypes-list.component';
import { JobTypesFormComponent } from './components/jobtypes-form.component';

const jobTypesRoutes: Routes = [
	{ path: 'jobtypes', component: JobTypesListComponent },
	{ path: 'jobtypes/:jobTypeId', component: JobTypesFormComponent }
];

@NgModule({
	imports: [RouterModule.forChild(jobTypesRoutes)],
	exports: [RouterModule]
})
export class JobTypesRoutingModule { }
