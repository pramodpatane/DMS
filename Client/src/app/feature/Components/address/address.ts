import { ChangeDetectorRef, Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { AddressConfiguration, AddressModel } from '../../Models/address.model';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { EmployeeModel } from '../../Models/employee.model';

@Component({
  selector: 'app-address',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './address.html',
  styleUrl: './address.css',
})
export class Address implements OnChanges {
  addressForm!: FormGroup;
  @Input() addressConfiguration = new AddressConfiguration();
  @Output() addressDataEvent = new EventEmitter<any>();

  constructor(private formBuilder: FormBuilder, private cdr: ChangeDetectorRef) {
    this.addressConfiguration.canHeaderView = false;
  }

  ngOnChanges(): void {

  }

  emitPageChange(): void {
    this.addressDataEvent.emit(this.addressConfiguration);
  }

  DeclareForm() {
    this.addressForm = this.formBuilder.group({
      parentRecordId: [null],
      addressLine1: ['', Validators.required],
      addressLine2: [''],
      country: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
      pinCode: ['', [Validators.required, Validators.pattern('^[0-9]{6}$')]],
      latitude: [null, [Validators.min(-90), Validators.max(90)]],
      longitude: [null, [Validators.min(-180), Validators.max(180)]]
    });
  }

  ConvertFormToModel() {
    const model = new AddressModel();
    model.addressLine1 = this.addressForm.value.addressLine1;
    model.addressLine2 = this.addressForm.value.addressLine2;
    model.country = this.addressForm.value.country;
    model.state = this.addressForm.value.state;
    model.city = this.addressForm.value.city;
    model.pinCode = this.addressForm.value.pinCode;
    model.latitude = this.addressForm.value.latitude;
    model.longitude = this.addressForm.value.longitude;

    //console.log(model);
    return model;
  }
}
