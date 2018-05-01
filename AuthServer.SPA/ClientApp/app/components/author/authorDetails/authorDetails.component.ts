import {Component, OnInit} from "@angular/core";
import {Headers, Http, RequestOptions} from "@angular/http";
import {ActivatedRoute} from "@angular/router";
import {Author} from "../../../models/author";
import {WebResponseType} from "../../../helpers/webResponseType";
import {AuthService} from "../../../authentication/authService";

@Component({
    selector: 'author-details',
    templateUrl: './authorDetails.component.html'
})
export class AuthorDetailsComponent {
    
    private id: string;
    private sub: any;
    private author: Author;
    private isAuthorLoaded: boolean = false;
    
    constructor(private http: Http, private route: ActivatedRoute, private authService : AuthService){
        this.route.params.subscribe(params => {
            this.id = params["id"];
            this.getAuthor();
        });
    }
    
    getAuthor(){
        let headers = new Headers();
        headers.append('Authorization', this.authService.getBearerToken());
        let opts = new RequestOptions();
        opts.headers = headers;
        this.http.get(`http://localhost:5004/api/authors/${this.id}`, opts)
            .map(res => res.json())
            .subscribe(res => {
               if(res.responseType == WebResponseType.Success){
                   this.author = res.data;
                   this.isAuthorLoaded = true;
               } 
            });
    }
}
