using System;
using System.IO;
using XForms.Store;
using XForms.Dependency;

namespace Trigger.BCL.ParaOffice
{
    public interface IErvRueckverkehrService
    {
        ErvRueckverkehr Get(string ervCode, DateTime rangeStart, DateTime rangeEnd);
    }
    
}
