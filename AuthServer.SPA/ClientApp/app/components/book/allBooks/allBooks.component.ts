import {Component} from "@angular/core";
import {Book} from "../../../models/book";
import {Headers, Http, RequestOptions} from "@angular/http";
import {WebResponseType} from "../../../helpers/webResponseType";
import {AuthService} from "../../../authentication/authService";

@Component({
    selector: 'all-books',
    templateUrl: './allBooks.component.html'
})
export class AllBooksComponent{
    private booksLoaded: boolean = false;
    private allBooks: Book[];
    
    constructor(private http: Http, private authService: AuthService){
        this.getBooks();
    }
    
    getBooks(){
        let headers = new Headers();
        headers.append('Authorization', this.authService.getBearerToken());
        let opts = new RequestOptions();
        opts.headers = headers;
        this.http.get('http://localhost:5004/api/books/all', opts)
            .map(res => res.json())
            .subscribe((res) => {
                if (res.responseType == WebResponseType.Success){
                    this.allBooks = res.data;
                    this.booksLoaded = true;
                }
            });
    }
}