export interface Order {
    idOrder: string;
    idUser: string;
    nameApplicant: string;
    emailApplicant: string;
    products: string[];
}
export interface CreateOrder {
    idUser: string;
    idOrder: string;
    nameApplicant: string;
    emailApplicant: string;
    products: string[];

}
export interface DeleteOrder {
    idOrder: string;

}