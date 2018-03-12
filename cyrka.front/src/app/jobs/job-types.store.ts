import { createFeatureSelector, createSelector } from '@ngrx/store';
import { JobTypesState } from './store/job-types.reducers';

export const getJobTypesFeatureState = createFeatureSelector<JobTypesState>(
	'jobTypes'
);

export const getJobTypeEntities = createSelector(
	getJobTypesFeatureState,
	(state: JobTypesState) => state.jobTypes
);
