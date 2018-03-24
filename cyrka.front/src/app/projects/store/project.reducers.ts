import { Project } from '../models/project';
import { ProjectActions, ProjectActionTypes } from './project.actions';

export interface ProjectState {
	projects: Project[];
}

export const initialState: ProjectState = {
	projects: [],
};

export function projectReducer(
	state: ProjectState = initialState,
	action: ProjectActions
) {
	switch (action.type) {
		case ProjectActionTypes.LOAD_PROJECTS:
			return { ...state, projects: action.payload };
		default:
			return state;
	}
}
