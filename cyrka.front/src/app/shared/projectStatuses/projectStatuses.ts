export enum ProjectStatuses {
	Draft = 'Draft',
	InProgress = 'InProgress',
	Closed = 'Closed',
	Cancelled = 'Cancelled',
}

export namespace ProjectStatuses {
	const allStatuses: Map<ProjectStatuses, string> = new Map([
		[ProjectStatuses.Draft, 'Планирование'],
		[ProjectStatuses.InProgress, 'В работе'],
		[ProjectStatuses.Closed, 'Завершен'],
		[ProjectStatuses.Cancelled, 'Отменен'],
	]);

	const allActions: Map<string, ProjectStatuses> = new Map([
		['Планировать', ProjectStatuses.Draft],
		['В работу', ProjectStatuses.InProgress],
		['Завершить', ProjectStatuses.Closed],
		['Отменить', ProjectStatuses.Cancelled],
	]);

	// export const allActions = (): [{actionName: string, status: }]
}
