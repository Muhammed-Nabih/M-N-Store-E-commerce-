import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  loaderRequestCount:number =0;
  constructor(private spinnerServies:NgxSpinnerService) { }

  loader(){
    this.loaderRequestCount++;
    this.spinnerServies.show(undefined,{
      type : 'ball-spin-fade',
      color : "#e30808",
      bdColor : "rgba(255,255,255,0.7)",
      size : "medium"
    });
  }

  hidingLoader(){
    this.loaderRequestCount--;
    if(this.loaderRequestCount<=0){
      this.loaderRequestCount=0;
      this.spinnerServies.hide();
    }
  }

}