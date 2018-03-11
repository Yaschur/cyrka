import { createFeatureSelector, createSelector } from '@ngrx/store';
import {
	JobTypesState,
	getJobTypeEntities as getJobTypeEntitiesF,
} from './store/job-types.reducers';

export const getJobTypesFeatureState = createFeatureSelector<JobTypesState>(
	'jobTypes'
);

export const getJobTypeEntities = createSelector(
	getJobTypesFeatureState,
	getJobTypeEntitiesF
);
