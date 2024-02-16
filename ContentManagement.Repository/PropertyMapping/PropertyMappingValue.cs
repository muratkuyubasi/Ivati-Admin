using ContentManagement.Data;
using System.Collections.Generic;

namespace ContentManagement.Repository
{
    public class PropertyMappingValue
    {
        private List<FamilyNote> familyNotes;

        public IEnumerable<string> DestinationProperties { get; private set; }
        public bool Revert { get; private set; }

        public PropertyMappingValue(IEnumerable<string> destinationProperties,
            bool revert = false)
        {
            DestinationProperties = destinationProperties;
            Revert = revert;
        }

        public PropertyMappingValue(List<FamilyNote> familyNotes)
        {
            this.familyNotes = familyNotes;
        }
    }
}
