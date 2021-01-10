using System;
using System.Collections.Generic;
using System.Linq;

namespace Hahn.ApplicationProcess.December2020.Domain
{
    public class DomainException:AggregateException
    {
        public DomainException(IEnumerable<string> messages) :base( messages.Select(x => new Exception(x)))
        {
            
            
        }


    }
}