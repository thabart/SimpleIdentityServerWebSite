import { Component, OnInit } from 'angular2/core'
import { LoginComponent } from './login/login.ts'
import { SecurityService } from './service/security.ts'
import { RouteConfig, RouterLink } from 'angular2/router'
import { LoggedInOutlet } from './loggedInOutlet.ts'

import { ProfileComponent } from './profile/profile.ts'
import { InstanceComponent } from './instance/instance.ts'

@Component({
    template: require('./index.html'),    
    styles: [ require('./index.scss') ],
    directives: [ LoggedInOutlet, RouterLink ]
})

@RouteConfig([
    {
        path: '/profile',
        name: 'Profile',
        component: ProfileComponent,        
        useAsDefault: true 
    },
    {
        path: '/instance',
        name: 'Instance',
        component: InstanceComponent
    }
])

export class IndexComponent implements OnInit {
    isLogoutNotDisplayed: boolean;
    constructor(private _securityService : SecurityService) { }
    ngOnInit() {
        this.isLogoutNotDisplayed = true;        
        if (this._securityService.isResourceOwnerConnected())
        {
            this.isLogoutNotDisplayed = false;
        }
    }
    disconnect() {
        this._securityService.disconnectResourceOwner();
    }
}