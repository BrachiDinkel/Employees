import { Injectable } from '@angular/core';
import { FormArray, FormGroup, ValidationErrors } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class ValiditionService {
  constructor() {}

  validateDateStart(group: FormGroup): ValidationErrors | null {
    const employeeRoleList = (group.get('employeeRoleList') as FormArray)
      .controls;
    const formStartDate = group.get('startDate')?.value;

    for (const role of employeeRoleList) {
      const entryStartDate = role.get('startDate')?.value;
      if (new Date(entryStartDate) >= new Date(formStartDate)) {
        return { invalidEntryStartDate: true };
      }
    }
    return null;
  }
}
