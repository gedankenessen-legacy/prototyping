import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Employee } from './models/Employee';

@Injectable({
	providedIn: 'root'
})
export class EmployeeService {
	// Dummy Daten, aggieren als Datenbank
	private data = [
		new Employee({ id: 0, surname: 'Dieter', lastname: 'Dieter', birthday: new Date('1965-10-22T12:00') }),
		new Employee({ id: 1, surname: 'Hans', lastname: 'Hubschrauber', birthday: new Date('1959-09-12T10:00') }),
		new Employee({ id: 2, surname: 'Gertrud', lastname: 'Gut', birthday: new Date('1990-07-08T22:00') }),
		new Employee({ id: 3, surname: 'Dagma', lastname: 'Dieter', birthday: new Date('2002-08-13T07:00') }),
		new Employee({ id: 4, surname: 'Hilde', lastname: 'Haube', birthday: new Date('1982-05-04T14:00') }),
		new Employee({ id: 5, surname: 'Peter', lastname: 'Panne', birthday: new Date('1970-06-24T15:00') }),
		new Employee({ id: 6, surname: 'Jasmin', lastname: 'Januar', birthday: new Date('2000-10-11T23:00') }),
		new Employee({ id: 7, surname: 'Jorg', lastname: 'Josman', birthday: new Date('1999-03-02T21:00') }),
		new Employee({ id: 8, surname: 'Gerhard', lastname: 'Grube', birthday: new Date('1959-11-23T18:00') }),
		new Employee({ id: 9, surname: 'Niels', lastname: 'Niemand', birthday: new Date('1975-12-30T09:00') }),
		new Employee({ id: 11, surname: 'Joseph', lastname: 'Jojo', birthday: new Date('1989-04-15T15:00') }),
	];

	constructor() { }

	public getEmployees(filter: string): Observable<Employee[]> {
		return (filter) ? of(this.data.filter(x => `${x.surname} ${x.lastname}`.includes(filter))) : of(this.data);
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
