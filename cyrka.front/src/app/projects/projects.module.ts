import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { SharedModule } from '../shared/shared.module';
import { ProjectsRoutingModule } from './projects-routing.module';

import { ProjectsComponent } from './components/projects.component';
import { ProjectsFormComponent } from './components/projects-form.component';
import { JobsApiService } from './services/jobs-api.service';
import { JobApiService } from './services/job-api.service';
import { CustomersApiService } from './services/customers-api.service';
import { ProjectsApiService } from './services/projects-api.service';
import { ProjectApiService } from './services/project-api.service';
import { ProjectsListComponent } from './components/projects-list.component';
import { projectReducer } from './store/project.reducers';
import { ProjectEffects } from './store/project.effects';
import { CustomerEffects } from './store/customer.effects';
import { ProjectListComponent } from './components/project-list/project-list.component';
import { ProjectListItemComponent } from './components/project-list-item/project-list-item.component';
import { ProjectItemComponent } from './components/project-item/project-item.component';
import { ProjectFormComponent } from './components/project-form/project-form.component';
import { ProjectComponent } from './components/project/project.component';
import { ProjectProductComponent } from './components/project-product/project-product.component';
import { ProjectProductFormComponent } from './components/project-product-form/project-product-form.component';
import { CustomerApiService } from './services/customer-api.service';
import { ProjectJobComponent } from './components/project-job/project-job.component';
import { ProjectJobListComponent } from './components/project-job-list/project-job-list.component';
import { ProjectJobFormComponent } from './components/project-job-form/project-job-form.component';
import { JobtypeEffects } from './store/jobtype.effects';

@NgModule({
	imports: [
		CommonModule,
		ReactiveFormsModule,
		SharedModule,
		StoreModule.forFeature('project', projectReducer),
		EffectsModule.forFeature([ProjectEffects, CustomerEffects, JobtypeEffects]),
		ProjectsRoutingModule,
	],
	declarations: [
		ProjectsComponent,
		ProjectsFormComponent,
		ProjectsListComponent,
		ProjectListComponent,
		ProjectListItemComponent,
		ProjectItemComponent,
		ProjectFormComponent,
		ProjectComponent,
		ProjectProductComponent,
		ProjectProductFormComponent,
		ProjectJobComponent,
		ProjectJobListComponent,
		ProjectJobFormComponent,
	],
	providers: [
		JobsApiService,
		JobApiService,
		CustomersApiService,
		ProjectsApiService,
		ProjectApiService,
		CustomerApiService,
	],
})
export class ProjectsModule {}
