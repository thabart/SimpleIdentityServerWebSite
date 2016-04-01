/// <reference path="../../../node_modules/angular2/typings/browser.d.ts" />

import {
    it,
    inject,
    injectAsync,
    beforeEachProviders,
    TestComponentBuilder
} from 'angular2/testing'
import { provide } from 'angular2/core'
import { SecurityService } from './security.ts'
import { Settings } from '../settings.ts'
import { URLSearchParams } from 'angular2/http'
import { Router } from 'angular2/router'
import { IdentityServerService, IntrospectionResponse, IntrospectionRequest } from './identityserver.ts'

describe('Test Security service', () => {
    
    class MockRouter {
        navigateByUrl(url) { }
    }

    beforeEachProviders(() => [
        provide(Router, {
            useClass: MockRouter  
        })
    ])
    
    it('When getting authorization url then redirection url is returned', function() {
        // ARRANGE
        var authorizationUrl = "http://localhost/authorization";
        var clientId = "client_id";
        var currentUrl = "current_url";
        var settings = new Settings();        
        spyOn(settings, "getAuthorizationUrl").and.returnValue(authorizationUrl);
        spyOn(settings, "getClientId").and.returnValue(clientId);
        spyOn(settings, "getCurrentUrl").and.returnValue(currentUrl);
        var instance = new SecurityService(null, settings, null);
        
        // ACT
        var result = instance.getAuthorizationUrl();
        
        // ASSERT
        expect(result).not.toBe(null);
        var splittedUrl = result.split('?');
        expect(splittedUrl.length == 2).toBe(true);
        var queryString = splittedUrl[1];
        var params = new URLSearchParams(queryString);
        var scope = params.get('scope');
        expect(params.get('scope')).toBe(encodeURI("openid profile role"));
        expect(params.get('response_type')).toBe(encodeURI("id_token token"));
        expect(params.get('client_id')).toBe(clientId);
        expect(params.get('redirect_uri')).toBe(encodeURI(currentUrl));
        expect(params.get('nonce')).not.toBe(null);
    });    
    
    it("When passing an incorrect hash fragment then redirect to the login page", inject([Router], (routerMock) => {
        // ASSERT
        spyOn(routerMock, 'navigateByUrl').and.callFake(function(route) {
            expect(route).toBe('/error/callback');
        });

        // ARRANGE
        var instance = new SecurityService(routerMock, null, null);
        
        // ACT
        instance.authenticateResourceOwner(null);
        
    }));

    it ("When passing a hash fragment with no access token then redirect to the login page", inject([Router], (routerMock) => {
        // ASSERT
        spyOn(routerMock, 'navigateByUrl').and.callFake(function(route) {
            expect(route).toBe('/error/callback');
        });
                
        // ARRANGE
        var instance = new SecurityService(routerMock, null, null);
        
        // ACT
        instance.authenticateResourceOwner("#state=none");
    }));

    it ("When the access token is not correct then redirect to login page", inject([Router], (routerMock) => {
        // ASSERT
        spyOn(routerMock, 'navigateByUrl').and.callFake(function(route) {
            expect(route).toBe('/error/callback');
        });
                
        // ARRANGE
        var introspectionResponse = new IntrospectionResponse();
        introspectionResponse.active = false;
        var identityServerService = new IdentityServerService(null, null);
        spyOn(identityServerService, 'introspectAccessToken').and.callFake(function(resp) {
            return {
                then(callback) {
                    callback(introspectionResponse);
                    return;
                }
            }
        });
        
        var instance = new SecurityService(routerMock, null, identityServerService);
        
        // ACT
        instance.authenticateResourceOwner("#state=none&access_token=invalid_token");
    }));

    it ("When the access token is correct then redirect to home page", inject([Router], (routerMock) => {
        // ASSERT
        spyOn(routerMock, 'navigateByUrl').and.callFake(function(route) {
            expect(route).toBe('/home');
        });
                
        // ARRANGE
        var introspectionResponse = new IntrospectionResponse();
        introspectionResponse.active = true;
        var identityServerService = new IdentityServerService(null, null);
        spyOn(identityServerService, 'introspectAccessToken').and.callFake(function(resp) {
            return {
                then(callback) {
                    callback(introspectionResponse);
                    return;
                }
            }
        });
        
        var instance = new SecurityService(routerMock, null, identityServerService);
        
        // ACT
        instance.authenticateResourceOwner("#state=none&access_token=valid_token");
    }));

});