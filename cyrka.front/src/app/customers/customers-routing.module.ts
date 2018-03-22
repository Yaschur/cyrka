import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { CustomerItemComponent } from './components/customer-item/customer-item.component';
import { CustomerFormComponent } from './components/customer-form/customer-form.component';

const customersRoutes: Routes = [
	{
		path: 'customers',
		component: CustomerListComponent,
		pathMatch: 'full',
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
