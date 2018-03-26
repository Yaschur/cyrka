import { Project } from '../models/project';
import { ProjectActions, ProjectActionTypes } from './project.actions';

export interface ProjectState {
	projects: Project[];
	projectId: string;
}

export const initialState: ProjectState = {
	projects: [],
	projectId: '',
};

export function projectReducer(
	state: ProjectState = initialState,
	action: ProjectActions
) {
	switch (action.type) {
		case ProjectActionTypes.LOAD_PROJECTS:
			return { ...state, projects: action.payload };
		case ProjectActionTypes.LOAD_PROJECT: {
			const exInd = state.projects.findIndex(p => p.id === action.payload.id);
			const newInd = exInd < 0 ? state.projects.length : exInd;
			return {
				...state,
				projects: [
					...state.projects.slice(0, newInd),
					action.payload,
					...state.projects.slice(newInd + 1),
				],
			};
		}
		case ProjectActionTypes.GET_PROJECT:
			return { ...state, projectId: action.payload };
		default:
			return state;
	}
}
