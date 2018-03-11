import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { JobsRoutingModule } from './jobs-routing.module';
import { JobTypesApiService } from './services/jobtypes-api.service';
import { jobTypesReducer } from './store/job-types.reducers';
import { JobTypesEffects } from './store/job-types.effects';
import { JobTypesListComponent } from './components/jobtypes-list.component';

@NgModule({
	imports: [
		CommonModule,
		JobsRoutingModule,
		StoreModule.forFeature('jobTypes', jobTypesReducer),
		EffectsModule.forFeature([JobTypesEffects]),
		JobsRoutingModule,
	],
	declarations: [JobTypesListComponent],
	providers: [JobTypesApiService],
})
export class JobsModule {}
