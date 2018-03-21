import { Component, Input } from '@angular/core';
import { Title } from '../../models/title';

@Component({
	selector: 'app-title-list-item',
	templateUrl: './title-list-item.component.html',
	styleUrls: ['./title-list-item.component.scss'],
})
export class TitleListItemComponent {
	@Input()
	title: Title;

	constructor() {}
}
