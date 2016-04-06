import { Component, OnInit } from 'angular2/core'

import { ProfileService } from '../service/profile.ts'

@Component({
    template: require('./profile.html')
})

export class ProfileComponent implements OnInit {
    constructor(private _profileService : ProfileService) { }
    ngOnInit() {
        
        this._profileService.getCurrentProfile()
            .then(res => {
                console.log(res);
            })
            .catch(r => {
                console.log(r.json());
                console.log('coucou');
                console.log(r); 
            });
    }
}