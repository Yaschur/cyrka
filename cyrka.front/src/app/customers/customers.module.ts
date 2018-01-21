import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ClarityModule } from '@clr/angular';

import { CustomersRoutingModule } from './customers-routing.module';

import { CustomersApiService } from './services/customers-api.service';
import { CustomersListComponent } from './components/customers-list.component';
import { CustomersRegisterComponent } from './components/customers-register.component';
import { CustomersDetailsComponent } from './components/customers-details.component';
import { TitlesAddComponent } from './components/titles-add.component';

@NgModule({
	declarations: [
		CustomersListComponent,
		CustomersRegisterComponent,
		CustomersDetailsComponent,
		TitlesAddComponent
	],
	imports: [
		CommonModule,
		ReactiveFormsModule,
		ClarityModule,
		CustomersRoutingModule
	],
	providers: [
		CustomersApiService
	]
})
export class CustomersModule { }
