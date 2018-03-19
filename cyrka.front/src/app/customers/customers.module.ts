import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ClarityModule } from '@clr/angular';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { CustomersRoutingModule } from './customers-routing.module';

import { CustomersApiService } from './services/customers-api.service';
import { CustomersListComponent } from './components/customers-list.component';
import { CustomersRegisterComponent } from './components/customers-register.component';
import { CustomersDetailsComponent } from './components/customers-details.component';
import { TitlesFormComponent } from './components/titles-form.component';
import { CustomersComponent } from './components/customers.component';
import { CustomerFormComponent } from './components/customer-form/customer-form.component';
import { CustomersItemComponent } from './components/customers-item.component';
import { CustomerApiService } from './services/customer-api.service';
import { customerReducer } from './store/customer.reducers';
import { CustomerEffects } from './store/customer.effects';
import { CustomerMenuComponent } from './components/customer-menu/customer-menu.component';
import { CustomerComponent } from './components/customer/customer.component';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { CustomerItemComponent } from './components/customer-item/customer-item.component';

@NgModule({
	declarations: [
		CustomersListComponent,
		CustomersRegisterComponent,
		CustomerFormComponent,
		CustomersItemComponent,
		CustomersDetailsComponent,
		TitlesFormComponent,
		CustomersComponent,
		CustomerMenuComponent,
		CustomerComponent,
		CustomerListComponent,
		CustomerItemComponent,
	],
	imports: [
		CommonModule,
		ReactiveFormsModule,
		ClarityModule,
		StoreModule.forFeature('customer', customerReducer),
		EffectsModule.forFeature([CustomerEffects]),
		CustomersRoutingModule,
	],
	providers: [CustomersApiService, CustomerApiService],
})
export class CustomersModule {}
