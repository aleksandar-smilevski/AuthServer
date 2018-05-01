import {Component, OnInit} from "@angular/core";
import {Headers, Http, RequestOptions} from "@angular/http";
import {ActivatedRoute} from "@angular/router";
import {CartService} from "../../cart/cart.service";
import {Book} from "../../../models/book";
import {WebResponseType} from "../../../helpers/webResponseType";
import {Observable} from "rxjs/Observable";
import {AuthService} from "../../../authentication/authService";

@Component({
  selector: 'book-details',
  templateUrl: './bookDetails.component.html'  
})
export class BookDetailsComponent implements OnInit{
    
    private id: string;
    private book: Book;
    private bookLoaded: boolean = false;
    public shoppingCartItems$: Observable<Book[]>;
    
    constructor(private http: Http, private route: ActivatedRoute, private cartService: CartService, private authService: AuthService){}
    
    ngOnInit(): void {
        this.route.params.subscribe(params => {
             this.id = params["id"];
             this.getBook();
        });
    }
    
    getBook(){
        let headers = new Headers();
        headers.append('Authorization', this.authService.getBearerToken());
        let opts = new RequestOptions();
        opts.headers = headers;
        this.http.get(`http://localhost:5004/api/books/${this.id}`, opts)
            .map(res => res.json())
            .subscribe(res => {
                if (res.responseType == WebResponseType.Success){
                    this.book = res.data;
                    this.bookLoaded = true;
                }
            });
    }
    
    addToCart(book: Book){
        this.cartService.addToCart(book);
    }
}