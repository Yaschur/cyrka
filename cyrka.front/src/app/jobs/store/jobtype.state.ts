import { State } from '@ngxs/store';

import { JobtypeStateModel } from './jobtype-model.state';

@State<JobtypeStateModel>({
	name: 'jobtype',
	defaults: {
		jobtypes: [],
		selectedJobtype: null,
	},
})
export class JobtypeState {}
