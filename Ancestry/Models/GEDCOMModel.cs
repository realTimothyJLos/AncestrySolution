namespace Ancestry.Models
{
    public class GEDCOMIndividual
    {
        public int Id { get; set; }
        public string GEDCOMId { get; set; } // GEDCOM-specific ID property

        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string DeathDate { get; set; }
        public string Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PlaceOfDeath { get; set; }
        public string Occupation { get; set; }
        public List<string> FamilyMembers { get; set; } // Example: Parents, Spouse, Children
        public List<string> Events { get; set; } // Example: Marriage, Divorce, Adoption
        public string Notes { get; set; }
        public List<GEDCOMSource> Sources { get; set; }

        public string IndividualId { get; set; } // ID for individual
        public string FamilyId { get; set; } // ID for family
    }
}
