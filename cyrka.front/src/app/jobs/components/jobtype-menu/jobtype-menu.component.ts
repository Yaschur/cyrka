import { Component, Input } from '@angular/core';
import { Jobtype } from '../../models/jobtype';

interface MenuLink {
	linkTitle: string;
	linkText: string;
	linkUrl: string;
}

@Component({
	selector: 'app-jobtype-menu',
	templateUrl: './jobtype-menu.component.html',
	styleUrls: ['./jobtype-menu.component.scss'],
})
export class JobtypeMenuComponent {
	@Input()
	set targetJobtype(jt: Jobtype) {
		this.changeMenu = {
			linkText: jt && jt.id ? 'Изменить' : 'Добавить',
			linkTitle:
				jt && jt.id
					? 'изменить данные услуги'
					: 'зарегистрировать новую услугу',
			linkUrl: jt && jt.id ? `/jobtypes/${jt.id}/edit` : '/jobtypes/register',
		};
	}

	changeMenu: MenuLink;

	constructor() {}
}
