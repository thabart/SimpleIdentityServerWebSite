/// <reference path="../node_modules/angular2/typings/browser.d.ts" />

import 'angular2/bundles/angular2-polyfills';

import {bootstrap} from 'angular2/platform/browser';
import {provide} from 'angular2/core';
import {HTTP_PROVIDERS} from 'angular2/http';
import {ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy} from 'angular2/router';
import {AppComponent} from './components/app.ts';


export function main()
{
    return bootstrap(AppComponent, [
      ROUTER_PROVIDERS,
      HTTP_PROVIDERS,
        provide(LocationStrategy, {useClass: HashLocationStrategy})
    ]);
}

document.addEventListener('DOMContentLoaded', function (event) {
    main();
});