import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ClarityModule } from '@clr/angular';
import { RouterModule } from '@angular/router';
import { NgxsModule } from '@ngxs/store';
import { SharedModule } from '../shared/shared.module';

import { CustomersRoutingModule } from './customers-routing.module';

import { CustomerFormComponent } from './components/customer-form/customer-form.component';
import { CustomerApiService } from './services/customer-api.service';
import { CustomerComponent } from './components/customer/customer.component';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { CustomerItemComponent } from './components/customer-item/customer-item.component';
import { TitleListItemComponent } from './components/title-list-item/title-list-item.component';
import { TitleListFormComponent } from './components/title-list-form/title-list-form.component';
import { CustomerState } from './store/customer.state';

@NgModule({
	declarations: [
		CustomerFormComponent,
		CustomerComponent,
		CustomerListComponent,
		CustomerItemComponent,
		TitleListItemComponent,
		TitleListFormComponent,
	],
	imports: [
		CommonModule,
		RouterModule,
		ReactiveFormsModule,
		ClarityModule,
		SharedModule,
		NgxsModule.forFeature([CustomerState]),
		CustomersRoutingModule,
	],
	providers: [CustomerApiService],
})
export class CustomersModule {}
