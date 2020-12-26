import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Employee } from './models/Employee';

@Injectable({
	providedIn: 'root'
})
export class EmployeeService {
	// Dummy Daten, aggieren als Datenbank
	private data = [
		new Employee({ id: 0, surname: 'Dieter' }),
	];

	constructor() { }

	public getEmployees(filter: string): Observable<Employee[]> {
		return of(this.data.filter(x => `${x.surname} ${x.lastname}`.includes(filter)));
	}

	public addEmployee(employee: Employee): Observable<boolean> {
		this.data.push({ ...employee, id: this.data.length });
		return of(true);
	}

	public removeEmployee(id: number): Observable<boolean> {
		this.data = this.data.splice(this.data.findIndex(x => x.id === id), 1);
		return of(true);
	}
}
