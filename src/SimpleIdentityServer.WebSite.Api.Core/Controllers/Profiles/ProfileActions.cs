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

using SimpleIdentityServer.WebSite.Api.Core.Models;
using SimpleIdentityServer.WebSite.Api.Core.Controllers.Profiles.Actions;
using SimpleIdentityServer.WebSite.Api.Core.Parameters;

namespace SimpleIdentityServer.WebSite.Api.Core.Controllers.Profiles
{
    public interface IProfileActions
    {
        Profile GetCurrentProfile(string subject);

        Profile AddProfile(AddProfileParameter addProfileParameter);
    }

    public class ProfileActions : IProfileActions
    {
        private readonly IGetCurrentProfileAction _getCurrentProfileAction;

        private readonly IAddProfileAction _addProfileAction;

        #region Constructor

        public ProfileActions(
            IGetCurrentProfileAction getCurrentProfileAction,
            IAddProfileAction addProfileAction)
        {
            _getCurrentProfileAction = getCurrentProfileAction;
            _addProfileAction = addProfileAction;
        }

        #endregion

        #region Public methods

        public Profile GetCurrentProfile(string subject)
        {
            return _getCurrentProfileAction.Execute(subject);
        }

        public Profile AddProfile(AddProfileParameter addProfileParameter)
        {
            return _addProfileAction.Execute(addProfileParameter);
        }

        #endregion
    }
}
