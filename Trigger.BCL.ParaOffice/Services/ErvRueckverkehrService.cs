using System;
using System.IO;
using XForms.Store;
using XForms.Dependency;

namespace Trigger.BCL.ParaOffice
{
    public class ErvRueckverkehrService
    {
        public void Load(int items)
        {
            for (int i = 0; i < items; i++)
            {
                var rv = new ErvRueckverkehr();
                rv.AktenZeichen = string.Format("1 C 332/{0} Z", i + 10);
                rv.Art = "LA";
                rv.EmpfangDatum = DateTime.Now;
                rv.ErvCode = "R123456";
                rv.Gericht = "BG Korneuburg (110)";
                rv.HinterlegungDatum = rv.EmpfangDatum.AddHours(-8);
                rv.Partei1 = "Max Muster";
                rv.Partei2 = "Anton Anwalt";

                rv.Save();

                var rvDok = new ErvRueckverkehrDokument();

                var file = "Running your application.pdf";
                var sourcePath = Path.Combine(MapProvider.Instance.ResolveInstance<IStoreConfiguration>().DocumentStoreLocation, file);

                MapProvider.Instance.ResolveType<IFileDataService>().AddFile(rvDok, sourcePath);

                rvDok.Subject = string.Format("Ladung Test - Nr.: {0}", i);

                rvDok.Rueckverkehr = rv;

                rvDok.Save();
            }
        }
    }
}
