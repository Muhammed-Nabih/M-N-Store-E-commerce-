import { Component, Input, OnInit } from '@angular/core';
import { IProducts } from 'src/app/shared/Models/Products';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-shop-item',
  templateUrl: './shop-item.component.html',
  styleUrls: ['./shop-item.component.scss']
})
export class ShopItemComponent implements OnInit {
  @Input() product: IProducts
  constructor(private basketService:BasketService) { }

  ngOnInit(): void {
    
  }
  addItemToBasket(){
    this.basketService.addItemToBasket(this.product);
  }

}
