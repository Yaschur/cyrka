import { UserProfile } from './user-profile';

export class Login {
	static readonly type = '[Auth] Login';
}
export class Logout {
	static readonly type = '[Auth] Logout';
}
export class LoginSuccess {
	constructor(public readonly payload: { token: string; expiresAt: number }) {}
	static type = '[Auth] LoginSuccess';
}
export class LoadProfile {
	static readonly type = '[Auth] LoadProfile';
}
export class ProfileSuccess {
	constructor(public readonly payload: UserProfile) {}
	static readonly type = '[Auth] ProfileSuccess';
}

export class LoginFailed {
	constructor(public payload: any) {}
	static type = '[Auth] LoginFailed';
}
export class ProfileFailed {
	constructor(public readonly payload: any) {}
	static readonly type = '[Auth] ProfileFailed';
}
