import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomersComponent } from './components/customers.component';
import { CustomersListComponent } from './components/customers-list.component';
import { CustomersRegisterComponent } from './components/customers-register.component';
import { CustomersDetailsComponent } from './components/customers-details.component';

const customersRoutes: Routes = [
	{
		path: 'customers', component: CustomersComponent, children: [
			{ path: '', component: CustomersListComponent, pathMatch: 'full' },
			{ path: 'register', component: CustomersRegisterComponent },
			{ path: ':customerId/change', component: CustomersRegisterComponent },
			{ path: ':customerId/details', component: CustomersDetailsComponent }
		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(customersRoutes)],
	exports: [RouterModule]
})
export class CustomersRoutingModule { }
