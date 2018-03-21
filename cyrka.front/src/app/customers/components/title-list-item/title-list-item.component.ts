import { Component, Input, EventEmitter, Output } from '@angular/core';
import { Title } from '../../models/title';

@Component({
	selector: 'app-title-list-item',
	templateUrl: './title-list-item.component.html',
	styleUrls: ['./title-list-item.component.scss'],
})
export class TitleListItemComponent {
	@Input() title: Title;

	@Output() select: EventEmitter<void>;

	editMode: boolean;

	constructor() {
		this.select = new EventEmitter();
		this.editMode = false;
	}

	setEditMode() {
		this.editMode = true;
	}

	setSelection() {
		this.select.emit();
	}
}
