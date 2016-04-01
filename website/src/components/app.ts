import { Component, OnInit } from 'angular2/core'
import { RouteConfig } from 'angular2/router'
import { LoggedInOutlet } from './loggedInOutlet.ts'
import { LoginComponent } from './login/login.ts'
import { HomeComponent } from './home/home.ts'
import { Settings } from './settings.ts'
import { SecurityService } from './service/security.ts'
import { CallBackErrorComponent } from './error/callback-error.ts'

@Component({
    selector: 'app',
    template: require('./app.html'),
    styles: [ require('./app.scss') ],
    directives: [ LoggedInOutlet ],
    providers: [
        Settings
    ]
})

@RouteConfig([
    {
      path: '/',
      name: 'Index',
      component: HomeComponent,
      useAsDefault: true  
    },
    {
        path: '/login',
        name: 'Login',
        component: LoginComponent
    }, 
    {
        path: '/home',
        name: 'Home',
        component: HomeComponent
    },
    {
        path: '/error/callback',
        name: 'ErrorCallback',
        component: CallBackErrorComponent
    }
])

export class AppComponent implements OnInit {
    constructor(private _securityService : SecurityService) { }
    ngOnInit() {
        var hash = this.getAuthorizationCallbackQueryString();
        if (hash != null) {
            // Authorization callback
            this._securityService.authenticateResourceOwner(hash);
        }
    }
    private getAuthorizationCallbackQueryString() {
        var hash = window.location.hash;
        if (hash &&
            hash.substr(1,1) != '/')
        {
            return hash;
        }
        
        return null;
    }
}