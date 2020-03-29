using NUnit.Framework;
using ActiveCruzer.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace ActiveCruzer.AutoMapper.Tests
{
    [TestFixture()]
    public class UserMapperProfileTests
    {
        [Test]
        public void AutoMapper_Configuration_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<UserMapperProfile>();
            });
            configuration.AssertConfigurationIsValid();
        }

    }
}