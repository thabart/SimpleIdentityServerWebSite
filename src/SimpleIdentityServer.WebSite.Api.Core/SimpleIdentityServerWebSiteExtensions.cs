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

using Microsoft.Extensions.DependencyInjection;
using SimpleIdentityServer.WebSite.Api.Core.Controllers.Dockers;
using SimpleIdentityServer.WebSite.Api.Core.Controllers.Dockers.Operations;
using SimpleIdentityServer.WebSite.Api.Core.Controllers.Profiles;
using SimpleIdentityServer.WebSite.Api.Core.Controllers.Profiles.Actions;
using SimpleIdentityServer.WebSite.Api.Core.Factories;
using SimpleIdentityServer.WebSite.Api.Core.Validators;
using System;

namespace SimpleIdentityServer.WebSite.Api.Core
{
    public static class SimpleIdentityServerWebSiteExtensions
    {
        #region Public static methods

        public static IServiceCollection AddSimpleIdentityServerWebSite(
            this IServiceCollection serviceCollection,
            WebSiteOptions webSiteOptions)
        {
            RegisterServices(serviceCollection, webSiteOptions);
            return serviceCollection;
        }

        public static IServiceCollection AddSimpleIdentityServerWebSite(
            this IServiceCollection serviceCollection,
            Action<WebSiteOptions> callbackOptions)
        {
            if (callbackOptions == null)
            {
                throw new ArgumentNullException(nameof(callbackOptions));
            }

            var webSiteOptions = new WebSiteOptions();
            callbackOptions(webSiteOptions);
            RegisterServices(serviceCollection, webSiteOptions);
            return serviceCollection;
        }

        #endregion

        #region Private static methods

        private static void RegisterServices(
            IServiceCollection serviceCollection,
            WebSiteOptions webSiteOptions)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (webSiteOptions == null)
            {
                throw new ArgumentNullException(nameof(webSiteOptions));
            }

            // Register operations
            serviceCollection.AddTransient<IProfileActions, ProfileActions>();
            serviceCollection.AddTransient<IGetCurrentProfileAction, GetCurrentProfileAction>();
            serviceCollection.AddTransient<IAddProfileAction, AddProfileAction>();
            serviceCollection.AddTransient<IDockerOperations, DockerOperations>();
            serviceCollection.AddTransient<IStartDockerContainerOperation, StartDockerContainerOperation>();
            serviceCollection.AddTransient<IStopDockerContainerOperation, StopDockerContainerOperation>();

            // Register validator
            serviceCollection.AddTransient<IContainerValidator, ContainerValidator>();

            // Register factories
            serviceCollection.AddTransient<IDockerClientFactory, DockerClientFactory>();

            // Register global options
            serviceCollection.AddInstance(webSiteOptions);
        }

        #endregion
    }
}
