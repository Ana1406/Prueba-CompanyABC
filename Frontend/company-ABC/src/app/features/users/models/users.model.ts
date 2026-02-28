export interface Users {
    name: string;
    email: string;
    rol: string;
    createdDate: Date;
}

export interface CreateUser {
    name: string;
    emailIn: string;
    password: string;
    rol: string;
}

export interface FilterUser {
    email: string;
    page: number;
    pageSize: number;
}