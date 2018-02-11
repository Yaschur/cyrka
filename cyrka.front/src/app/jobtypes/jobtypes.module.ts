import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { JobTypesRoutingModule } from './jobtypes-routing.module';

import { JobTypesApiService } from './services/jobtypes-api.service';
import { JobTypesComponent } from './components/jobtypes.component';
import { JobTypesListComponent } from './components/jobtypes-list.component';
import { JobTypesFormComponent } from './components/jobtypes-form.component';

@NgModule({
	imports: [
		CommonModule,
		ReactiveFormsModule,
		JobTypesRoutingModule
	],
	declarations: [
		JobTypesComponent,
		JobTypesListComponent,
		JobTypesFormComponent
	],
	providers: [JobTypesApiService]
})
export class JobTypesModule { }
