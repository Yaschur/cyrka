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
import { CustomersApiService } from './services/customers-api.service';
import { ProjectsApiService } from './services/projects-api.service';
import { ProjectApiService } from './services/project-api.service';
import { ProjectsListComponent } from './components/projects-list.component';
import { ProjectMenuComponent } from './components/project-menu/project-menu.component';

@NgModule({
	imports: [
		CommonModule,
		ReactiveFormsModule,
		SharedModule,
		ProjectsRoutingModule,
	],
	declarations: [
		ProjectsComponent,
		ProjectsFormComponent,
		ProjectsListComponent,
		ProjectMenuComponent,
	],
	providers: [
		JobsApiService,
		CustomersApiService,
		ProjectsApiService,
		ProjectApiService,
	],
})
export class ProjectsModule {}
