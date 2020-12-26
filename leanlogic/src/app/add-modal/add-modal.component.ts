import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { EmployeeService } from '../employee.service';
import { Employee } from '../models/Employee';

@Component({
	selector: 'app-add-modal',
	templateUrl: './add-modal.component.html',
	styleUrls: ['./add-modal.component.scss']
})
export class AddModalComponent implements OnInit {

	public inputs: FormGroup;

	constructor(
		private modal: NzModalRef,
		private employeeService: EmployeeService
	) {
		this.inputs = new FormGroup({
			surname: new FormControl('', Validators.required),
			lastname: new FormControl('', Validators.required),
			birthday: new FormControl('', Validators.required),
		});
	}

	ngOnInit(): void {
	}

	public add(): void {
		if (this.inputs.valid) {
			this.employeeService.addEmployee({
				surname: this.inputs.controls['surname'].value,
				lastname: this.inputs.controls['lastname'].value,
				birthday: this.inputs.controls['birthday'].value,
			} as Employee);
			this.modal.close();
		}
	}


	public close(): void {
		this.modal.close();
	}
}
