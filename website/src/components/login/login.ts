import { Component } from 'angular2/core'
import { Settings } from '../settings.ts'
import { SecurityService } from '../services/security.ts'

@Component({
    template: require('./login.html'),
    styles: [ require('./login.scss') ],
})

export class LoginComponent {
    constructor(private _securityService : SecurityService) { }
    authenticate() {
        var redirectionUrl = this._securityService.getAuthorizationUrl();
        window.location.href = redirectionUrl;      
    }    
}