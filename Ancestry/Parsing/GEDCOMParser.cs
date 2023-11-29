using System;
using System.Collections.Generic;
using System.IO;
using Ancestry.Models;

namespace Ancestry.Parsing
{
    public class GEDCOMParser
    {
        public List<GEDCOMIndividual> ParseGEDCOM(string filePath)
        {
            List<GEDCOMIndividual> gedcomData = new List<GEDCOMIndividual>();
            GEDCOMIndividual currentIndividual = null;

            using (StreamReader streamReader = new StreamReader(filePath))
            {
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

                        // Create a new individual and assign GEDCOMId
                        currentIndividual = new GEDCOMIndividual();
                        string id = line.Split('@')[1]; // Extracting the ID between @ symbols
                        currentIndividual.GEDCOMId = $"@{id}@";

                    }
                    else if (line.StartsWith("1 NAME"))
                    {
                        if (currentIndividual != null)
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

            return gedcomData;
        }
    }
}
