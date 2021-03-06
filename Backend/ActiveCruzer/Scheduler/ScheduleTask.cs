﻿using ActiveCruzer.BLL;
using ActiveCruzer.DAL.DataContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ActiveCruzer.BLL;

namespace ActiveCruzer.Scheduler
{
    public class ScheduleTask : ScheduledProcessor
    {
        public ScheduleTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        /// <summary>
        ///  runs every day at 1 AM
        /// </summary>
        protected override string Schedule => "* 18 * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var _userbll = serviceProvider.GetRequiredService<UserBLL>();
            var _requestBll = serviceProvider.GetRequiredService<RequestBll>();
            var executed = _userbll.OverdueUsersDeleted();
            _requestBll.CloseOverDueRequests();
            return Task.CompletedTask;
        }
    }
}
