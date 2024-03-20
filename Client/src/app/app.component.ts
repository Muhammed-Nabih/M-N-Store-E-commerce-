import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'N&M'

  constructor(private basketService: BasketService, private accountServices: AccountService) { }
  

  ngOnInit(): void {
    this.loadBasket();
    this.loadCurrentUser();
  }
  loadBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe({
        next: () => { console.log('intialBasket') },
        error: (err) => { console.error(err) }
      })
    }
  }
  
  loadCurrentUser() {
    const token = localStorage.getItem('token');
    // if (token) {

      this.accountServices.loadCurrentUser(token).subscribe({
        next: () => { console.log('loadded User Succeffully') },
        error: (err) => { console.log(err) }
      })
    // }
  }
}
