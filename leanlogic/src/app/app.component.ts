import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Employee } from './models/Employee';
import { EmployeeService } from './employee.service';
import { AddModalComponent } from './add-modal/add-modal.component';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent {
	public options: FormGroup;
	public employees: Employee[] = [
		new Employee({ id: 0, surname: 'Dieter', lastname: 'Hermann', birthday: new Date('1965-10-22T12:00') }),
	];

	constructor(
		private employeeService: EmployeeService,
		private modal: NzModalService,
	) {
		this.options = new FormGroup({
			filter: new FormControl(),
		});

		// Lade daten
		this.refresh();
	}

	public refresh(): void {
		this.employeeService.getEmployees(this.options.controls['filter'].value).subscribe(res => {
			this.employees = res;
		});
	}

	public add(): void {
		const ref = this.modal.create({
			nzTitle: 'Add employee',
			nzContent: AddModalComponent,
			nzFooter: [
				{
					label: 'Add',
					onClick: instance => instance.add(),
				},
				{
					label: 'Close',
					onClick: instance => instance.close(),
				}
			]
		})

		ref.afterClose.subscribe(() => {
			this.refresh();
		})
	}

	public remove(id: number): void {
		this.employeeService.removeEmployee(id).subscribe(() => this.refresh());
	}

	public filter(): void {
		this.refresh();
	}
}
