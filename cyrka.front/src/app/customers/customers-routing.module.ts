import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomersComponent } from './components/customers.component';
import { CustomersListComponent } from './components/customers-list.component';
import { CustomersRegisterComponent } from './components/customers-register.component';
import { CustomersDetailsComponent } from './components/customers-details.component';
import { CustomersItemComponent } from './components/customers-item.component';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { CustomerItemComponent } from './components/customer-item/customer-item.component';
import { CustomerFormComponent } from './components/customer-form/customer-form.component';

const customersRoutes: Routes = [
	{
		path: 'customers',
		component: CustomerListComponent,
		pathMatch: 'full',
		// path: 'customers', component: CustomersComponent, children: [
		// 	{ path: '', component: CustomersListComponent, pathMatch: 'full' },
		// 	{ path: 'register', component: CustomersRegisterComponent },
		// 	{
		// 		path: ':customerId', children: [
		// 			{ path: '', redirectTo: 'details', pathMatch: 'full' },
		// 			{ path: ':mode', component: CustomersItemComponent }
		// 		]
		// 	}
		// ]
	},
	{
		path: 'customers/register',
		component: CustomerFormComponent,
		pathMatch: 'full',
	},
	{
		path: 'customers/:customerId',
		component: CustomerItemComponent,
		pathMatch: 'full',
	},
	{
		path: 'customers/:customerId/edit',
		component: CustomerFormComponent,
		pathMatch: 'full',
	},
];

@NgModule({
	imports: [RouterModule.forChild(customersRoutes)],
	exports: [RouterModule],
})
export class CustomersRoutingModule {}
