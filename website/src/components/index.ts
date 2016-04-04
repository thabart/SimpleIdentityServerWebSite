import { Component, OnInit } from 'angular2/core'
import { LoginComponent } from './login/login.ts'
import { SecurityService } from './service/security.ts'
import { RouteConfig } from 'angular2/router'
import { HomeComponent } from './home/home.ts'
import { LoggedInOutlet } from './loggedInOutlet.ts'

@Component({
    template: require('./index.html'),    
    styles: [ require('./index.scss') ],
    directives: [ LoggedInOutlet ]
})

@RouteConfig([
    {
        path: '/home',
        name: 'Home',
        component: HomeComponent,        
        useAsDefault: true 
    }
])

export class IndexComponent implements OnInit {
    isLogoutNotDisplayed: boolean;
    constructor(private _securityService : SecurityService) { }
    ngOnInit() {
        this.isLogoutNotDisplayed = true;        
        if (this._securityService.isResourceOwnerConnected())
        {
            this.isLogoutNotDisplayed = false;
        }
    }
    disconnect() {
        this._securityService.disconnectResourceOwner();
    }
}