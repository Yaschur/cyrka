import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
	selector: 'app-callback',
	templateUrl: './callback.component.html',
	styleUrls: ['./callback.component.scss'],
})
export class CallbackComponent implements OnInit, OnDestroy {
	loggedInSub: Subscription;

	constructor(private auth: AuthService, private router: Router) {
		auth.handleLoginCallback();
	}

	ngOnInit() {
		this.loggedInSub = this.auth.loggedIn$.subscribe(
			loggedIn => (loggedIn ? this.router.navigate(['/']) : null)
		);
	}

	ngOnDestroy() {
		this.loggedInSub.unsubscribe();
	}
}
