import { NgModule } from '@angular/core';

import { SubmenuComponent } from './menu/components/submenu-component/submenu-component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@NgModule({
	declarations: [SubmenuComponent],
	imports: [CommonModule, RouterModule],
	exports: [SubmenuComponent],
})
export class SharedModule {}
