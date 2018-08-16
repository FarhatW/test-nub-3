export class Address {
  agency: string;
  service: string;
  company: string;
  streetNumber: number;
  address1: string;
  address2: string;
  postalCode: string;
  city: string;
  addressExtra: string;
}

export class AddressSave {
  constructor(
    public agency: string,
    public service: string,
    public company: string,
    public streetNumber: number,
    public address1: string,
    public address2: string,
    public postalCode: string,
    public city: string,
    public addressExtra: string) { }
}
