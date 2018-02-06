import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JobTypesRoutingModule } from './jobtypes-routing.module';

import { JobTypesListComponent } from './components/jobtypes-list.component';
import { JobTypesApiService } from './services/jobtypes-api.service';

@NgModule({
	imports: [
		CommonModule,
		JobTypesRoutingModule
	],
	declarations: [JobTypesListComponent],
	providers: [JobTypesApiService]
})
export class JobTypesModule { }
