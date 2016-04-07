import { Injectable } from 'angular2/core'
import { SecurityService } from './security.ts'
import { Http, Headers, RequestOptions } from 'angular2/http'
import { Settings } from '../settings.ts'

export class Profile {
    subject: string;
    name : string;
    picture: string;
    authorization_server: string;
    manager_website: string;
    manager_website_api: string;
    is_active: boolean;
}

export class CreateProfileRequest {
    name : string;
}
    
@Injectable()
export class ProfileService {
    private _profilesCurrentPartialPath = "/profiles/current";
    private _profilePartialPath = "/profiles";
    constructor(
        private _http : Http,
        private _settings: Settings,
        private _securityService: SecurityService) { }
    getCurrentProfile() {
        let accessToken = this._securityService.getAccessToken();
        let headers = new Headers();
        headers.append('Authorization', 'Bearer ' + accessToken);
        let options = new RequestOptions({ headers : headers });
        let url = this._settings.getApiUrl() + this._profilesCurrentPartialPath;
        console.log(url);
        return this._http.get(url, options)
            .toPromise()
            .then(res => {
                return <Profile>res.json();
            });
    }
    createProfile(request : CreateProfileRequest) {
        let json = JSON.stringify(request);
        let accessToken = this._securityService.getAccessToken();
        let headers = new Headers({'Content-Type': 'application/json'});
        headers.append('Authorization', 'Bearer ' + accessToken);
        let options = new RequestOptions({ headers : headers });
        let url = this._settings.getApiUrl() + this._profilePartialPath;
        return this._http.post(url, json, options)
            .toPromise()
            .then(res => {
                return <Profile>res.json(); 
            });
    }
}    