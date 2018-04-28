import {Component, OnInit} from "@angular/core";
import {Observable} from "rxjs/Observable";
import {Book} from "../../models/book";
import {CartService} from "../cart/cart.service";

@Component({
    selector: 'cart-items',
    templateUrl: './cartItems.component.html'
})
export class CartItemsComponent {
    public shoppingCartItems$: Observable<Book[]>;
    public shoppingCartItems: Book[]  = [];
    
    constructor(private cartService: CartService){
        this.shoppingCartItems$ = this
            .cartService
            .getBooks();

        this.shoppingCartItems$.subscribe(_ => this.shoppingCartItems =  _);
    }
    
    quantityChanged(e: Event, item: Book){
        
    }
    
}