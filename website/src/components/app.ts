import { Component, OnInit } from 'angular2/core'
import { RouteConfig, RouterOutlet } from 'angular2/router'
import { LoginComponent } from './login/login.ts'
import { IndexComponent } from './index.ts'
import { Settings } from './settings.ts'
import { SecurityService } from './services/security.ts'
import { CallBackErrorComponent } from './error/callback-error.ts'

@Component({
    selector: 'app',
    template: require('./app.html'),
    providers: [
        Settings
    ],
    directives: [ RouterOutlet ]
})

@RouteConfig([
    {
        path: '/',
        redirectTo: ['/Management']
    },
    {
        path: '/management/...',
        component: IndexComponent,
        name: 'Management'
    },
    {
        path: '/login',
        name: 'Login',
        component: LoginComponent
    },
    {
        path: '/error/callback',
        name: 'ErrorCallback',
        component: CallBackErrorComponent
    }
])

export class AppComponent implements OnInit {
    isLogoutNotDisplayed: boolean;
    constructor(private _securityService : SecurityService) { }
    ngOnInit() {
        var hash = this.getAuthorizationCallbackQueryString();
        if (hash != null) {
            // Execute authorization callbackAuthorization callback
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
