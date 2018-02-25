import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ProjectsRoutingModule } from './projects-routing.module';
import { ProjectsComponent } from './components/projects.component';
import { ProjectsFormComponent } from './components/projects-form.component';
import { CustomersApiService } from './services/customers-api.service';
import { ProjectsApiService } from './services/projects-api.service';

@NgModule({
	imports: [
		CommonModule,
		ReactiveFormsModule,
		ProjectsRoutingModule
	],
	declarations: [
		ProjectsComponent,
		ProjectsFormComponent],
	providers: [
		CustomersApiService,
		ProjectsApiService
	]
})
export class ProjectsModule { }
