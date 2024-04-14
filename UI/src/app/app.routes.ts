import { Routes } from '@angular/router';
import { EmployeeListComponent } from './employee/employee-list/employee-list.component';

export const routes: Routes = [
    { path: '', redirectTo: '/employee-list', pathMatch: 'full' },
    
    { path: 'employee-list', component: EmployeeListComponent },

];
