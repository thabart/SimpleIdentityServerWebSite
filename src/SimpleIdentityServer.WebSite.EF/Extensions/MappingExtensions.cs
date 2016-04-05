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

using Domain = SimpleIdentityServer.WebSite.Api.Core.Models;
using Model = SimpleIdentityServer.WebSite.EF.Models;

namespace SimpleIdentityServer.WebSite.EF.Extensions
{
    internal static class MappingExtensions
    {
        #region To Domain

        public static Domain.Profile ToDomain(this Model.Profile profile)
        {
            return new Domain.Profile
            {
                AuthorizationServerUrl = profile.AuthorizationServerUrl,
                IsActive = profile.IsActive,
                ManagerWebSiteApiUrl = profile.ManagerWebSiteApiUrl,
                ManagerWebSiteUrl = profile.ManagerWebSiteUrl,
                Picture = profile.Picture,
                Subject = profile.Subject
            };
        }

        #endregion
    }
}
