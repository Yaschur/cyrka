import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JobTypesRoutingModule } from './jobtypes-routing.module';

import { JobTypesApiService } from './services/jobtypes-api.service';
import { JobTypesListComponent } from './components/jobtypes-list.component';
import { JobTypesFormComponent } from './components/jobtypes-form.component';

@NgModule({
	imports: [
		CommonModule,
		JobTypesRoutingModule
	],
	declarations: [
		JobTypesListComponent,
		JobTypesFormComponent
	],
	providers: [JobTypesApiService]
})
export class JobTypesModule { }
