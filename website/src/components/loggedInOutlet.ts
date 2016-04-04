import { Directive,
    ElementRef,
    DynamicComponentLoader,
    Attribute } from 'angular2/core';
import { RouterOutlet, ComponentInstruction, Router  } from 'angular2/router'
import { IntrospectionRequest, IdentityServerService } from './service/identityserver.ts'

@Directive({
    selector: 'loggedin-router-outlet'
})

export class LoggedInOutlet extends RouterOutlet 
{
    constructor(
        private _viewContainer: ElementRef,
        private _compiler: DynamicComponentLoader,
        private _router: Router,
        @Attribute('name') _nameAttr: string,
        private _identityServerService : IdentityServerService)
    {
         super(_viewContainer, _compiler, _router, _nameAttr);
    }
    activate(instruction: ComponentInstruction)
    {
        var accessToken = localStorage.getItem('access_token_authorization_server');
        var url = this._router.lastNavigationAttempt;
        if (!accessToken)
        {
            this._router.navigateByUrl('/login');
            return super.activate(instruction);
        }
        else
        {
            var request = new IntrospectionRequest();
            request.token = accessToken;
            return this._identityServerService.introspectAccessToken(request)
                .then(res => {
                    if (res.active == false)
                    {                    
                        this._router.navigateByUrl('/login');
                    }
                    else if (url == '/login')
                    {
                        this._router.navigate(['Management']);      
                    }
                    
                    return super.activate(instruction);
                });
        }
    }    
}