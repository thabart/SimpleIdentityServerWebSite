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

namespace SimpleIdentityServer.WebSite.Api.Host
{
    public static class Constants
    {
        public static class RouteValues
        {
            public const string Root = "api";

            public const string Container = Root + "/containers";

            public const string Profile = Root + "/profiles";
        }

        public static class ProfileActions
        {
            public const string CurrentProfile = "current";
        }

        public static class DockerActions
        {
            public const string Start = "{name}/start";

            public const string Stop = "{name}/stop";
        }

        public static class ErrorResponseNames
        {
            public const string Error = "error";

            public const string ErrorDescription = "error_description";
        }

        public static class ProfileResponseNames
        {
            public const string Subject = "subject";

            public const string Name = "name";

            public const string Picture = "picture";

            public const string AuthorizationServerUrl = "authorization_server";

            public const string ManagerWebSiteUrl = "manager_website";

            public const string ManagerWebSiteApiUrl = "manager_website_api";

            public const string IsActive = "is_active";
        }

        public static class PostProfileRequestNames
        {
            public const string Name = "name";
        }

        public static class ClaimNames
        {
            public const string Subject = "sub";
        }
    }
}
