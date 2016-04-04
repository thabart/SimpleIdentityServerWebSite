import { Injectable } from 'angular2/core'
import { Settings } from '../settings.ts'
import { Http, Headers, RequestOptions } from 'angular2/http'

export class IntrospectionRequest
{
    public token : string
}

export class IntrospectionResponse
{
    public active : boolean;
    public scope : string;
}

@Injectable()
export class IdentityServerService
{
    private _introspectionPartialPath = "/introspect";
    constructor(private _http: Http, private _settings: Settings) { }
    introspectAccessToken(introspectionRequest : IntrospectionRequest)
    {
        var clientId = this._settings.getClientId();
        var clientSecret = this._settings.getClientSecret();
        let parameters = "token="+introspectionRequest.token+"&token_type_hint=access_token" +
            "&client_id="+clientId+"&client_secret="+clientSecret;
        let url = this._settings.getAuthorizationUrl() + this._introspectionPartialPath;
        let headers = new Headers(
            {'Content-Type': 'application/x-www-form-urlencoded'}
        );               
        let options = new RequestOptions({headers : headers});
        return this._http.post(url, parameters, {
                headers : headers
            })
            .toPromise()
            .then(res => <IntrospectionResponse>res.json())
            .catch(err => {
                var response = new IntrospectionResponse();
                response.active = false;
                return Promise.resolve(response);
            });        
    }
}