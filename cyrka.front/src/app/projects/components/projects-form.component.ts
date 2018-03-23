import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/mergeMap';
import 'rxjs/add/operator/zip';

import { ProjectPlain } from '../models/project-plain';
import { CustomersApiService } from '../services/customers-api.service';
import { ProjectsApiService } from '../services/projects-api.service';
import { Customer } from '../models/customer';
import { Title } from '../models/title';
import { ProductSet } from '../models/product-set';
import { JobType } from '../models/job-type';
import { JobsApiService } from '../services/jobs-api.service';
import { JobSet } from '../models/job-set';
import { UnitDescriptor } from '../../shared/units/unit-descriptor';
import { UnitService } from '../../shared/units/unit.service';
import { of } from 'rxjs/observable/of';
import { ApiAnswer } from '../../shared/api/api-answer';

interface JobTypeWithUnit extends JobType {
	unitDescriptor: UnitDescriptor;
}

@Component({
	selector: 'app-projects-form',
	templateUrl: './projects-form.component.html',
	styleUrls: ['./projects-form.component.scss'],
})
export class ProjectsFormComponent implements OnInit {
	public form: FormGroup;
	public formTitle: string;
	public submitTitle: string;

	public customers: Customer[];
	public titles: Title[];
	public jobTypes: JobTypeWithUnit[];
	public units: UnitDescriptor[];
	public noe: number;

	constructor(
		private _formBuilder: FormBuilder,
		private _projectApi: ProjectsApiService,
		private _customerApi: CustomersApiService,
		private _jobApi: JobsApiService,
		private _unitSrv: UnitService,
		private _route: ActivatedRoute,
		private _router: Router
	) {
		this.customers = [];
		this.titles = [];
		this.jobTypes = [];
		this.noe = 0;
		this.form = this._formBuilder.group({
			product: this._formBuilder.group({
				customer: [null, Validators.required],
				title: [null, Validators.required],
				episodeNumber: [null, Validators.required],
				episodeDuration: [null, Validators.required],
			}),
			jobs: _formBuilder.array([]),
		});
		this.form
			.get('product.customer')
			.valueChanges.subscribe(c => this.customerChanges(c));
		this.form
			.get('product.title')
			.valueChanges.subscribe(c => this.titleChanges(c));
	}

	public get formJobs(): FormArray {
		return this.form.get('jobs') as FormArray;
	}
	public get availableJobTypes(): JobTypeWithUnit[] {
		return this.jobTypes.filter(
			jt =>
				!this.formJobs.controls
					.map(c => c.value['jobType'].id)
					.some(id => jt.id === id)
		);
	}

	public ngOnInit() {
		this._route.params
			.switchMap(
				p =>
					p['projectId']
						? this._projectApi.getById(p['projectId'])
						: of(<ProjectPlain>{ product: {}, jobs: [] })
			)
			.zip(
				this._customerApi.getAll(),
				this._jobApi.getAll(),
				this._unitSrv.getAll(),
				(
					project: ProjectPlain,
					customers: Customer[],
					jobTypes: JobType[],
					units: UnitDescriptor[]
				) => ({ project, customers, jobTypes, units })
			)
			.subscribe(p =>
				this.initItAll(p.project, p.customers, p.jobTypes, p.units)
			);
	}

	public onSave() {
		if (this.form.invalid || this.form.pristine) {
			console.log(
				`form is invalid: ${this.form.invalid}, form is pristine: ${
					this.form.pristine
				}`
			);
			return;
		}
		(this._id
			? of<ApiAnswer>({ resourceId: this._id, resourceType: '' })
			: this._projectApi.register()
		)
			.flatMap(res =>
				this._projectApi.setProduct(res.resourceId, this.getProductSet())
			)
			.flatMap(res =>
				this._projectApi.setJobs(res.resourceId, this.getJobSets())
			)
			.subscribe(() => this.onCancel());
	}

	public onCancel() {
		this._router.navigate(['..'], { relativeTo: this._route });
	}

	public addJobType(jtId: string) {
		this.addJobFormGroup(jtId);
	}

	private _id: string;

	private initItAll(
		project: ProjectPlain,
		customers: Customer[],
		jobTypes: JobType[],
		units: UnitDescriptor[]
	) {
		this._id = project.id;
		this.formTitle = this._id
			? 'изменение данных проекта'
			: 'создание нового проекта';
		this.submitTitle = this._id ? 'изменить' : 'создать';
		this.customers = customers;
		const selCustomer = project.product
			? customers.find(c => c.id === project.product.customerId) || null
			: null;
		const selTitle = selCustomer
			? selCustomer.titles.find(t => t.id === project.product.titleId) || null
			: null;
		this.form.patchValue({
			product: {
				customer: selCustomer,
				title: selTitle,
				episodeNumber: project.product ? project.product.episodeNumber || 0 : 0,
				episodeDuration: project.product
					? project.product.episodeDuration || 0
					: 0,
			},
		});
		this.jobTypes = jobTypes.map(jt => <JobTypeWithUnit>jt).map(jtu => {
			jtu.unitDescriptor = units.find(u => u.key === jtu.unit) || units[0];
			return jtu;
		});
		project.jobs.forEach(j =>
			this.addJobFormGroup(j.jobTypeId, j.amount, j.ratePerUnit)
		);
	}

	private customerChanges(val: Customer) {
		if (val) {
			this.titles = val.titles;
			this.form.get('product.title').enable();
		} else {
			this.titles = [];
			this.form.get('product.title').disable();
		}
	}

	private titleChanges(val: Title) {
		if (val) {
			this.noe = val.numberOfSeries;
			this.form.get('product.episodeNumber').enable();
			this.form.get('product.episodeDuration').enable();
		} else {
			this.noe = null;
			this.form.get('product.episodeNumber').disable();
			this.form.get('product.episodeDuration').disable();
		}
	}

	private getProductSet(): ProductSet {
		const formProduct = this.form.get('product').value;
		return <ProductSet>{
			customerId: formProduct.customer.id,
			customerName: formProduct.customer.name,
			titleId: formProduct.title.id,
			titleName: formProduct.title.name,
			totalEpisodes: this.noe,
			episodeNumber: formProduct.episodeNumber,
			episodeDuration: formProduct.episodeDuration,
		};
	}

	private getJobSets(): JobSet[] {
		const formJobs = this.form.get('jobs').value;
		return formJobs.map(
			j =>
				<JobSet>{
					jobtypeId: j.jobType.id,
					jobtypeName: j.jobType.name,
					unitName: j.jobType.unitDescriptor.key,
					ratePerUnit: j.rate,
					amount: j.amount,
				}
		);
	}

	private addJobFormGroup(jobTypeId: string, amount?: number, rate?: number) {
		const selJobType = this.jobTypes.find(j => j.id === jobTypeId);
		if (!selJobType) {
			return;
		}
		this.formJobs.push(
			this._formBuilder.group({
				jobType: [selJobType, Validators.required],
				rate: [rate || selJobType ? selJobType.rate : 0, Validators.required],
				amount: [amount || 1, Validators.required],
			})
		);
		this.form.markAsDirty();
	}
}
