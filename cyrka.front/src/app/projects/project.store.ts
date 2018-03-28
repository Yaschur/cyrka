import { createFeatureSelector, createSelector } from '@ngrx/store';

import { ProjectState } from './store/project.reducers';
import { Project } from './models/project';

export const getProjectFeatureState = createFeatureSelector<ProjectState>(
	'project'
);

export const getProjectEntities = createSelector(
	getProjectFeatureState,
	(state: ProjectState) => state.projects
);

export const getProjectEntity = createSelector(
	getProjectFeatureState,
	(state: ProjectState) =>
		state.projectId
			? state.projects.find(p => p.id === state.projectId)
			: <Project>{}
);

export const getCustomerEntities = createSelector(
	getProjectFeatureState,
	(state: ProjectState) => state.customers
);
