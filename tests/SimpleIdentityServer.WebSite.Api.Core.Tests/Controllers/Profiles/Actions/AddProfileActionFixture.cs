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

using Moq;
using SimpleIdentityServer.WebSite.Api.Core.Configuration;
using SimpleIdentityServer.WebSite.Api.Core.Controllers.Profiles.Actions;
using SimpleIdentityServer.WebSite.Api.Core.Errors;
using SimpleIdentityServer.WebSite.Api.Core.Exceptions;
using SimpleIdentityServer.WebSite.Api.Core.Models;
using SimpleIdentityServer.WebSite.Api.Core.Parameters;
using SimpleIdentityServer.WebSite.Api.Core.Repositories;
using System;
using Xunit;

namespace SimpleIdentityServer.WebSite.Api.Core.Tests.Controllers.Profiles.Actions
{
    public class AddProfileActionFixture
    {
        private Mock<IProfileRepository> _profileRepositoryStub;

        private Mock<IEndPointConfiguration> _endPointConfigurationStub;

        private IAddProfileAction _addProfileAction;

        #region Exceptions

        [Fact]
        public void When_Passing_Null_Parameter_Then_Exception_Is_Thrown()
        {
            // ARRANGE
            InitializeFakeObjects();

            // ACT & ASSERT
            Assert.Throws<ArgumentNullException>(() => _addProfileAction.Execute(null));
        }

        [Fact]
        public void When_Passing_No_Subject_Then_Exception_Is_Thrown()
        {
            // ARRANGE
            InitializeFakeObjects();
            var addProfileParameter = new AddProfileParameter();

            // ACT & ASSERT
            Assert.Throws<ArgumentNullException>(() => _addProfileAction.Execute(addProfileParameter));
        }

        [Fact]
        public void When_Passing_No_Name_Then_Exception_Is_Thrown()
        {
            // ARRANGE
            InitializeFakeObjects();
            var addProfileParameter = new AddProfileParameter
            {
                Subject = "subject"
            };

            // ACT & ASSERT
            Assert.Throws<ArgumentNullException>(() => _addProfileAction.Execute(addProfileParameter));
        }

        [Fact]
        public void When_Profile_Already_Exists_Then_Exception_Is_Thrown()
        {            
            // ARRANGE
            InitializeFakeObjects();
            var addProfileParameter = new AddProfileParameter
            {
                Subject = "subject",
                Name = "name"
            };
            var profile = new Profile();
            _profileRepositoryStub.Setup(p => p.GetProfileBySubject(It.IsAny<string>()))
                .Returns(profile);

            // ACT & ASSERT
            var exception = Assert.Throws<IdentityServerException>(() => _addProfileAction.Execute(addProfileParameter));
            Assert.True(exception.Code == ErrorCodes.InvalidRequestCode);
            Assert.True(exception.Message == ErrorDescriptions.TheProfileAlreadyExists);
        }

        #endregion

        #region Happy path

        [Fact]
        public void When_Adding_Profile_Then_Operation_Is_Called()
        {
            // ARRANGE
            InitializeFakeObjects();
            var addProfileParameter = new AddProfileParameter
            {
                Subject = "subject",
                Name = "name"
            };
            _profileRepositoryStub.Setup(p => p.AddProfile(It.IsAny<Profile>()))
                .Returns(true);

            // ACT
            var result = _addProfileAction.Execute(addProfileParameter);

            // ASSERTS
            Assert.NotNull(result);
            _profileRepositoryStub.Verify(p => p.AddProfile(It.IsAny<Profile>()));
        }

        #endregion

        private void InitializeFakeObjects()
        {
            _profileRepositoryStub = new Mock<IProfileRepository>();
            _endPointConfigurationStub = new Mock<IEndPointConfiguration>();
            _addProfileAction = new AddProfileAction(
                _profileRepositoryStub.Object,
                _endPointConfigurationStub.Object);
        }
    }
}
