import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ClarityModule } from 'clarity-angular';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProjectListComponent } from './projects/components/project-list.component';
import { ProjectEditComponent } from './projects/components/project-edit.component';

import 'rxjs/add/operator/switchMap';

@NgModule({
	declarations: [
		AppComponent,
		ProjectListComponent,
		ProjectEditComponent
	],
	imports: [
		BrowserModule,
		BrowserAnimationsModule,
		ReactiveFormsModule,
		HttpClientModule,
		ClarityModule.forRoot(),
		AppRoutingModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
