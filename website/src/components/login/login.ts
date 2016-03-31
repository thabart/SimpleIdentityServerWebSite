import { Component } from 'angular2/core';
import { Settings } from '../settings.ts';

@Component({
    template: require('./login.html')
})

export class LoginComponent {
    constructor(private _settings : Settings) { }
    authenticate() {
        var authorizationUrl = this._settings.getAuthorizationUrl();
        
    }    
}