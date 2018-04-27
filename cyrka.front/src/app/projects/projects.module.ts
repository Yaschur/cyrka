import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { NgxsModule } from '@ngxs/store';
import { SharedModule } from '../shared/shared.module';
import { ProjectsRoutingModule } from './projects-routing.module';

import { JobApiService } from './services/job-api.service';
import { ProjectApiService } from './services/project-api.service';
import { ProjectListComponent } from './components/project-list/project-list.component';
import { ProjectListItemComponent } from './components/project-list-item/project-list-item.component';
import { ProjectItemComponent } from './components/project-item/project-item.component';
import { ProjectComponent } from './components/project/project.component';
import { ProjectProductComponent } from './components/project-product/project-product.component';
import { ProjectProductFormComponent } from './components/project-product-form/project-product-form.component';
import { CustomerApiService } from './services/customer-api.service';
import { ProjectJobComponent } from './components/project-job/project-job.component';
import { ProjectJobListComponent } from './components/project-job-list/project-job-list.component';
import { ProjectJobFormComponent } from './components/project-job-form/project-job-form.component';
import { ProjectStatusComponent } from './components/project-status/project-status.component';
import { ProjectTurnoverComponent } from './components/project-turnover/project-turnover.component';
import { ProjectState } from './store/project.state';

@NgModule({
	imports: [
		CommonModule,
		ReactiveFormsModule,
		SharedModule,
		NgxsModule.forRoot([ProjectState]),
		ProjectsRoutingModule,
	],
	declarations: [
		ProjectListComponent,
		ProjectListItemComponent,
		ProjectItemComponent,
		ProjectComponent,
		ProjectProductComponent,
		ProjectProductFormComponent,
		ProjectJobComponent,
		ProjectJobListComponent,
		ProjectJobFormComponent,
		ProjectStatusComponent,
		ProjectTurnoverComponent,
	],
	providers: [JobApiService, ProjectApiService, CustomerApiService],
})
export class ProjectsModule {}
