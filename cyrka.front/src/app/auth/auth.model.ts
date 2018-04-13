import { UserProfile } from './user-profile';

export interface AuthStateModel {
	user?: UserProfile;
	accessToken?: string;
	expiresAt: number;
}
