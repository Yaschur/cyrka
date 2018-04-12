export class Login {
	static readonly type = '[Auth] Login';
}
export class Logout {
	static readonly type = '[Auth] Logout';
}
export class LoginSuccess {
	static type = '[Auth] LoginSuccess';
	constructor(public payload: any) {}
}
export class LoginFailed {
	static type = '[Auth] LoginFailed';
	constructor(public error: any) {}
}
