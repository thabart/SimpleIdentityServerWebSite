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

using System.Runtime.Serialization;

namespace SimpleIdentityServer.WebSite.Api.Host.DTOs.Responses
{
    [DataContract]
    public class ProfileResponse
    {
        [DataMember(Name = Constants.ProfileResponseNames.Subject)]
        public string Subject { get; set; }

        [DataMember(Name = Constants.ProfileResponseNames.Name)]
        public string Name { get; set; }

        [DataMember(Name = Constants.ProfileResponseNames.Picture)]
        public string Picture { get; set; }

        [DataMember(Name = Constants.ProfileResponseNames.AuthorizationServerUrl)]
        public string AuthorizationServerUrl { get; set; }

        [DataMember(Name = Constants.ProfileResponseNames.ManagerWebSiteUrl)]
        public string ManagerWebSiteUrl { get; set; }

        [DataMember(Name = Constants.ProfileResponseNames.ManagerWebSiteApiUrl)]
        public string ManagerWebSiteApiUrl { get; set; }

        [DataMember(Name = Constants.ProfileResponseNames.IsActive)]
        public bool IsActive { get; set; }
    }
}
