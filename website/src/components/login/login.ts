import { Component } from 'angular2/core';
import { Settings } from '../settings.ts';

@Component({
    template: require('./login.html')
})

export class LoginComponent {
    constructor(private _settings : Settings) { }
    authenticate() {
        var authorizationUrl = this._settings.getAuthorizationUrl();
        var clientId = this._settings.getClientId();
        var callbackUrl = this._settings.getCurrentUrl() + "/callback";
        authorizationUrl += "/authorization?scope=openid%20profile%20role&response_type=id_token&client_id="
            + clientId + "&redirect_uri=" + callbackUrl + "&nonce=nonce";
        window.location.href = authorizationUrl;      
    }    
}