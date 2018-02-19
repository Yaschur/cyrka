import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { ClarityModule } from '@clr/angular';

import { AppRoutingModule } from './app-routing.module';
import { CustomersModule } from './customers/customers.module';
import { JobTypesModule } from './jobtypes/jobtypes.module';
import { ProjectsModule } from './projects/projects.module';

import { AppComponent } from './app.component';

@NgModule({
	declarations: [
		AppComponent
	],
	imports: [
		BrowserModule,
		HttpClientModule,
		CustomersModule,
		JobTypesModule,
		ProjectsModule,
		ClarityModule,
		AppRoutingModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
