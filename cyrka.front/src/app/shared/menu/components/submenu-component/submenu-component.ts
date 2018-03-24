import { Component, Input } from '@angular/core';
import { MenuLink } from '../../menu-link';

@Component({
	selector: 'app-submenu',
	templateUrl: './submenu-component.html',
	styleUrls: ['./submenu-component.scss'],
})
export class SubmenuComponent {
	@Input() items: MenuLink[];

	constructor() {}
}
