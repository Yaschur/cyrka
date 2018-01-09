import { NgModule } from '@angular/core';

import { CustomersRoutingModule } from './customers-routing.module';

import { CustomersApiService } from './services/customers-api.service';
import { CustomersListComponent } from './components/customers-list.component';

@NgModule({
	declarations: [
		CustomersListComponent
	],
	imports: [
		CustomersRoutingModule
	],
	providers: [
		CustomersApiService
	]
})
export class CustomersModule { }
