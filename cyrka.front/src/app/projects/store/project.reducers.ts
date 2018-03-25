import { Project } from '../models/project';
import { ProjectActions, ProjectActionTypes } from './project.actions';

export interface ProjectState {
	projects: Project[];
	project: Project;
}

export const initialState: ProjectState = {
	projects: [],
	project: null,
};

export function projectReducer(
	state: ProjectState = initialState,
	action: ProjectActions
) {
	switch (action.type) {
		case ProjectActionTypes.LOAD_PROJECTS:
			return { ...state, projects: action.payload };
		case ProjectActionTypes.LOAD_PROJECT:
			return { ...state, project: action.payload };
		default:
			return state;
	}
}
