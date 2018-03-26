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
import { projectReducer } from './store/project.reducers';
import { ProjectEffects } from './store/project.effects';
import { ProjectListComponent } from './components/project-list/project-list.component';
import { ProjectListItemComponent } from './components/project-list-item/project-list-item.component';
import { ProjectItemComponent } from './components/project-item/project-item.component';
import { ProjectFormComponent } from './components/project-form/project-form.component';

@NgModule({
	imports: [
		CommonModule,
		ReactiveFormsModule,
		SharedModule,
		StoreModule.forFeature('project', projectReducer),
		EffectsModule.forFeature([ProjectEffects]),
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
	],
	providers: [
		JobsApiService,
		CustomersApiService,
		ProjectsApiService,
		ProjectApiService,
	],
})
export class ProjectsModule {}
