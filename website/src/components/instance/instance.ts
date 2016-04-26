import { Component, OnInit } from 'angular2/core'
import { Settings } from '../settings.ts'

import { ProfileService, Profile } from '../services/profile.ts'
import { ContainerService } from '../services/container.ts'

@Component({
    template: require('./instance.html')
})

export class InstanceComponent implements OnInit {
    isErrorNotDisplayed : boolean;
    errorMessage : string;
    profile: Profile;
    isWaitingNotDisplayed : boolean;
    isLauncherNotDisplayed : boolean;
    isStartButtonNotDisplayed : boolean;
    isStopButtonNotDisplayed : boolean;
    constructor(
        private _profileService : ProfileService,
        private _containerService : ContainerService)
    { }
    ngOnInit() {
      this.displayStart();
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
      var name = this.profile.name;
      this._containerService.start(name)
          .then(res => {
            this.displayStop();
          })
          .catch(r => {
              this.displayError(r.json());
          });
    }
    stop() {
      var name = this.profile.name;
      this._containerService.stop(name)
          .then(res => {
              this.displayStart();
          })
          .catch(r => {
              this.displayError(r.json());
          });
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
    private displayStart() {
      this.isStartButtonNotDisplayed = false;
      this.isStopButtonNotDisplayed = true;
    }
    private displayStop() {
      this.isStartButtonNotDisplayed = true;
      this.isStopButtonNotDisplayed = false;
    }
}
