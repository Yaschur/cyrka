import { Component } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
	selector: 'app-callback',
	templateUrl: './callback.component.html',
	styleUrls: ['./callback.component.scss'],
})
export class CallbackComponent {
	message: string;

	constructor(private auth: AuthService, private router: Router) {
		auth.handleLoginCallback();
		this.message = 'Authentication...';
	}
}
