import { UserProfile } from './user-profile';

/**
 * Command to start login operation
 */
export class Login {
	/**
	 * @param payload url to return when login succeed
	 */
	constructor(public readonly payload?: string) {}
	static readonly type = '[Auth] Login';
}
/**
 * Event
 */
export class LoginSuccess {
	/**
	 * @param payload usually from auth provider
	 */
	constructor(public readonly payload: { token: string; expiresAt: number }) {}
	static type = '[Auth] LoginSuccess';
}
/**
 * Event
 */
export class LoginFailed {
	/**
	 * @param payload reason of fail
	 */
	constructor(public payload: any) {}
	static type = '[Auth] LoginFailed';
}

/**
 * Command to start callback after auth provider call it
 */
export class Callback {
	static readonly type = '[Auth] Callback';
}

/**
 * Command to resurrect session
 */
export class CheckSession {
	/**
	 * @param payload url to return when resurrection succeed
	 */
	constructor(public readonly payload?: string) {}
	static readonly type = '[Auth] CheckSession';
}

/**
 * Command to logout explicitly
 */
export class Logout {
	static readonly type = '[Auth] Logout';
}

/**
 * Command to load profile
 */
export class LoadProfile {
	static readonly type = '[Auth] LoadProfile';
}
/**
 * Event of profile loaded
 */
export class ProfileSuccess {
	/**
	 * @param payload profile data
	 */
	constructor(public readonly payload: UserProfile) {}
	static readonly type = '[Auth] ProfileSuccess';
}
/**
 * Event of loading profile failed
 */
export class ProfileFailed {
	/**
	 * @param payload reason of fail
	 */
	constructor(public readonly payload: any) {}
	static readonly type = '[Auth] ProfileFailed';
}
