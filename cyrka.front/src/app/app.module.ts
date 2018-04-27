import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { NgxsModule } from '@ngxs/store';
import { ClarityModule } from '@clr/angular';

import { AppRoutingModule } from './app-routing.module';
import { CustomersModule } from './customers/customers.module';
import { JobsModule } from './jobs/jobs.module';
import { ProjectsModule } from './projects/projects.module';

import { AuthService } from './auth/auth.service';
import { TokenInterceptor, UnauthInterceptor } from './auth/auth.interceptors';
import { AuthState } from './auth/auth.state';
import { AppComponent } from './app.component';
import { CallbackComponent } from './auth/callback/callback.component';
import { AuthGuard } from './auth/auth.guard';

@NgModule({
	declarations: [AppComponent, CallbackComponent],
	imports: [
		BrowserModule,
		HttpClientModule,
		CustomersModule,
		JobsModule,
		ProjectsModule,
		NgxsModule.forRoot([AuthState]),
		ClarityModule,
		AppRoutingModule,
	],
	providers: [
		AuthService,
		AuthGuard,
		{ provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
		{ provide: HTTP_INTERCEPTORS, useClass: UnauthInterceptor, multi: true },
	],
	bootstrap: [AppComponent],
})
export class AppModule {}
