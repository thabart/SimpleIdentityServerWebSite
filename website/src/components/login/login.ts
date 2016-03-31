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
        var callbackUrl = this._settings.getCurrentUrl();
        var scope = "openid profile role";
        var responseType = "id_token";
        var nonce = "N" + Math.random() + "" + Date.now();
        var redirectUrl = 
            authorizationUrl + "/authorization" +
            "?scope="+ encodeURI(scope) + 
            "&response_type=" + encodeURI(responseType) +
            "&client_id=" + clientId + 
            "&redirect_uri=" + encodeURI(callbackUrl) + 
            "&nonce=" + encodeURI(nonce);
        window.location.href = redirectUrl;      
    }    
}