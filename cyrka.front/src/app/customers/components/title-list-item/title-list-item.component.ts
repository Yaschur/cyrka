import { Component, Input, EventEmitter, Output } from '@angular/core';

import { Store } from '@ngxs/store';

import { Title } from '../../models/title';
import { UpdateTitle } from '../../store/customer.actions';

@Component({
	selector: 'app-title-list-item',
	templateUrl: './title-list-item.component.html',
	styleUrls: ['./title-list-item.component.scss'],
})
export class TitleListItemComponent {
	@Input() title: Title;
	@Input() customerId: string;

	@Output() select: EventEmitter<void>;

	editMode: boolean;

	constructor(private _store: Store) {
		this.select = new EventEmitter();
		this.setEditMode(false);
	}

	setEditMode(flag: boolean) {
		this.editMode = flag;
	}

	setSelection() {
		this.select.emit();
	}

	getChanged(title: Title) {
		this._store.dispatch(
			new UpdateTitle({ customerId: this.customerId, title: title })
		);
		this.setEditMode(false);
	}
}
