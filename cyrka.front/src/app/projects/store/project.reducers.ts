import { Project } from '../models/project';
import { ProjectActions, ProjectActionTypes } from './project.actions';
import { Customer } from '../models/customer';
import { Jobtype } from '../../jobs/models/jobtype';

export interface ProjectState {
	projects: Project[];
	projectId: string;
	customers: Customer[];
	jobtypes: Jobtype[];
}

export const initialState: ProjectState = {
	projects: [],
	projectId: '',
	customers: [],
	jobtypes: [],
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
		case ProjectActionTypes.LOAD_JOBTYPES:
			return { ...state, jobtypes: action.payload };
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
		case ProjectActionTypes.SET_JOB: {
			const projInd = state.projects.findIndex(p => p.id === state.projectId);
			if (projInd < 0) {
				return state;
			}
			return {
				...state,
				projects: [
					...state.projects.slice(0, projInd),
					{
						...state.projects[projInd],
						jobs: [...state.projects[projInd].jobs, action.payload],
					},
					...state.projects.slice(projInd + 1),
				],
			};
		}
		case ProjectActionTypes.SET_STATUS: {
			const projInd = state.projects.findIndex(p => p.id === state.projectId);
			if (projInd < 0) {
				return state;
			}
			return {
				...state,
				projects: [
					...state.projects.slice(0, projInd),
					{
						...state.projects[projInd],
						status: action.payload,
					},
					...state.projects.slice(projInd + 1),
				],
			};
		}
		case ProjectActionTypes.CHANGE_JOB: {
			const projInd = state.projects.findIndex(p => p.id === state.projectId);
			if (projInd < 0) {
				return state;
			}
			const jobInd = state.projects[projInd].jobs.findIndex(
				j => j.jobTypeId === action.payload.jobTypeId
			);
			if (jobInd < 0) {
				return state;
			}
			return {
				...state,
				projects: [
					...state.projects.slice(0, projInd),
					{
						...state.projects[projInd],
						jobs: [
							...state.projects[projInd].jobs.slice(0, jobInd),
							{ ...action.payload },
							...state.projects[projInd].jobs.slice(jobInd + 1),
						],
					},
					...state.projects.slice(projInd + 1),
				],
			};
		}
		default:
			return state;
	}
}
