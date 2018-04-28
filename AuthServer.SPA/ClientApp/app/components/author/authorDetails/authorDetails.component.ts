import {Component, OnInit} from "@angular/core";
import {Http} from "@angular/http";
import {ActivatedRoute} from "@angular/router";
import {Author} from "../../../models/author";
import {WebResponseType} from "../../../helpers/webResponseType";

@Component({
    selector: 'author-details',
    templateUrl: './authorDetails.component.html'
})
export class AuthorDetailsComponent implements OnInit {
    
    private id: string;
    private sub: any;
    private author: Author;
    private isAuthorLoaded: boolean = false;
    
    constructor(private http: Http, private route: ActivatedRoute){
        this.route.params.subscribe(params => {
            this.id = params["id"];
            this.getAuthor();
        });
    }
    
    ngOnInit(){
       
    }
    
    getAuthor(){
        this.http.get(`http://localhost:5004/api/authors/${this.id}`)
            .map(res => res.json())
            .subscribe(res => {
               if(res.responseType == WebResponseType.Success){
                   this.author = res.data;
                   console.log(this.author);
                   this.isAuthorLoaded = true;
               } 
            });
    }
}
