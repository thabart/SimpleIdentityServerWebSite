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
using SimpleIdentityServer.WebSite.Api.Core.Repositories;

namespace SimpleIdentityServer.WebSite.Api.Core.Validators
{
    public interface IContainerValidator
    {
        void CheckContainerExist(string containerName);
    }

    internal class ContainerValidator : IContainerValidator
    {
        private readonly IProfileRepository _profileRepository;

        #region Constructor

        public ContainerValidator(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        #endregion

        #region Public methods

        public void CheckContainerExist(string containerName)
        {
            var profile = _profileRepository.GetProfileByName(containerName);
            if (profile == null)
            {
                throw new IdentityServerException(ErrorCodes.InvalidRequestCode,
                   string.Format(ErrorDescriptions.TheContainerDoesntExist, containerName));
            }
        }

        #endregion
    }
}
