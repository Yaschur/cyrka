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
}
