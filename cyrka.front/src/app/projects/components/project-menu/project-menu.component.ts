import { Component} from '@angular/core';
import { MenuLink } from '../../../shared/menu/menu-link';

@Component({
	selector: 'app-project-menu',
	templateUrl: './project-menu.component.html',
	styleUrls: ['./project-menu.component.scss'],
})
export class ProjectMenuComponent {
	menus: MenuLink[];

	constructor() {}
}
