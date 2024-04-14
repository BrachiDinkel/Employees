import { CommonModule } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClientModule } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { RoleService } from '../../service/role.service';
import { MatInputModule } from '@angular/material/input';
import {
  MatDialogModule,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Employee } from '../../models/employee.model';
import { EmployeeService } from '../../service/employee.service';
import { employeeRole } from '../../models/employeeRole.model';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ValiditionService } from '../../service/validition.service';
import { Role } from '../../models/role.model';

@Component({
  selector: 'app-add-edit-employee',
  standalone: true,
  imports: [
    MatFormFieldModule,
    ReactiveFormsModule,
    CommonModule,
    HttpClientModule,
    MatSelectModule,
    MatInputModule,
    MatDialogModule,
    MatCardModule,
    FormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
  ],
  providers: [
    HttpClientModule,
    RoleService,
    EmployeeService,
    MatDatepickerModule,
  ],
  templateUrl: './add-edit-employee.component.html',
  styleUrl: './add-edit-employee.component.css',
})
export class AddEditEmployeeComponent {
  employeeForm!: FormGroup;
  formFieldsValid: boolean = true;
  roleForm!: FormGroup;
  roles!: Role[];
  roleDialogOpened: boolean = false;
  employeeDetails!: Employee;
  roleEmployee!: employeeRole;
  isEditMode: boolean = false;
  isEmployeeIdDisabled: boolean = false;
  isManagmentOptions = [
    { label: 'Is Management', value: true },
    { label: 'No Management', value: false },
  ];
  genderOptions = [
    { label: 'male', value: 0 },
    { label: 'female', value: 1 },
  ];
  constructor(
    @Inject(MAT_DIALOG_DATA) public employee: { id: number },
    private roleService: RoleService,
    private formBuilder: FormBuilder,
    private employeeService: EmployeeService,
    private dialogRef: MatDialogRef<AddEditEmployeeComponent>,
    private _snackBar: MatSnackBar,
    private valdiatorService: ValiditionService
  ) {}

  ngOnInit(): void {
    this.isEmployeeIdDisabled =
      this.employee?.id !== null &&
      this.employee?.id !== undefined &&
      !isNaN(this.employee?.id);
    this.employeeForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      employeeId: ['', [Validators.required, Validators.pattern('^[0-9]+$')]],
      startDate: ['', Validators.required],
      birthDate: ['', Validators.required],
      gender: ['', Validators.required],
      isActive: [true],
      employeeRoleList: this.formBuilder.array([
        { validator: this.valdiatorService.validateDateStart },
      ]),
    });

    this.getEmployee();
    this.roleForm = this.formBuilder.group({
      employeeRoles: this.formBuilder.array([]),
    });
    this.getRoles();
  }

  public getEmployee() {
    if (!this.employee) {
      return;
    }
    this.employeeService.getEmployeeById(this.employee.id).subscribe(
      (response) => {
        this.employeeForm.patchValue(response);
        response.employeeRoleList?.forEach((role) => {
          this.addEmployeeRole(
            role.roleId,
            role.isManagement,
            role.startDate,
            (role.id = 1)
          );
        });
      },
      (error) => {
        const message = 'An error occurred while fetching roles';
        this.alertMessage(message);
      }
    );
  }

  public getRoles() {
    this.roleService.getAllRole().subscribe((data) => {
      this.roles = data.filter(
        (role: { id: number }) => this.roleForm.value.employeeRoles != role.id
      );
    });
  }

  public saveEmployee(): void {
    this.employeeDetails = this.employeeForm.value;
    this.employeeDetails.employeeRoleList = this.roleList.value;
    this.employeeDetails.employeeRoleList.forEach((r) => {
      r.employeeId = parseInt(this.employeeForm.value.employeeId);
      r.roleId = r.roleId;
      r.isManagement = r.isManagement == true;
      r.id = r.id;
    });
    if (this.employee) {
      this.updateEmployee();
      return;
    }

    this.createEmployee();
  }

  private createEmployee() {
    this.employeeService.postEmployee(this.employeeDetails).subscribe(
      (response) => {
        const message = 'The Employee Added Successfully!';
        this.alertMessage(message);

        this.dialogRef.close();
      },
      (error) => {
        const message = 'An error occurred while adding the employee';
        this.alertMessage(message);
      }
    );
  }

  private alertMessage(message: string) {
    this._snackBar.open(message, 'close', {
      duration: 8000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }

  private updateEmployee() {
    this.employeeService
      .putEmployee(this.employee.id, this.employeeDetails)
      .subscribe((response) => {
        const message = 'The Employee Updated Successfully!';
        this.alertMessage(message);
      });
    this.dialogRef.close();
  }

  openRoleDialog(): void {
    let isFull: boolean = false;
    if (this.roleList.length > 0) {
      isFull =
        this.roleList.at(this.roleList.length - 1).value.roleId != null &&
        this.roleList.at(this.roleList.length - 1).value.isManagement != null &&
        this.roleList.at(this.roleList.length - 1).value.startDate != null;
    }
    if (isFull || this.roleList.length == 0) {
      this.addEmployeeRole();
    }
  }

  addEmployeeRole(
    roleId?: number,
    isManagement?: boolean,
    startDate?: Date,
    id?: number
  ) {
    this.roleList.push(
      this.getEmployeeRoleControls(roleId, isManagement, startDate, id)
    );
  }

  get roleList() {
    return this.roleForm.get('employeeRoles') as FormArray;
  }

  getEmployeeRoleControls(
    roleId?: number,
    isManagement?: boolean,
    startDate?: Date,
    id?: number
  ): FormGroup {
    const newGroup = this.formBuilder.group({
      roleId: [roleId],
      isManagement: [isManagement],
      startDate: [startDate],
      id: [id],
    });
    return newGroup;
  }

  closeDialog() {
    this.dialogRef.close();
  }

  isRoleExist(rolek: any) {
    return this.roleList.value.find((r: any) => r.roleId === rolek?.id);
  }
}
