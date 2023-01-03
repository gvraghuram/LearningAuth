using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASSOne.Platform.Core.Errors
{
    public static class Errors
    {
        public static class Application
        {
            public static Error CreateApplicationError()
            {
                return new Error("Testing", "Testing");
            }
        }
    }
}
