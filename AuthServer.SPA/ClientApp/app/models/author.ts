import {Book} from "./book";

export class Author {
    id: string;
    firstName: string;
    lastName: string;
    books: Book[];

    constructor(id: string, firstName: string, lastName:string){
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
    }
}