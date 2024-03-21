import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';
import { IAddress } from 'src/app/shared/Models/address';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {
  @Input() checkoutForm:FormGroup;
  constructor(private accountService:AccountService,private toastr:ToastrService) { }

  ngOnInit(): void {
  }
  saveUserAddress(){
    let _currentAddress = this.checkoutForm.get('addressForm').value;
    this.accountService.updateUserAddress(_currentAddress).subscribe({
      next:((addres:IAddress)=>{
        this.toastr.success('Updated Successfully');
        this.checkoutForm.get('addressForm').reset(addres);

      }),
      error:((err)=>{this.toastr.error('Error on Saving')})
    })
  }
  get _firstName() {
    return this.checkoutForm.get('addressForm.firstName');
  }
  get _lastName() {
    return this.checkoutForm.get('addressForm.lastName');
  }
  get _street() {
    return this.checkoutForm.get('addressForm.street');
  }
  get _state() {
    return this.checkoutForm.get('addressForm.state');
  }
  get _city() {
    return this.checkoutForm.get('addressForm.city');
  }
  get _zipCode() {
    return this.checkoutForm.get('addressForm.zipCode');
  }

}
