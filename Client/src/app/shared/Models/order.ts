import { IAddress } from "./address"

export interface IOrderToCreate {
    basketId: string
    deliveryMethodId: number
    shipToAddress: IAddress
}

export interface IOrder {
    id: number
    buyerEmail: string
    orderDate: string
    shipToAddress: IAddress
    deliveryMethod: string
    shippingPrice: number
    orderItems: IOrderItem[]
    subtotal: number
    total: number
    orderStatus: string
}


export interface IOrderItem {
    productItemId: number
    productItemName: string
    pictureUrl: string
    price: number
    quantity: number
}