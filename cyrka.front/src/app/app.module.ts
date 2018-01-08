import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';

import { ClarityModule } from 'clarity-angular';

import { AppComponent } from './app.component';
import {  } from "./customers/";


@NgModule({
	declarations: [
		AppComponent
	],
	imports: [
		BrowserModule,
		CustomersModule,
		ClarityModule.forRoot(),

		AppRoutingModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
