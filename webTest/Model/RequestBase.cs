using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webTest.ViewModel;

namespace webTest.Model
{
    abstract class RequestBase
    {
        protected Logger log = MainViewModel.logger;
        public abstract void DoRequest();
    }
}
