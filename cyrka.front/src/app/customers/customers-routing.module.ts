import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomersListComponent } from './components/customers-list.component';
import { CustomersRegisterComponent } from './components/customers-register.component';

const customersRoutes: Routes = [
	{ path: 'customers', component: CustomersListComponent },
	{ path: 'customers/register', component: CustomersRegisterComponent }
];

@NgModule({
	imports: [RouterModule.forChild(customersRoutes)],
	exports: [RouterModule]
})
export class CustomersRoutingModule { }
