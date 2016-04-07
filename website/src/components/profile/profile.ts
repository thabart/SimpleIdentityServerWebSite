import { Component, OnInit } from 'angular2/core'

import { ProfileService, CreateProfileRequest, Profile } from '../services/profile.ts'

@Component({
    template: require('./profile.html')
})

export class ProfileComponent implements OnInit {
    isErrorNotDisplayed : boolean;
    isCreateProfileNotDisplayed : boolean;
    isDisplayProfileInformationNotDisplayed : boolean;
    isWaitingNotDisplayed : boolean;
    errorMessage : string;
    profile: Profile;
    name : string;
    constructor(
        private _profileService : ProfileService) { }
    ngOnInit() {
        this.isWaitingNotDisplayed = false;
        this.resetError();
        this.isCreateProfileNotDisplayed = true;
        this.isDisplayProfileInformationNotDisplayed = true;
        this._profileService.getCurrentProfile()
            .then(res => {
                this.displayProfile(res);
            })
            .catch(r => {
                this.displayError(r.json());
            });
    }
    createProfile() {
        this.isWaitingNotDisplayed = false;
        this.resetError();
        var request = new CreateProfileRequest();
        request.name = this.name;
        this._profileService.createProfile(request)
            .then(res => {
                this.isCreateProfileNotDisplayed = true;
                this.isDisplayProfileInformationNotDisplayed = false;
                this.profile = res;
             })
            .catch(r => {
                this.displayError(r.json());
            });
    }
    private resetError() {
        this.isErrorNotDisplayed = true;
        this.errorMessage = '';
    }
    private displayProfile(profile) {
        this.resetError();
        this.isWaitingNotDisplayed = true;
        this.isCreateProfileNotDisplayed = true;
        this.isDisplayProfileInformationNotDisplayed = false;
        this.profile = profile;
    }
    private displayError(error) {        
        this.isWaitingNotDisplayed = true;
        this.isErrorNotDisplayed = false;
        this.isCreateProfileNotDisplayed = false; 
        this.isDisplayProfileInformationNotDisplayed = true;
        if (!error.hasOwnProperty('error_description'))
        {
            this.errorMessage = 'an error occured while trying to contact the server';
        }
        else
        {
            this.errorMessage = error.error_description;
        }
    }
}