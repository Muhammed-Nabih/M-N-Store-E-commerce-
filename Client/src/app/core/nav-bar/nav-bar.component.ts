import { Component, OnInit } from '@angular/core';
import { observable, Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/Models/basket';
import { IUser } from 'src/app/shared/Models/user';


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  constructor(private basketService:BasketService,private accountService:AccountService) { }
  basket$ : Observable<IBasket>;
  currentUser$ : Observable<IUser>;


  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.currentUser$ = this.accountService.currentUser$;
  }
  logout(){
    this.accountService.logout();
  }

}
