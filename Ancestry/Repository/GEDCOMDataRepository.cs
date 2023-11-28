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

        public GEDCOMIndividual GetIndividualByIndividualId(string individualId)
        {
            // Assuming individualId is passed as I422532423762 in the URL
            string formattedId = $"@I{individualId}@"; // Format it to match GEDCOM IDs

            // Fetch an individual by their GEDCOM-specific ID from the list obtained in GetAllIndividuals()
            return GetAllIndividuals().Find(i => i.GEDCOMId == formattedId);
        }


        private GEDCOMIndividual ParseLineToIndividual(string line)
        {

            // Create individual object
            GEDCOMIndividual individual = new GEDCOMIndividual();

            // Extract and set GEDCOMId
            string currentId = line.Split('@')[1]; // Extracting the ID between @ symbols
            individual.GEDCOMId = currentId;
            
            return individual;
        }
    }
}
