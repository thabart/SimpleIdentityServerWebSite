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

using SimpleIdentityServer.WebSite.Api.Core.Errors;
using SimpleIdentityServer.WebSite.Api.Core.Exceptions;
using SimpleIdentityServer.WebSite.Api.Core.Models;
using SimpleIdentityServer.WebSite.Api.Core.Repositories;
using System;

namespace SimpleIdentityServer.WebSite.Api.Core.Controllers.Profiles.Actions
{
    public interface IGetCurrentProfileAction
    {
        Profile Execute(string subject);
    }

    internal class GetCurrentProfileAction : IGetCurrentProfileAction
    {
        private readonly IProfileRepository _profileRepository;

        #region Constructor

        public GetCurrentProfileAction(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        #endregion

        #region Public methods

        public Profile Execute(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentNullException(nameof(subject));
            }

            var profile = _profileRepository.GetProfile(subject);
            if (profile == null)
            {
                throw new IdentityServerException(ErrorCodes.LoginRequiredCode,
                    string.Format(ErrorDescriptions.TheProfileDoesntExist, subject));
            }

            if (!profile.IsActive)
            {
                throw new IdentityServerException(ErrorCodes.LoginRequiredCode,
                    string.Format(ErrorDescriptions.TheProfileIsNotActive, subject));
            }

            return profile;
        }

        #endregion
    }
}
