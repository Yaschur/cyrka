import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';

import { ClarityModule } from 'clarity-angular';

import { AppComponent } from './app.component';


@NgModule({
	declarations: [
		AppComponent
	],
	imports: [
		BrowserModule,
		ClarityModule.forRoot(),

		AppRoutingModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
