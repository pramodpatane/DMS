import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentService } from '../../../core/Services/departments.service';
import { MenuPermissionsService } from '../../../core/Services/menu.permissions.service';
import { SwalService } from '../../../global/swal.service';
import { EmployeeService } from '../../Services/employee-service';
import { Employees } from './employees';
import { of } from 'rxjs';

describe('EmployeesComponent', () => {
  let component: Employees;
  let fixture: ComponentFixture<Employees>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Employees],
      providers: [
        { provide: DepartmentService, useValue: { GetDepartmentDropdown: () => Promise.resolve(of([])) } },
        {
          provide: EmployeeService,
          useValue: {
            GetAllData: () => Promise.resolve(of({ data: [], totalCount: 0 })),
            GetById: () => of({
              recordId: 'employee-1',
              firstName: 'Test',
              lastName: 'Employee',
              email: 'test@example.com',
              salary: 100,
              departmentId: 1,
              isActive: true,
              isDeleted: false
            })
          }
        },
        {
          provide: MenuPermissionsService,
          useValue: {
            canView: () => false,
            canAdd: () => false,
            canEdit: () => false,
            canDelete: () => false
          }
        },
        { provide: SwalService, useValue: {} }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Employees);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should show a fresh employee form when OpenForm is called', () => {
    component.OpenForm();

    expect(component.IsDefaultView).toBe(false);
    expect(component.HeaderText).toBe('Add Employee');
    expect(component.ButtonText).toBe('Insert');
    expect(component.employeeForm.get('firstName')?.value).toBe('');
  });

  it('should open the form after loading an employee for editing', () => {
    component.Edit('employee-1');

    expect(component.IsDefaultView).toBe(false);
    expect(component.HeaderText).toBe('Edit Employee');
    expect(component.ButtonText).toBe('Update');
    expect(component.employeeForm.get('firstName')?.value).toBe('Test');
    expect(component.employeeForm.get('recordId')?.value).toBe('employee-1');
  });
});
