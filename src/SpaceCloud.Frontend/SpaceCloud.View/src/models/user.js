export default class User {
  constructor(
    mail,
    password,
    firstName,
    lastName,
    stayLogged,
    locationId,
    companyId,
  ) {
    this.mail = mail;
    this.password = password;
    this.firstName = firstName;
    this.lastName = lastName;
    this.stayLogged = stayLogged;
    if (!locationId) this.locationId = 1;
    else this.locationId = locationId;
    if (!companyId) this.companyId = 1;
    else this.companyId = companyId;
  }
}
