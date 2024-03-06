import { IProducts } from "./Products";

export interface IPagniation {
    pageNumber: number;
    pageSize: number;
    count: number;
    data: IProducts[];
  }
  