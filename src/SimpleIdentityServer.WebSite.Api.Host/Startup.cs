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

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using SimpleIdentityServer.UserInformation.Authentication;
using SimpleIdentityServer.WebSite.Api.Core;
using SimpleIdentityServer.WebSite.Api.Core.Configuration;
using SimpleIdentityServer.WebSite.Api.Host.Configuration;
using SimpleIdentityServer.WebSite.Api.Host.Middlewares;
using SimpleIdentityServer.WebSite.Api.Host.Swagger;
using SimpleIdentityServer.WebSite.EF;
using Swashbuckle.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace SimpleIdentityServer.WebSite.Api.Host
{
    public class Startup
    {
        private class AssignOauth2SecurityRequirements : IOperationFilter
        {
            public void Apply(Operation operation, OperationFilterContext context)
            {
                var assignedScopes = new List<string>
                {
                    "openid"
                };

                var oauthRequirements = new Dictionary<string, IEnumerable<string>>
                {
                    {
                        "oauth2", assignedScopes
                    }
                };

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
                operation.Security.Add(oauthRequirements);
            }
        }

        private class SimpleIdentityServerOptions
        {
            public string UserInformationUrl { get; set; }

            public string AuthorizationUrl { get; set; }

            public string TokenUrl { get; set; }

            public string ConnectionString { get; set; }
        }

        private SimpleIdentityServerOptions _simpleIdentityServerOptions;

        #region Properties

        public IConfigurationRoot Configuration { get; set; }

        #endregion

        #region Public methods

        public Startup(IHostingEnvironment env,
            IApplicationEnvironment appEnv)
        {
            // Load all the configuration information from the "json" file & the environment variables.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            InitializeOptions();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddSwaggerGen();
            services.ConfigureSwaggerDocument(opts =>
            {
                opts.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Simple identity website",
                    TermsOfService = "None"
                });
                opts.SecurityDefinitions.Add("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = _simpleIdentityServerOptions.AuthorizationUrl,
                    TokenUrl = _simpleIdentityServerOptions.TokenUrl,
                    Description = "Implicit flow",
                    Scopes = new Dictionary<string, string>
                    {
                        { "openid", "OpenId" },
                        { "role" , "Get the roles" },
                        { "profile" , "Get the profile" }
                    }
                });
                opts.OperationFilter<AssignOauth2SecurityRequirements>();
            });
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProvider = new CompositeFileProvider(
                    new EmbeddedFileProvider(
                        typeof(SwaggerUiController).GetTypeInfo().Assembly,
                        "SimpleIdentityServer.WebSite.Api.Host"
                    ),
                    options.FileProvider
                );
            });

            services.AddMvc();

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()));

            RegisterDependencies(services);
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            var userInformationUrl = Configuration["UserInfoUrl"];

            loggerFactory.AddConsole();

            app.UseStatusCodePages();

            app.UseExceptionHandler();

            var userInformationOptions = new UserInformationOptions
            {
                UserInformationEndPoint = userInformationUrl
            };
            app.UseAuthenticationWithUserInformation(userInformationOptions);

            app.UseCors("AllowAll");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });

            app.UseSwaggerGen();
            app.UseSwaggerUi();
        }

        #endregion

        #region Public static methods

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

        #endregion

        #region Private methods

        private void InitializeOptions()
        {
            _simpleIdentityServerOptions = new SimpleIdentityServerOptions
            {
                AuthorizationUrl = Configuration["AuthorizationUrl"],
                UserInformationUrl = Configuration["UserInfoUrl"],
                TokenUrl = Configuration["TokenUrl"],
                ConnectionString = Configuration["Data:DefaultConnection:ConnectionString"]
            };
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddSimpleIdentityServerWebSite(opt =>
            {
                opt.IsHttpsAuthentication = true;
                opt.IsCertificateSelfSigned = true;
                opt.Certificate = CertificateProvider.Get();
                opt.DockerApiUri = new Uri("https://192.168.99.100:2376");
            });
            services.AddTransient<IEndPointConfiguration, EndPointConfiguration>();
            services.AddSimpleIdentityServerEf(_simpleIdentityServerOptions.ConnectionString);
        }

        #endregion
    }
}
