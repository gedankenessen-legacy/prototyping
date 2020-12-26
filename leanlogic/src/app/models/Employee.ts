export class Employee {
	public id: number; 
	public surname: String;
	public lastname: String;
	public birthday: Date;

	constructor(obj: any) {
		Object.assign(this, obj);
	}
}
