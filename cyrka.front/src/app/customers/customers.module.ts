import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClarityModule } from 'clarity-angular';

import { CustomersRoutingModule } from './customers-routing.module';

import { CustomersApiService } from './services/customers-api.service';
import { CustomersListComponent } from './components/customers-list.component';
import { CustomersRegisterComponent } from './components/customers-register.component';

@NgModule({
	declarations: [
		CustomersListComponent,
		CustomersRegisterComponent
	],
	imports: [
		CommonModule,
		ClarityModule.forChild(),
		CustomersRoutingModule
	],
	providers: [
		CustomersApiService
	]
})
export class CustomersModule { }