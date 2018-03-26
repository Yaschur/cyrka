import { createFeatureSelector, createSelector } from '@ngrx/store';

import { ProjectState } from './store/project.reducers';

export const getProjectFeatureState = createFeatureSelector<ProjectState>(
	'project'
);

export const getProjectEntities = createSelector(
	getProjectFeatureState,
	(state: ProjectState) => state.projects
);

export const getProjectEntity = createSelector(
	getProjectFeatureState,
	(state: ProjectState) => state.projects.find(p => p.id === state.projectId)
);
