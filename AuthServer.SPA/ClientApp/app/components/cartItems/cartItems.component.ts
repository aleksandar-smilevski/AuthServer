import {Component, OnInit} from "@angular/core";
import {Observable} from "rxjs/Observable";
import {Book} from "../../models/book";
import {CartService} from "../cart/cart.service";
import {Order} from "../../models/order";

@Component({
    selector: 'cart-items',
    templateUrl: './cartItems.component.html'
})
export class CartItemsComponent {
    public shoppingCartItems$: Observable<Book[]>;
    public shoppingCartItems: Book[]  = [];
    private order: Order = new Order();
    
    constructor(private cartService: CartService){
        this.shoppingCartItems$ = this
            .cartService
            .getBooks();

        this.shoppingCartItems$.subscribe(_ => this.shoppingCartItems =  _);
    }
    
    onFormSubmit(myForm: any){
       console.log(myForm.value);
    }
    
}