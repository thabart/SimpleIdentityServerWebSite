require("./index.css")

import 'rxjs/Rx';
import 'angular2/bundles/angular2-polyfills'

import {bootstrap} from 'angular2/platform/browser'
import {provide} from 'angular2/core'
import {HTTP_PROVIDERS} from 'angular2/http'
import {ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy} from 'angular2/router'
import {AppComponent} from './components/app.ts'
import {SecurityService} from './components/service/security.ts'
import {ProfileService} from './components/service/profile.ts'
import {Settings} from './components/settings.ts'
import {IdentityServerService} from './components/service/identityserver.ts'

export function main()
{
    return bootstrap(AppComponent, [
        ROUTER_PROVIDERS,
        HTTP_PROVIDERS,
        provide(LocationStrategy, {useClass: HashLocationStrategy}),
        SecurityService,
        IdentityServerService,
        ProfileService,
        Settings
    ]);
}

document.addEventListener('DOMContentLoaded', function (event) {
    main();
});