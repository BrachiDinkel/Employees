import { employeeRole } from './employeeRole.model';
export enum gender {
  Male,
  Female,
}
export class Employee {
  id?: number;
  employeeId?: number;
  firstName?: string;
  lastName?: string;
  startDate?: Date;
  birthDate?: Date;
  isActive?: boolean;
  gender?: gender;
  employeeRoleList: Array<employeeRole> = [];
}
