using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Helpers
{
    public static class ViewFactory
    {
        public static T Resolve<T>() where T : class
        {
            return App.Services.GetRequiredService<T>();
        }
        
    }
}
