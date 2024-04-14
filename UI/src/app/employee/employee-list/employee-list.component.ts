import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { Employee } from '../../models/employee.model';
import { EmployeeService } from '../../service/employee.service';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { DeleteEmployeeComponent } from '../delete-employee/delete-employee.component';
import { MatButtonModule } from '@angular/material/button';
import * as xlsx from 'xlsx';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AddEditEmployeeComponent } from '../employee/add-edit-employee.component';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [
    MatDialogModule,
    CommonModule,
    HttpClientModule,
    MatTableModule,
    MatIconModule,
    AddEditEmployeeComponent,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatAutocompleteModule,
    MatInputModule,
    MatToolbarModule,
  ],
  providers: [HttpClientModule, EmployeeService],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css',
})

export class EmployeeListComponent {
  employeeList: Employee[] = [];
  displayedColumns: string[] = [
    'First Name',
    'Last Name',
    'Employee Id',
    'Start Date',
    'Delete',
    'Edit',
  ];
  dataSource = this.employeeList;
  searchText: string = '';
  highlightedRowIndex: number | null = null;
  private editMemberComponent = AddEditEmployeeComponent;

  ngOnInit(): void {
    this.getEmployeeDetails();
  }

  constructor(
    private employeeService: EmployeeService,
    private dialog: MatDialog
  ) {}

  public getEmployeeDetails() {
    this.employeeService.getAllEmployee().subscribe((data) => {
      this.employeeList = data;
      this.employeeList = this.employeeList.filter(
        (employee) => employee.isActive === true
      );
      this.dataSource = this.employeeList;
    });
  }

  openDeleteConfirmationDialog(employeeId: number): void {
    const dialogRef = this.dialog.open(DeleteEmployeeComponent, {
      width: '350px',
      data: { employeeId },
    });

    dialogRef.afterClosed().subscribe(() => this.getEmployeeDetails());
  }

  deleteEmployee(id: number): void {
    this.employeeService.deleteEmployee(id).subscribe(
      () => {},
      (error) => {
        console.error('Error deleting employee:', error);
      }
    );
  }

  public addEmployee() {
    const dialogRef = this.dialog.open(this.editMemberComponent);
    dialogRef.afterClosed().subscribe(() => this.getEmployeeDetails());
  }
  public editRecord(id: number) {
    const dialogRef = this.dialog.open(this.editMemberComponent, {
      width: 'auto',
      maxWidth: '100vw',
      maxHeight: '90vh',

      data: { id: id },
    });
    dialogRef.afterClosed().subscribe(() => this.getEmployeeDetails());
  }

  applyFilter(event: KeyboardEvent): void {
    if (!event) {
      this.getEmployeeDetails();
      return;
    }
    const searchText = (event.target as HTMLInputElement)?.value.toLowerCase();
    if (searchText !== undefined) {
      this.dataSource = this.dataSource.filter(
        (employee) =>
          employee.firstName?.toLowerCase().includes(searchText) ||
          employee.lastName?.toLowerCase().includes(searchText) ||
          employee.startDate?.toString().toLowerCase().includes(searchText) ||
          employee.employeeId?.toString().toLowerCase().includes(searchText)
      );
    }

    if (!searchText) {
      this.dataSource = this.employeeList;
    }
  }

  public exportToExcel() {
    const worksheet = xlsx.utils.json_to_sheet(this.dataSource);
    const workbook = xlsx.utils.book_new();
    xlsx.utils.book_append_sheet(workbook, worksheet, 'Employees');
    xlsx.writeFile(workbook, 'employees_data.xlsx');
  }
}
