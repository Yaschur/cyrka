import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JobTypesRoutingModule } from './jobtypes-routing.module';

import { JobTypesListComponent } from './components/jobtypes-list.component';
import { JobtypesApiService } from './services/jobtypes-api.service';

@NgModule({
	imports: [
		CommonModule,
		JobTypesRoutingModule
	],
	declarations: [JobTypesListComponent],
	providers: [JobtypesApiService]
})
export class JobTypesModule { }
