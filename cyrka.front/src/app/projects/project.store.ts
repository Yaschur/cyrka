import { createFeatureSelector, createSelector } from '@ngrx/store';

import { ProjectState } from './store/project.reducers';

export const getProjectFeatureState = createFeatureSelector<ProjectState>(
	'project'
);

export const getCustomerEntities = createSelector(
	getProjectFeatureState,
	(state: ProjectState) => state.projects
);
