import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProjectListComponent } from './projects/components/project-list.component';
import { ProjectEditComponent } from './projects/components/project-edit.component';

const routes: Routes = [
	{ path: 'projects', component: ProjectListComponent, pathMatch: 'full' },
	{ path: 'projects/:id', component: ProjectEditComponent, pathMatch: 'full' },
	{ path: '', redirectTo: 'projects', pathMatch: 'full' }
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
