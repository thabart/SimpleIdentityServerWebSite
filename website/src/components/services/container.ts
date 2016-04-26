import { Injectable } from 'angular2/core'
import { SecurityService } from './security.ts'
import { Http, Headers, RequestOptions } from 'angular2/http'
import { Settings } from '../settings.ts'

@Injectable()
export class ContainerService {
    private _containerPartialPath = '/containers';
    constructor(
        private _http : Http,
        private _settings: Settings,
        private _securityService: SecurityService) { }
    start(name : string) {
      return this.executeAction(name, 'start');
    }
    stop(name : string) {
      return this.executeAction(name, 'stop');
    }
    private executeAction(
        name: string,
        action: string) {
      let accessToken = this._securityService.getAccessToken();
      let headers = new Headers();
      headers.append('Authorization', 'Bearer ' + accessToken);
      let options = new RequestOptions({ headers : headers });
      let url = this._settings.getApiUrl() + this._containerPartialPath + '/' + name + '/' + action;
      return this._http.get(url, options)
          .toPromise()
          .then(res => {
              return res.text();
          });
    }
}
