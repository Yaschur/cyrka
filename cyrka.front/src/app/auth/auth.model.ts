import { UserProfile } from './user-profile';

export interface AuthStateModel {
	user?: UserProfile;
	accessToken?: string;
	expiresAt: number;
	message?: string;
}

export interface AuthResult {
	accessToken: string;
	expiresIn: number;
}

export interface AuthError {
	error: string;
	errorDescription: string;
}
