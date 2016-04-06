#region copyright
// Copyright 2015 Habart Thierry
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using SimpleIdentityServer.WebSite.Api.Core.Configuration;
using SimpleIdentityServer.WebSite.Api.Core.Errors;
using SimpleIdentityServer.WebSite.Api.Core.Exceptions;
using SimpleIdentityServer.WebSite.Api.Core.Models;
using SimpleIdentityServer.WebSite.Api.Core.Parameters;
using SimpleIdentityServer.WebSite.Api.Core.Repositories;
using System;

namespace SimpleIdentityServer.WebSite.Api.Core.Controllers.Profiles.Actions
{
    public interface IAddProfileAction
    {
        Profile Execute(AddProfileParameter addProfileParameter);
    }

    internal class AddProfileAction : IAddProfileAction
    {
        private readonly IProfileRepository _profileRepository;

        private readonly IEndPointConfiguration _endPointConfiguration;

        #region Constructor

        public AddProfileAction(
            IProfileRepository profileRepository,
            IEndPointConfiguration endPointConfiguration)
        {
            _profileRepository = profileRepository;
            _endPointConfiguration = endPointConfiguration;
        }

        #endregion

        #region Public methods

        public Profile Execute(AddProfileParameter addProfileParameter)
        {
            if (addProfileParameter == null)
            {
                throw new ArgumentNullException(nameof(addProfileParameter));
            }

            if (string.IsNullOrWhiteSpace(addProfileParameter.Subject))
            {
                throw new ArgumentNullException(nameof(addProfileParameter.Subject));
            }

            if (string.IsNullOrWhiteSpace(addProfileParameter.Name))
            {
                throw new ArgumentNullException(nameof(addProfileParameter.Name));
            }

            var isExistingProfile = _profileRepository.GetProfileBySubject(addProfileParameter.Subject) != null
                || _profileRepository.GetProfileByName(addProfileParameter.Name) != null;
            if (isExistingProfile)
            {
                throw new IdentityServerException(ErrorCodes.InvalidRequestCode,
                    ErrorDescriptions.TheProfileAlreadyExists);
            }

            var newProfile = new Profile
            {
                Subject  = addProfileParameter.Subject,
                Name = addProfileParameter.Name,
                IsActive = true,
                AuthorizationServerUrl = _endPointConfiguration.GetAuthorizationServer(addProfileParameter.Name),
                ManagerWebSiteApiUrl = _endPointConfiguration.GetManagerWebSiteApi(addProfileParameter.Name),
                ManagerWebSiteUrl = _endPointConfiguration.GetManagerWebSite(addProfileParameter.Name)
            };

            var isProfileAdded = _profileRepository.AddProfile(newProfile);
            if (!isProfileAdded)
            {
                return null;
            }

            return newProfile;
        }

        #endregion
    }
}
