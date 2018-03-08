export interface JobType {
	id: string;
	name: string;
	unit: string;
	rate: number;
	description: string;
}

export interface JobTypeState extends JobType {
	isNew: boolean;
}

export interface JobTypeListState {
	jobTypes: JobType[];
}
