import {Component, OnInit} from "@angular/core";
import {Http} from "@angular/http";
import {ActivatedRoute} from "@angular/router";
import {CartService} from "../../cart/cart.service";
import {Book} from "../../../models/book";
import {WebResponseType} from "../../../helpers/webResponseType";
import {Observable} from "rxjs/Observable";

@Component({
  selector: 'book-details',
  templateUrl: './bookDetails.component.html'  
})
export class BookDetailsComponent implements OnInit{
    
    private id: string;
    private book: Book;
    private bookLoaded: boolean = false;
    public shoppingCartItems$: Observable<Book[]>;
    
    constructor(private http: Http, private route: ActivatedRoute, private cartService: CartService){}
    
    ngOnInit(): void {
        this.route.params.subscribe(params => {
             this.id = params["id"];
             this.getBook();
        });
    }
    
    getBook(){
        this.http.get(`http://localhost:5004/api/books/${this.id}`)
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