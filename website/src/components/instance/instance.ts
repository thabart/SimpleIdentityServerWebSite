import { Component, OnInit } from 'angular2/core'
import { Settings } from '../settings.ts'

import { ProfileService, Profile } from '../services/profile.ts'

@Component({
    template: require('./instance.html')
})

export class InstanceComponent implements OnInit {
    isErrorNotDisplayed : boolean;
    errorMessage : string;
    profile: Profile;
    isWaitingNotDisplayed : boolean;
    isLauncherNotDisplayed : boolean;
    constructor(
        private _profileService : ProfileService)
    { }
    ngOnInit() {
      this.isLauncherNotDisplayed = true;
      this.isWaitingNotDisplayed = false;
      this.resetError();
      this._profileService.getCurrentProfile()
          .then(res => {
              this.displayLauncher(res);
          })
          .catch(r => {
              this.displayError(r.json());
          });
    }
    start() {
      console.log("start instance");
    }
    private resetError() {
        this.isErrorNotDisplayed = true;
        this.errorMessage = '';
    }
    private displayLauncher(profile) {
      this.profile = profile;
      this.isLauncherNotDisplayed = false;
      this.isWaitingNotDisplayed = true;
    }
    private displayError(error) {
        this.isLauncherNotDisplayed = true;
        this.isWaitingNotDisplayed = true;
        this.isErrorNotDisplayed = false;
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
