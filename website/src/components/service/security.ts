import { Injectable } from 'angular2/core'
import { Router } from 'angular2/router'
import { URLSearchParams } from 'angular2/http'
import { Settings } from '../settings.ts'
import { IdentityServerService, IntrospectionResponse, IntrospectionRequest } from './identityserver.ts'

@Injectable()
export class SecurityService {
    private _localStorageName : string = 'access_token_authorization_server';
    constructor(
        private _router: Router,
        private _settings: Settings,
        private _identityServerService: IdentityServerService) { }
    getAuthorizationUrl() : string {
        var authorizationUrl = this._settings.getAuthorizationUrl();
        var clientId = this._settings.getClientId();
        var callbackUrl = this._settings.getCurrentUrl();
        var scope = "openid profile role";
        var responseType = "id_token token";
        var nonce = "N" + Math.random() + "" + Date.now();
        var redirectUrl = 
            authorizationUrl + "/authorization" +
            "?scope="+ encodeURI(scope) + 
            "&response_type=" + encodeURI(responseType) +
            "&client_id=" + clientId + 
            "&redirect_uri=" + encodeURI(callbackUrl) + 
            "&nonce=" + encodeURI(nonce);
        return redirectUrl;
    }
    authenticateResourceOwner(hashParameter : string) {
        let result = this.extractAccessToken(hashParameter);
        if (result == null)
        {
            this._router.navigateByUrl('/error/callback');
            return;
        }
        
        var request = new IntrospectionRequest();
        request.token = result.token;
        this._identityServerService.introspectAccessToken(request)
            .then(res => {
                if (res.active == false)
                {
                    this._router.navigateByUrl('/error/callback');                    
                }
                else
                {               
                    localStorage.setItem(this._localStorageName, result.token);
                    this._router.navigateByUrl('/management');
                }
            });
    }
    isResourceOwnerConnected() {
        return localStorage.getItem(this._localStorageName) != null;
    }
    disconnectResourceOwner() {
        localStorage.removeItem(this._localStorageName);
        this._router.navigateByUrl('/login');
    }
    getAccessToken() {
        return localStorage.getItem(this._localStorageName);
    }
    private extractAccessToken(parameter : string) {
        if (!parameter || parameter.indexOf('#') != 0)
        {
            return null;
        }        
                
        var queryString = parameter.substr(1);        
        var params = new URLSearchParams(queryString);
        var token = params.get('access_token');
        var state = params.get('state');
        if (token == null)
        {
            return null;
        }
        
        return {
            token : token,
            state : state
        };
    }
}