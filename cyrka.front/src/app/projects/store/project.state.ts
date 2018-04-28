import { map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { State, Action, StateContext, Selector } from '@ngxs/store';

import { ProjectStateModel } from './project-model.state';
import { ProjectApiService } from '../services/project-api.service';
import {
	FindProjects,
	LoadProjects,
	GetProject,
	LoadProject,
	CreateProject,
	SetProduct,
	SetJob,
	SetStatus,
	ChangeJob,
	SetPayments,
	LoadCustomers,
	FindCustomers,
	FindJobtypes,
	LoadJobtypes,
	ClearProjectSelection,
} from './project.actions';
import { CustomerApiService } from '../services/customer-api.service';
import { JobApiService } from '../services/job-api.service';
import { Project } from '../models/project';

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
	constructor(
		private readonly _projectApi: ProjectApiService,
		private readonly _customerApi: CustomerApiService,
		private readonly _jobApi: JobApiService
	) {}

	@Action(FindProjects)
	findProjects(sc: StateContext<ProjectStateModel>) {
		return this._projectApi.fetchAll().pipe(
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
		return this._projectApi.getById(a.payload).pipe(
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

	@Action(ClearProjectSelection)
	clearProjectSelection(sc: StateContext<ProjectStateModel>) {
		sc.patchState({
			selectedProject: null,
		});
	}

	@Action(CreateProject)
	createProject(sc: StateContext<ProjectStateModel>) {
		return this._projectApi.register().pipe(
			map(res => sc.dispatch(new GetProject(res.resourceId))),
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
		return this._projectApi.setProduct(state.selectedProject, a.payload).pipe(
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}

	@Action(SetJob)
	setJob(sc: StateContext<ProjectStateModel>, a: SetJob) {
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
				{ ...proj, jobs: [...proj.jobs, a.payload] },
				...state.projects.slice(projInd + 1),
			],
		});
		return this._projectApi.setJob(state.selectedProject, a.payload).pipe(
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}

	@Action(SetStatus)
	setStatus(sc: StateContext<ProjectStateModel>, a: SetStatus) {
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
				{ ...proj, status: a.payload },
				...state.projects.slice(projInd + 1),
			],
		});
		return this._projectApi.setStatus(state.selectedProject, a.payload).pipe(
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}

	@Action(ChangeJob)
	changeJob(sc: StateContext<ProjectStateModel>, a: ChangeJob) {
		const state = sc.getState();
		const projInd = state.projects.findIndex(
			p => p.id === state.selectedProject
		);
		if (projInd < 0) {
			return;
		}
		const proj = state.projects[projInd];
		const jobInd = proj.jobs.findIndex(
			j => j.jobTypeId === a.payload.jobTypeId
		);
		if (jobInd < 0) {
			return;
		}
		sc.patchState({
			projects: [
				...state.projects.slice(0, projInd),
				{
					...proj,
					jobs: [
						...proj.jobs.slice(0, jobInd),
						{ ...a.payload },
						...proj.jobs.slice(jobInd + 1),
					],
				},
				...state.projects.slice(projInd + 1),
			],
		});
		return this._projectApi
			.changeJob(state.selectedProject, a.payload.jobTypeId, a.payload)
			.pipe(
				catchError(e => {
					console.log('Network error', e);
					return of();
				})
			);
	}

	@Action(SetPayments)
	setPayments(sc: StateContext<ProjectStateModel>, a: SetPayments) {
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
				{ ...proj, payments: a.payload },
				...state.projects.slice(projInd + 1),
			],
		});
		return this._projectApi.setPayments(state.selectedProject, a.payload).pipe(
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}

	@Action(FindCustomers)
	findCustomers(sc: StateContext<ProjectStateModel>) {
		return this._customerApi.fetchAll().pipe(
			map(res => sc.dispatch(new LoadCustomers(res))),
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}

	@Action(LoadCustomers)
	loadCustomers(sc: StateContext<ProjectStateModel>, a: LoadCustomers) {
		sc.patchState({
			customers: a.payload,
		});
	}

	@Action(FindJobtypes)
	findJobtypes(sc: StateContext<ProjectStateModel>) {
		return this._jobApi.fetchAll().pipe(
			map(res => sc.dispatch(new LoadJobtypes(res))),
			catchError(e => {
				console.log('Network error', e);
				return of();
			})
		);
	}

	@Action(LoadJobtypes)
	loadJobtypes(sc: StateContext<ProjectStateModel>, a: LoadJobtypes) {
		sc.patchState({
			jobtypes: a.payload,
		});
	}

	@Selector()
	static getProjects(state: ProjectStateModel) {
		return state.projects;
	}

	@Selector()
	static getProject(state: ProjectStateModel) {
		return (
			state.projects.find(p => p.id === state.selectedProject) || <Project>{}
		);
	}

	@Selector()
	static getJobtypes(state: ProjectStateModel) {
		return state.jobtypes;
	}

	@Selector()
	static getCustomers(state: ProjectStateModel) {
		return state.customers;
	}
}
