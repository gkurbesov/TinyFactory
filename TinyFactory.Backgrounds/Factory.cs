using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Backgrounds
{
    public class Factory : TinyFactory
    {
        public Factory() : base() { }


        protected override void ConfigureFactory(IFactoryCollection collection)
        {
            collection.AddTransient<IDataSource, DataSource>();
            collection.AddHostedService<SchedulerA>();
            collection.AddHostedService<SchedulerB>();
            collection.AddHostedService(new SchedulerC());
        }
    }
}
