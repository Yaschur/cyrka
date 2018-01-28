import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomersComponent } from './components/customers.component';
import { CustomersListComponent } from './components/customers-list.component';
import { CustomersRegisterComponent } from './components/customers-register.component';
import { CustomersDetailsComponent } from './components/customers-details.component';
import { CustomersItemComponent } from './components/customers-item.component';

const customersRoutes: Routes = [
	{
		path: 'customers', component: CustomersComponent, children: [
			{ path: '', component: CustomersListComponent, pathMatch: 'full' },
			{ path: 'register', component: CustomersRegisterComponent },
			{
				path: ':customerId', children: [
					{ path: '', redirectTo: 'details', pathMatch: 'full' },
					{ path: ':mode', component: CustomersItemComponent }
				]
			}
		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(customersRoutes)],
	exports: [RouterModule]
})
export class CustomersRoutingModule { }
