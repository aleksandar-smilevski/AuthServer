import {Component, OnInit} from "@angular/core";
import {Observable} from "rxjs/Observable";
import {Book} from "../../models/book";
import {of} from "rxjs/observable/of";
import {CartService} from "./cart.service";

@Component({
    templateUrl: './cart.component.html',
    selector: 'cart'
})
export class CartComponent implements OnInit{
    public shoppingCartItems$: Observable<Book[]> = of([]);
    public shoppingCartItems: Book[] = [];
    
    constructor(private cartService: CartService){
        this.shoppingCartItems$ = this.cartService.getBooks();
        
        this.shoppingCartItems$.subscribe(_ => this.shoppingCartItems = _);
    }
    
    ngOnInit(): void {
        
    }
    
}