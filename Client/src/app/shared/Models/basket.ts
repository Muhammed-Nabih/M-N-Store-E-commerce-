import { uuid } from "angular-uuid"
export interface IBasket {
    id: string;
    basketItems: IBasketItem[];
}

export interface IBasketItem {
    id: number;
    productName: string;
    productPicture: string;
    price: number;
    quantity: number;
    category: string;
}

export class Basket implements IBasket {
    id : uuid.v4;
    basketItems: IBasketItem[];
}