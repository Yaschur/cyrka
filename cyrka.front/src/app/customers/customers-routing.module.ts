import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomersListComponent } from './components/customers-list.component';
import { CustomersRegisterComponent } from './components/customers-register.component';
import { CustomersDetailsComponent } from './components/customers-details.component';

const customersRoutes: Routes = [
	{ path: 'customers', component: CustomersListComponent },
	{ path: 'customers/register', component: CustomersRegisterComponent },
	{ path: 'customers/:id/change', component: CustomersRegisterComponent },
	{ path: 'customers/:id', component: CustomersDetailsComponent }
];

@NgModule({
	imports: [RouterModule.forChild(customersRoutes)],
	exports: [RouterModule]
})
export class CustomersRoutingModule { }
