using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Demo.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service2.svc or Service2.svc.cs at the Solution Explorer and start debugging.
    public class Service2 : IService2
    {
        protected readonly ILogger Logger;

        public Service2()
            => Logger = ServiceProvider.Current.GetService<ILogger<Service2>>();

        public void DoWork()
        {
            Logger.LogWarning(nameof(DoWork));
        }
    }
}
