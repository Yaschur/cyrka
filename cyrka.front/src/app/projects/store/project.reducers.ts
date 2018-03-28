import { Project } from '../models/project';
import { ProjectActions, ProjectActionTypes } from './project.actions';
import { Customer } from '../models/customer';

export interface ProjectState {
	projects: Project[];
	projectId: string;
	customers: Customer[];
}

export const initialState: ProjectState = {
	projects: [],
	projectId: '',
	customers: [],
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
		case ProjectActionTypes.LIST_PROJECTS:
			return { ...state, projectId: '' };
		case ProjectActionTypes.LOAD_CUSTOMERS:
			return { ...state, customers: action.payload };
		case ProjectActionTypes.SET_PRODUCT: {
			const projInd = state.projects.findIndex(p => p.id === state.projectId);
			if (projInd < 0) {
				return state;
			}
			return {
				...state,
				projects: [
					...state.projects.slice(0, projInd),
					{ ...state.projects[projInd], product: action.payload },
					...state.projects.slice(projInd + 1),
				],
			};
		}
		default:
			return state;
	}
}
