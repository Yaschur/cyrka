import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/mergeMap';
import 'rxjs/add/operator/zip';


import { Project } from '../models/project';
import { CustomersApiService } from '../services/customers-api.service';
import { ProjectsApiService } from '../services/projects-api.service';
import { Customer } from '../models/customer';
import { Title } from '../models/title';
import { ProductSet } from '../models/product-set';
import { ApiAnswer } from '../models/api-answer';
import { JobType } from '../models/job-type';

@Component({
	selector: 'app-projects-form',
	templateUrl: './projects-form.component.html',
	styleUrls: ['./projects-form.component.scss']
})
export class ProjectsFormComponent implements OnInit {

	constructor(
		private _formBuilder: FormBuilder,
		private _projectApi: ProjectsApiService,
		private _customerApi: CustomersApiService,
		private _route: ActivatedRoute,
		private _router: Router
	) {
		this.customers = [];
		this.titles = [];
		this.noe = 0;
		this.form = this._formBuilder.group({
			'product': this._formBuilder.group({
				'customer': [null, Validators.required],
				'title': [null, Validators.required],
				'episodeNumber': [null, Validators.required],
				'episodeDuration': [null, Validators.required]
			}),
			'jobs': _formBuilder.array([])
		});
		this.form.get('product.customer').valueChanges
			.subscribe(c => this.customerChanges(c));
		this.form.get('product.title').valueChanges
			.subscribe(c => this.titleChanges(c));
	}

	public form: FormGroup;
	public formTitle: string;
	public submitTitle: string;

	public customers: Customer[];
	public titles: Title[];
	public noe: number;

	public ngOnInit() {
		this._route.params
			.switchMap(p => p['projectId'] ? this._projectApi.getById(p['projectId']) : Observable.of(<Project>{ product: {} }))
			.zip(this._customerApi.getAll(), (project: Project, customers: Customer[]) => ({ project, customers }))
			.subscribe(p => this.initItAll(p.project, p.customers));
	}

	public onSave() {
		if (this.form.invalid || this.form.pristine) {
			return;
		}
		(this._id ? Observable.of<ApiAnswer>({ resourceId: this._id, resourceType: '' }) : this._projectApi.register())
			.flatMap(res => this._projectApi.setProduct(res.resourceId, this.getProductSet()))
			.subscribe(() => this.onCancel());
	}

	public onCancel() {
		this._router.navigate(['..'], { relativeTo: this._route });
	}

	private _id: string;

	private initItAll(project: Project, customers: Customer[]) {
		this._id = project.id;
		this.formTitle = this._id ? 'изменение данных проекта' : 'создание нового проекта';
		this.submitTitle = this._id ? 'изменить' : 'создать';
		this.customers = customers;
		const selCustomer = project.product ? customers.find(c => c.id === project.product.customerId) || null : null;
		const selTitle = selCustomer ? selCustomer.titles.find(t => t.id === project.product.titleId) || null : null;
		this.form.setValue({
			product: {
				customer: selCustomer,
				title: selTitle,
				episodeNumber: project.product ? project.product.episodeNumber || 0 : 0,
				episodeDuration: project.product ? project.product.episodeDuration || 0 : 0
			}
		});
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
			episodeDuration: formProduct.episodeDuration
		};
	}

	private addJobFormGroup(selJobType?: JobType, rate?: number, amount?: number) {
		(<FormArray>this.form.controls['jobs']).push(
			this._formBuilder.group({
				'jobType': [selJobType || null, Validators.required],
				'rate': [rate || 0, Validators.required],
				'amount': [amount || 0, Validators.required]
			})
		);
	}
}
