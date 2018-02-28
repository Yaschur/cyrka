export interface Project {
	id: string;
	product: {
		customerId: string;
		customerName: string;
		titleId: string;
		titleName: string;
		totalEpisodes: number;
		episodeNumber: number;
		episodeDuration: number;
	};
	jobs: {
		jobTypeId: string;
		jobTypeName: string;
		unitName: string;
		ratePerUnit: number;
		amount: number;
	}[]
}
