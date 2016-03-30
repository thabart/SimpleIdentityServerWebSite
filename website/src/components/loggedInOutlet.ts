import { Directive,
    ElementRef,
    DynamicComponentLoader,
    Attribute } from 'angular2/core';
import { RouterOutlet, ComponentInstruction, Router  } from 'angular2/router'

@Directive({
    selector: 'loggedin-router-outlet'
})

export class LoggedInOutlet extends RouterOutlet 
{
    constructor(
        private _viewContainer: ElementRef,
        private _compiler: DynamicComponentLoader,
        private _router: Router,
        @Attribute('name') _nameAttr: string)
    {
         super(_viewContainer, _compiler, _router, _nameAttr);
    }
    activate(instruction: ComponentInstruction)
    {
        var url = this._router.lastNavigationAttempt;
        if (!localStorage.getItem('access_token_authorization_server'))
        {
            this._router.navigateByUrl('/login');
            return super.activate(instruction);
        }
    }    
}