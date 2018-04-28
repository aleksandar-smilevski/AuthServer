import {Component} from "@angular/core";
import {Book} from "../../../models/book";
import {Http} from "@angular/http";
import {WebResponseType} from "../../../helpers/webResponseType";

@Component({
    selector: 'all-books',
    templateUrl: './allBooks.component.html'
})
export class AllBooksComponent{
    private booksLoaded: boolean = false;
    private allBooks: Book[];
    
    constructor(private http: Http){
        this.getBooks();
    }
    
    getBooks(){
        this.http.get('http://localhost:5004/api/books/all')
            .map(res => res.json())
            .subscribe((res) => {
                if (res.responseType == WebResponseType.Success){
                    this.allBooks = res.data;
                    this.booksLoaded = true;
                }
            });
    }
}