import { map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { State, Action, StateContext } from '@ngxs/store';

import { ProjectStateModel } from './project-model.state';
import { ProjectApiService } from '../services/project-api.service';
import {
	FindProjects,
	LoadProjects,
	GetProject,
	LoadProject,
	CreateProject,
	SetProduct,
} from './project.actions';

@State<ProjectStateModel>({
	name: 'project',
	defaults: {
		customers: [],
		jobtypes: [],
		projects: [],
		selectedProject: null,
	},
})
export class ProjectState {
	constructor(private readonly _projectApi: ProjectApiService) {}

	@Action(FindProjects)
	findProjects(sc: StateContext<ProjectStateModel>) {
		// if (sc.getState().customers.length > 0) {
		// 	return;
		// }
		this._projectApi.fetchAll().pipe(
			map(res => sc.dispatch(new LoadProjects(res))),
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}

	@Action(LoadProjects)
	loadProjects(sc: StateContext<ProjectStateModel>, a: LoadProjects) {
		sc.patchState({
			projects: a.payload,
		});
	}

	@Action(GetProject)
	getProject(sc: StateContext<ProjectStateModel>, a: GetProject) {
		sc.patchState({
			selectedProject: a.payload,
		});
		if (sc.getState().projects.some(p => p.id === a.payload)) {
			return;
		}
		this._projectApi.getById(a.payload).pipe(
			map(res => sc.dispatch(new LoadProject(res))),
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}

	@Action(LoadProject)
	loadProject(sc: StateContext<ProjectStateModel>, a: LoadProject) {
		const prjs = sc.getState().projects;
		const exInd = prjs.findIndex(p => p.id === a.payload.id);
		const newInd = exInd < 0 ? 0 : exInd;
		sc.patchState({
			projects: [
				...prjs.slice(0, newInd),
				a.payload,
				...prjs.slice(newInd + 1),
			],
		});
	}

	@Action(CreateProject)
	createProject(sc: StateContext<ProjectStateModel>) {
		this._projectApi.register().pipe(
			map(res => new GetProject(res.resourceId)),
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}

	@Action(SetProduct)
	setProduct(sc: StateContext<ProjectStateModel>, a: SetProduct) {
		const state = sc.getState();
		const projInd = state.projects.findIndex(
			p => p.id === state.selectedProject
		);
		if (projInd < 0) {
			return;
		}
		const proj = state.projects[projInd];
		sc.patchState({
			projects: [
				...state.projects.slice(0, projInd),
				{ ...proj, product: a.payload },
				...state.projects.slice(projInd + 1),
			],
		});
		this._projectApi.setProduct(state.selectedProject, a.payload).pipe(
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}
}
