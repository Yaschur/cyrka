import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomersListComponent } from './components/customers-list.component';

const customersRoutes: Routes = [
	{ path: 'customers', component: CustomersListComponent }
];

@NgModule({
	imports: [RouterModule.forChild(customersRoutes)],
	exports: [RouterModule]
})
export class CustomersRoutingModule { }
