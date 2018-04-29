import {Author} from "./author";

export class Book {
    id: string;
    title: string;
    description: string;
    stock: number;
    datePublished: string;
    isbn: string;
    price: number;
    authors: Author[];

    constructor(){
        
    }
}
