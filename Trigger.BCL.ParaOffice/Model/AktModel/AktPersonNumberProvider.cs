using System.Linq;
using XForms.Store;
using XForms.Dependency;

namespace Trigger.BCL.ParaOffice
{

    public class AktPersonNumberProvider
    {
        IStore Store
        {
            get
            {
                return MapProvider.Instance.ResolveType<IStore>();
            }
        }

        readonly AktPerson aktPerson;

        public AktPersonNumberProvider(AktPerson aktPerson)
        {
            this.aktPerson = aktPerson;
        }

        public int GetNextNumber()
        {
            if (aktPerson.Akt == null || aktPerson.Person == null)
                return 0;

            if (aktPerson.Partei == Partei.Partei1)
            {
                var partei1Seite = Store.LoadAll<AktPerson>().Where(p => p.Akt != null && p.Akt.MappingId.Equals(aktPerson.Akt.MappingId) && p.Partei == Partei.Partei1);

                return partei1Seite.Count() + 1;
            }
            if (aktPerson.Partei == Partei.Partei2)
            {
                var partei2Seite = Store.LoadAll<AktPerson>().Where(p => p.Akt != null && p.Akt.MappingId.Equals(aktPerson.Akt.MappingId) && p.Partei == Partei.Partei2);

                return partei2Seite.Count() + 1;
            }
            return 1;
        }
    }
    
}