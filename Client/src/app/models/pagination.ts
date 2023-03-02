export interface IPagination<T> {
    pageNumber: number;
    pageSize: number;
    count: number;
    data: T[];
}

export class Pagination<T> implements IPagination<T> {
    pageNumber: number;
    pageSize: number;
    count: number;
    data: T[] = [];
}