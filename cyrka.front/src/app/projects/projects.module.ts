import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { ClarityModule } from '@clr/angular';

import { ProjectsRoutingModule } from './projects-routing.module';
import { ProjectsComponent } from './components/projects.component';

@NgModule({
	imports: [
		CommonModule,
		NoopAnimationsModule,
		ClarityModule,
		ProjectsRoutingModule
	],
	declarations: [ProjectsComponent]
})
export class ProjectsModule { }
