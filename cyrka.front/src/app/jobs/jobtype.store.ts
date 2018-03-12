import { createFeatureSelector, createSelector } from '@ngrx/store';
import { JobtypeState } from './store/jobtype.reducers';

export const getJobtypeFeatureState = createFeatureSelector<JobtypeState>(
	'jobtype'
);

export const getJobtypeEntities = createSelector(
	getJobtypeFeatureState,
	(state: JobtypeState) => state.jobtypes
);
