import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ClarityModule } from '@clr/angular';

import { CustomersRoutingModule } from './customers-routing.module';

import { CustomersApiService } from './services/customers-api.service';
import { CustomersListComponent } from './components/customers-list.component';
import { CustomersRegisterComponent } from './components/customers-register.component';
import { CustomersDetailsComponent } from './components/customers-details.component';
import { TitlesFormComponent } from './components/titles-form.component';
import { CustomersComponent } from './components/customers.component';
import { CustomerFormComponent } from './components/customers-form.component';
import { CustomersItemComponent } from './components/customers-item.component';
import { CustomerApiService } from './services/customer-api.service';

@NgModule({
	declarations: [
		CustomersListComponent,
		CustomersRegisterComponent,
		CustomerFormComponent,
		CustomersItemComponent,
		CustomersDetailsComponent,
		TitlesFormComponent,
		CustomersComponent,
	],
	imports: [
		CommonModule,
		ReactiveFormsModule,
		ClarityModule,
		CustomersRoutingModule,
	],
	providers: [CustomersApiService, CustomerApiService],
})
export class CustomersModule {}
