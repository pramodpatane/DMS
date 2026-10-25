import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommonAgGrid } from './common-ag-grid';

describe('CommonAgGrid', () => {
  let component: CommonAgGrid;
  let fixture: ComponentFixture<CommonAgGrid>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommonAgGrid]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommonAgGrid);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should update total pages when the record count changes', async () => {
    fixture.componentRef.setInput('recordCount', 25);
    fixture.detectChanges();
    await fixture.whenStable();

    expect(component.totalPages).toBe(3);
  });

  it('should update total pages when the page size changes', () => {
    const select = document.createElement('select');
    const option = document.createElement('option');
    option.value = '25';
    select.add(option);
    select.value = '25';
    const event = new Event('change');
    Object.defineProperty(event, 'target', { value: select });
    component.totalCount = 30;

    component.onPageSizeChange(event);

    expect(component.totalPages).toBe(2);
    expect(component.gridConfiguration.gridFilter.pagesize).toBe(25);
  });
});
