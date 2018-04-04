export enum ProjectStatuses {
	Draft = 'Draft',
	InProgress = 'InProgress',
	Closed = 'Closed',
	Cancelled = 'Cancelled',
}

export namespace ProjectStatuses {
	export const allStatuses: Map<ProjectStatuses, string> = new Map([
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

	export const getActions = (
		status: ProjectStatuses
	): {
		actionName: string;
		statusValue: ProjectStatuses;
		statusName: string;
		isCurrent: boolean;
	}[] => {
		const res: {
			actionName: string;
			statusValue: ProjectStatuses;
			statusName: string;
			isCurrent: boolean;
		}[] = [];
		allActions.forEach((val, key) => {
			res.push({
				actionName: key,
				statusValue: val,
				statusName: allStatuses.get(val),
				isCurrent: val === status,
			});
		});
		return res;
	};
}
