import { HttpClientModule } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeService } from '../../service/employee.service';

@Component({
  selector: 'app-delete-employee',
  standalone: true,
  imports: [HttpClientModule],
  providers: [HttpClientModule, EmployeeService],

  templateUrl: './delete-employee.component.html',
  styleUrl: './delete-employee.component.css',
})
export class DeleteEmployeeComponent {
  constructor(
    public dialogRef: MatDialogRef<DeleteEmployeeComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private employeeService: EmployeeService
  ) {}

  ngOnInit(): void {}

  onCancelClick(): void {
    this.dialogRef.close();
  }

  onConfirmClick(): void {
    this.employeeService.deleteEmployee(this.data.employeeId).subscribe(
      () => {
        this.dialogRef.close('confirm');
      },
      (error) => {
        console.error('Error deleting employee:', error);
      }
    );
  }
}
