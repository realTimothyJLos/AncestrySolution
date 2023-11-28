using System.Collections.Generic;
using System.IO;
using Ancestry.Models;
using Ancestry.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Ancestry.Controllers
{
    public class GEDCOMController : Controller
    {
        private readonly GEDCOMDataRepository _dataRepository;

        public GEDCOMController(GEDCOMDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IActionResult Index()
        {
            List<GEDCOMIndividual> gedcomData = _dataRepository.GetAllIndividuals();
            return View(gedcomData);
        }

        private List<GEDCOMIndividual> ParseGEDCOM(string filePath)
        {
            List<GEDCOMIndividual> gedcomData = new List<GEDCOMIndividual>();

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    GEDCOMIndividual currentIndividual = null;

                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        // Assuming your GEDCOM file has lines representing individuals and their details
                        if (line.StartsWith("0 @") && line.EndsWith("@ INDI")) // Assuming individual record starts with "0 @...@ INDI"
                        {
                            if (currentIndividual != null)
                            {
                                // Set default "Living" if no death date is found
                                if (string.IsNullOrEmpty(currentIndividual.DeathDate))
                                {
                                    currentIndividual.DeathDate = "Living";
                                }

                                // Format name as SURNAME, FIRST NAME
                                string[] nameParts = currentIndividual.Name.Split('/');
                                if (nameParts.Length >= 2)
                                {
                                    currentIndividual.Name = $"{nameParts[1].Trim().ToUpper()}, {nameParts[0].Trim()}";
                                }
                                gedcomData.Add(currentIndividual);
                            }

                            currentIndividual = new GEDCOMIndividual();
                        }
                        else if (line.StartsWith("1 NAME"))
                        {
                            if (currentIndividual != null) // Ensure currentIndividual is not null before setting its properties
                            {
                                string[] parts = line.Split(' ');
                                currentIndividual.Name = string.Join(" ", parts[2..]); // Extract the name from the line
                            }
                        }
                        else if (line.StartsWith("1 BIRT"))
                        {
                            // Logic to handle birth date (similar to how name was handled)
                            // Example assuming birth date appears on the next line
                            string birthLine = streamReader.ReadLine();
                            string[] birthParts = birthLine.Split(' ');
                            currentIndividual.BirthDate = string.Join(" ", birthParts[2..]); // Extract the birth date from the line
                        }
                        else if (line.StartsWith("1 DEAT"))
                        {
                            // Logic to handle death date
                            string deathLine = streamReader.ReadLine();
                            string[] deathParts = deathLine.Split(' ');
                            currentIndividual.DeathDate = string.Join(" ", deathParts[2..]); // Extract the death date from the line
                        }
                        else if (line.StartsWith("1 SEX"))
                        {
                            // Logic to handle gender
                            string[] sexParts = line.Split(' ');
                            currentIndividual.Gender = sexParts[2]; // Extract the gender from the line
                        }
                    }

                    // Add the last individual after the loop ends
                    if (currentIndividual != null)
                    {
                        // Set default "Living" if no death date is found
                        if (string.IsNullOrEmpty(currentIndividual.DeathDate))
                        {
                            currentIndividual.DeathDate = "Living";
                        }

                        // Format name as SURNAME, FIRST NAME
                        string[] nameParts = currentIndividual.Name.Split('/');
                        if (nameParts.Length >= 2)
                        {
                            currentIndividual.Name = $"{nameParts[1].Trim().ToUpper()}, {nameParts[0].Trim()}";
                        }

                        gedcomData.Add(currentIndividual);
                    }
                }
            }

            return gedcomData;
        }
    }
}
