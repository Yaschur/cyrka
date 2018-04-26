import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { NgxsModule } from '@ngxs/store';

import { JobsRoutingModule } from './jobs-routing.module';
import { SharedModule } from '../shared/shared.module';

import { JobtypeApiService } from './services/jobtype-api.service';
import { JobtypeListComponent } from './components/jobtype-list/jobtype-list.component';
import { JobtypeMenuComponent } from './components/jobtype-menu/jobtype-menu.component';
import { JobtypeComponent } from './components/jobtype/jobtype.component';
import { JobtypeItemComponent } from './components/jobtype-item/jobtype-item.component';
import { JobtypeFormComponent } from './components/jobtype-form/jobtype-form.component';
import { JobtypeState } from './store/jobtype.state';

@NgModule({
	imports: [
		CommonModule,
		ReactiveFormsModule,
		SharedModule,
		NgxsModule.forFeature([JobtypeState]),
		JobsRoutingModule,
	],
	declarations: [
		JobtypeListComponent,
		JobtypeMenuComponent,
		JobtypeComponent,
		JobtypeItemComponent,
		JobtypeFormComponent,
	],
	providers: [JobtypeApiService],
})
export class JobsModule {}
