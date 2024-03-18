import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, IBasket, IBasketItem, IBasketTotals } from '../shared/Models/basket';
import { IProducts } from '../shared/Models/Products';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseURL:string = environment.baseURL;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();
  constructor(private http:HttpClient) { }


  private bsaketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  basketTotal$ = this.bsaketTotalSource.asObservable();

  incrementBasketItemQuantity(item:IBasketItem){
    const basket = this.getCurrentBasketValue();
    const itemIndex = basket.basketItems.findIndex(x=>x.id === item.id);
    basket.basketItems[itemIndex].quantity++;
    this.setBasket(basket);
  }
  decrementBasketItemQuantity(item:IBasketItem){
    const basket = this.getCurrentBasketValue();
    const itemIndex = basket.basketItems.findIndex(x=>x.id === item.id);

    if(basket.basketItems[itemIndex].quantity >1){
    basket.basketItems[itemIndex].quantity--;
    this.setBasket(basket);

    }else{
      this.removeItemFromBasket(item);
    }


  }
  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    if(basket.basketItems.some(x=>x.id === item.id))
    {
      basket.basketItems = basket.basketItems.filter(x=>x.id !== item.id);
      if(basket.basketItems.length >0){
        this.setBasket(basket);
      }
      else{
        this.deleteBasket(basket);
      }
    }
  }
  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseURL+'Basket/delete-basket-item/'+basket.id)
      .subscribe({
        next:()=>{
          this.basketSource.next(null);
          this.bsaketTotalSource.next(null);
          localStorage.removeItem('basket_id');
        },
        error:(err)=> {
          console.log(err);
        },

      });
  }


private calculateTotal(){
  const basket = this.getCurrentBasketValue();
  const shipping = 0;
  const subtotal = basket.basketItems.reduce((a,c)=>{
    return (c.price * c.quantity) + a;
  },0);
  const total = shipping+subtotal;
  this.bsaketTotalSource.next({shipping,subtotal,total});
}

  getBasket(id:string){
    return this.http.get(this.baseURL+'Basket/get-basket-item/'+id)
      .pipe(
        map((basket:IBasket)=>{
          this.basketSource.next(basket);
          this.calculateTotal();
          // console.info(this.getCurrentBasketValue());
        })
      )
  }


  setBasket(basket:IBasket){
    return this.http.post(this.baseURL+'Basket/update-basket',basket).subscribe({
      next:(res:IBasket)=>{
      this.basketSource.next(res);
      this.calculateTotal();
        // ,console.log(res)
      },
      error:(err) => {
        console.error(err);
      },
    })
  }

  getCurrentBasketValue(){
    return this.basketSource.value;
  }

  addItemToBasket(item:IProducts,quantity:number=1){
    const itemToAdd:IBasketItem = this.mapProductItemToBasketItem(item,quantity);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.basketItems = this.addOrUpdate(basket.basketItems,itemToAdd,quantity);
    return this.setBasket(basket);
  }
  private addOrUpdate(basketItems: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index = basketItems.findIndex(i=>i.id === itemToAdd.id);
    if(index ===-1){
      itemToAdd.quantity = quantity;
      basketItems.push(itemToAdd);
    }
    else{
      basketItems[index].quantity += quantity;
    }
    return basketItems;
  }
  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id',basket.id);
    return basket;

  }
  private mapProductItemToBasketItem(item: IProducts, quantity: number): IBasketItem {
    return {
      id:item.id,
      productName:item.name,
      price : item.price,
      productPicture : item.productPicture,
      category : item.categoryName,
      quantity
    }
  }

}