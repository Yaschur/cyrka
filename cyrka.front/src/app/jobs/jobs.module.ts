import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { JobsRoutingModule } from './jobs-routing.module';
import { JobtypeApiService } from './services/jobtype-api.service';
import { jobtypeReducer } from './store/jobtype.reducers';
import { JobtypeEffects } from './store/jobtype.effects';
import { JobtypeListComponent } from './components/jobtype-list/jobtype-list.component';
import { JobtypeMenuComponent } from './components/jobtype-menu/jobtype-menu.component';

@NgModule({
	imports: [
		CommonModule,
		JobsRoutingModule,
		StoreModule.forFeature('jobtype', jobtypeReducer),
		EffectsModule.forFeature([JobtypeEffects]),
		JobsRoutingModule,
	],
	declarations: [JobtypeListComponent, JobtypeMenuComponent],
	providers: [JobtypeApiService],
})
export class JobsModule {}
