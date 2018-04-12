export class Login {
	static readonly type = '[Auth] Login';
}
export class Logout {
	static readonly type = '[Auth] Logout';
}
export class LoginSuccess {
	constructor(public payload: any) {}
	static type = '[Auth] LoginSuccess';
}
export class LoginFailed {
	constructor(public error: any) {}
	static type = '[Auth] LoginFailed';
}
