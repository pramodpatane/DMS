export class AddressModel {
  parentRecordId: string = "";
  addressLine1: string = "";
  addressLine2: string = "";
  country: string = "";
  state: string = "";
  city: string = "";
  pinCode: string = "";
  latitude: string = "";
  longitude: string = "";
}

export class AddressConfiguration {
    canHeaderView: boolean = true;
}