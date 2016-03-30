import { Component } from 'angular2/core';
import { RouteConfig } from 'angular2/router';
import { LoggedInOutlet } from './loggedInOutlet.ts';
import { LoginComponent } from './login/login.ts';
import { HomeComponent } from './home/home.ts';

@Component({
    selector: 'app',
    template: require('./app.html'),
    styles: [ require('./app.scss') ],
    directives: [ LoggedInOutlet ]
})

@RouteConfig([
    {
      path: '/',
      name: 'Index',
      component: HomeComponent,
      useAsDefault: true  
    },
    {
        path: '/login',
        name: 'Login',
        component: LoginComponent
    }
])

export class AppComponent { }