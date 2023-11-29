using Ancestry.Models;
using Ancestry.Parsing;
using System.Collections.Generic;
using System.IO;

namespace Ancestry.Repository
{
    public class GEDCOMDataRepository
    {
        private readonly string _filePath;
        private List<GEDCOMIndividual> _allIndividuals;

        public GEDCOMDataRepository(string filePath)
        {
            _filePath = filePath;
            _allIndividuals = GetAllIndividualsFromFile();
        }

        private List<GEDCOMIndividual> GetAllIndividualsFromFile()
        {
            GEDCOMParser parser = new GEDCOMParser();
            return parser.ParseGEDCOM(_filePath);
        }

        public List<GEDCOMIndividual> GetAllIndividuals()
        {
            return _allIndividuals;
        }

        public GEDCOMIndividual GetIndividualByIndividualId(string individualId)
        {
            string formattedId = $"{individualId}"; // Add the 'I' prefix here

            // Fetch an individual by their GEDCOM-specific ID from the list obtained in GetAllIndividuals()
            return GetAllIndividuals().Find(i => i.GEDCOMId == formattedId);
        }

    }
}
