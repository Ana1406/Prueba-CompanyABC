export interface Payment {
    idPayment: string;
    nameOwner: string;
    products: string[];
    totalPrice: number;
    email: string;
    status: boolean;
}