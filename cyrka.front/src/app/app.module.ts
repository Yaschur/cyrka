import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { StoreModule } from '@ngrx/store';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { EffectsModule } from '@ngrx/effects';
import { NgxsModule } from '@ngxs/store';
import { ClarityModule } from '@clr/angular';

import { AppRoutingModule } from './app-routing.module';
import { CustomersModule } from './customers/customers.module';
import { JobsModule } from './jobs/jobs.module';
import { ProjectsModule } from './projects/projects.module';

import { AuthService } from './auth/auth.service';
import { AuthInterceptor } from './auth/auth.interceptor';
import { AppComponent } from './app.component';
import { CallbackComponent } from './auth/callback/callback.component';

@NgModule({
	declarations: [AppComponent, CallbackComponent],
	imports: [
		BrowserModule,
		HttpClientModule,
		CustomersModule,
		JobsModule,
		ProjectsModule,
		StoreModule.forRoot({}),
		EffectsModule.forRoot([]),
		StoreRouterConnectingModule,
		NgxsModule.forRoot([]),
		ClarityModule,
		AppRoutingModule,
	],
	providers: [
		AuthService,
		{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
	],
	bootstrap: [AppComponent],
})
export class AppModule {}
