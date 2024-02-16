using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data.Models
{
    public class DebtorType
    {
        public DebtorType()
        {

            Debtors = new HashSet<Debtor>();

        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Debtor> Debtors { get; set; }
    }
}
