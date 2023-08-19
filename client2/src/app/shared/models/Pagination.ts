import { IPost } from "./IPost";

export interface IPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: any[];
}

export class Pagination implements IPagination{
    pageIndex: number = 0;
    pageSize: number = 0;
    count: number = 0;
    data: any[] = [];
}