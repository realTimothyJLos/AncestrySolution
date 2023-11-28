using Ancestry.Models;

namespace Ancestry.Repository
{
    public class GEDCOMDataRepository
    {
        private readonly string _filePath; 

        public GEDCOMDataRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<GEDCOMIndividual> GetAllIndividuals()
        {
            List<GEDCOMIndividual> individuals = new List<GEDCOMIndividual>();

            using (StreamReader streamReader = new StreamReader(_filePath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    // Parse GEDCOM file lines and extract individual data
                    // Sample logic to create GEDCOMIndividual objects:
                    if (line.StartsWith("0 @") && line.EndsWith("@ INDI"))
                    {
                        // Logic to extract individual details from the GEDCOM file
                        GEDCOMIndividual individual = ParseLineToIndividual(line);
                        if (individual != null)
                        {
                            individuals.Add(individual);
                        }
                    }
                }
            }

            return individuals;
        }

        public GEDCOMIndividual GetIndividualByGEDCOMId(string gedcomId)
        {
            // Fetch an individual by GEDCOM-specific ID from the list obtained in GetAllIndividuals()
            return GetAllIndividuals().Find(i => i.GEDCOMId == gedcomId);
        }


        private GEDCOMIndividual ParseLineToIndividual(string line)
        {
            // Logic to parse a line from the GEDCOM file and create a GEDCOMIndividual object
            // Implement according to your GEDCOM file structure
            // Example parsing logic:
            GEDCOMIndividual individual = new GEDCOMIndividual();
            // Populate individual properties from the line data
            return individual;
        }
    }
}
