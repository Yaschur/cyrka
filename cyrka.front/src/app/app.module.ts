import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';

import { ClarityModule } from 'clarity-angular';

import { AppComponent } from './app.component';
import { CustomersModule } from './customers/customers.module';


@NgModule({
	declarations: [
		AppComponent
	],
	imports: [
		BrowserModule,
		HttpClientModule,
		CustomersModule,
		ClarityModule.forRoot(),
		AppRoutingModule
	],
	providers: [
		HttpClient
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
