using System;
using System.IO;
using XForms.Store;
using XForms.Dependency;

namespace Trigger.BCL.ParaOffice
{
    public class ErvRueckverkehrService : IErvRueckverkehrService
    {
        public ErvRueckverkehr Get(string ervCode, DateTime rangeStart, DateTime rangeEnd)
        {
            return ervCode == null ? null : CreateMock(ervCode);
        }

        static ErvRueckverkehr CreateMock(string ervCode)
        {
            var rv = new ErvRueckverkehr();
            rv.AktenZeichen = string.Format("1 C 332/{0} Z", 10);
            rv.Art = "LA";
            rv.EmpfangDatum = DateTime.Now;
            rv.ErvCode = ervCode;
            rv.Gericht = "BG Korneuburg (110)";
            rv.HinterlegungDatum = rv.EmpfangDatum.AddHours(-8);
            rv.Partei1 = "Max Muster";
            rv.Partei2 = "Anton Anwalt";

            rv.Save();

            var rvDok = new ErvRueckverkehrDokument();

            var file = "Running your application.pdf";
            var sourcePath = Path.Combine(MapProvider.Instance.ResolveInstance<IStoreConfiguration>().DocumentStoreLocation, file);

            MapProvider.Instance.ResolveType<IFileDataService>().AddFile(rvDok, sourcePath);

            rvDok.Subject = string.Format("Ladung Test - Code: {0}", ervCode);

            rvDok.Rueckverkehr = rv;

            rvDok.Save();

            return rv;
        }
    }
}
