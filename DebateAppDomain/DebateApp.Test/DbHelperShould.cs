﻿using DebateApp.Domain;
using DebateAppDomainAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace DebateAppDomain.Test
{
    public class DbHelperShould
    {
        private readonly ITestOutputHelper _output;
        private DebateModel dmut;
        private User uut;
        private Debate dut;
        private DBHelper dbh;
        private UserModel umut;
        public DbHelperShould(ITestOutputHelper Output)
        {
            _output = Output;
            uut = new User(10, "Steve Harvey", 200);
            umut = new UserModel()
            {
                Username = "SteveHarvey3",
                Password = "IHateMyself6969"
            };
            dut = new Casual(uut, "Are we any good at this?", "Grown Up Problems", "Not yet...");
            dmut = new DebateModel(dut);
            dbh = new DBHelper();
        }
        [Fact]
        public void BeAbleToRetrieveAUserModelByID()
        {
            var tst = dbh.DBGetUser(2);
            tst.Transfer();
            Assert.True(tst.Username == "Greg");
            Assert.True(tst.UserLogic.Username == "Greg");
        }
        [Fact]
        public void BeAbleToRetrieveADebateModelByID()
        {
            var id =dbh.DBCreateDebate(dmut).d.Debate_ID;
            var tst = dbh.DBGetDebate(id);
            Assert.NotNull(tst.d);
            Assert.IsType<Debate>(tst.d);
        }
        [Fact]
        public void CreateAUser()
        {
            var id = dbh.DBGetUser("SteveHarvey3").AccountId;
            dbh.DBDeleteUser(id);
            dbh.DBCreateUser(umut);
            var actual = dbh.DBGetUser("SteveHarvey3");
            _output.WriteLine(actual.ToString());
            Assert.IsType<UserModel>(actual);
            Assert.Equal("SteveHarvey3", actual.Username);

        }
        [Fact]
        public void GetAUserByName()
        {
            var actual = dbh.DBGetUser("Greg");
            Assert.Equal(actual.Username, "Greg");
        }
        [Fact]
        public void CheckUsernameUnique()
        {
            var actual = dbh.CheckUsername("Greg");
            var actual2 = dbh.CheckUsername("FOROASUFHOAUFHUOEHFOAUHSFAJO");
            Assert.False(actual);
            Assert.True(actual2);
        }
        [Fact]
        public void SaveAnUpdatedDebate()
        {
            var get = dbh.DBGetDebate(1);
            get.d.DebateTopic = "Can the dbapi update a debate?";
            dbh.DBSaveDebateChanges(get);

            var result = dbh.DBGetDebate(1);
            Assert.Equal("Can the dbapi update a debate?", result.d.DebateTopic);
            result.d.DebateTopic = "Almost there?";
            dbh.DBSaveDebateChanges(result);
        }

    }
}
