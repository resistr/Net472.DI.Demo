using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo.WebApplication
{
    public partial class About : Page
    {
        protected readonly ILogger Logger;

        public About()
            => Logger = ServiceProvider.Current.GetService<ILogger<About>>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Logger.LogWarning($"{nameof(About)}_{nameof(Page_Load)}");
        }
    }
}