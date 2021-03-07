using EntityManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EntityManagement.Services
{
    public class CsvParsingService : ICsvParsingService
    {
        public CompanyResponse ParseCsvFile(Stream input)
        {
            List<Company> companies = ParseCompanies(input);
            SetCompanyOwnershipIdsAndParents(companies);
            List<Annotation> annotations = CreateAnnotations(companies);
            var companyResponse = new CompanyResponse
            {
                Companies = companies,
                Annotations = annotations
            };
            return companyResponse;
        }

        private List<Company> ParseCompanies(Stream input)
        {
            List<Company> companies = new List<Company>();

            using (var reader = new StreamReader(input))
            {
                bool firstLine = true;

                int i = 1;

                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();

                    if (firstLine)
                    {
                        firstLine = false;
                        continue;
                    }

                    Company company = ParseCompany(line, i++);
                    companies.Add(company);
                }
            }

            return companies;
        }

        private Company ParseCompany(string inputLine, int id)
        {
            Company company = new Company();
            company.Id = id;

            int boardSeatIndex = ParseFirstStaticData(inputLine, company);
            int boardMembersIndex = ParseBoardMembers(inputLine, company, boardSeatIndex);
            int auditorIndex = ParseAuditors(inputLine, company, boardMembersIndex);
            int subsidiarySharesIndex = ParseOwnersAndSubsidiaries(inputLine, company, auditorIndex);
            int administeredByIndex = ParseAdministeredBy(inputLine, company, subsidiarySharesIndex);

            return company;
        }

        private int ParseFirstStaticData(string inputLine, Company company)
        {
            int companyIndex = inputLine.IndexOf(';');
            company.CompanyName = GetRawString(inputLine, 0, companyIndex);

            int orgNumIndex = GetNextSemicolonIndex(inputLine, companyIndex);
            company.OrganizationalNumber = GetRawString(inputLine, companyIndex, orgNumIndex);

            int internalCompanyIndex = GetNextSemicolonIndex(inputLine, orgNumIndex);
            company.InternalCompanyName = GetRawString(inputLine, orgNumIndex, internalCompanyIndex);

            int companyTypeIndex = GetNextSemicolonIndex(inputLine, internalCompanyIndex);
            company.CompanyType = GetRawString(inputLine, internalCompanyIndex, companyTypeIndex);

            int acquisitionIndex = GetNextSemicolonIndex(inputLine, companyTypeIndex);
            string acquisitionString = GetRawString(inputLine, companyTypeIndex, acquisitionIndex);
            company.Acquisition = acquisitionString.Equals("YES");

            int acquisitionDateIndex = GetNextSemicolonIndex(inputLine, acquisitionIndex);
            string acquistionDateString = GetRawString(inputLine, acquisitionIndex, acquisitionDateIndex);
            company.AcquisitionRegistrationDate = DateTimeOffset.Parse(acquistionDateString);

            int boardSeatIndex = GetNextSemicolonIndex(inputLine, acquisitionDateIndex);
            company.BoardSeat = GetRawString(inputLine, acquisitionDateIndex, boardSeatIndex);

            return boardSeatIndex;
        }

        private int GetNextSemicolonIndex(string inputLine, int startIndex)
        {
            return inputLine.IndexOf(';', startIndex + 1);
        }

        private String GetRawString(string inputLine, int startIndex, int stopIndex)
        {
            return inputLine.Substring(startIndex + 1, stopIndex - startIndex - 1);
        }

        private int ParseBoardMembers(string inputLine, Company company, int boardSeatIndex)
        {
            var boardMemberTumple = ParseMultiple(inputLine, boardSeatIndex);
            company.BoardMembers = boardMemberTumple.Item2.ToList();
            return boardMemberTumple.Item1;
        }

        private (int, string[]) ParseMultiple(string inputLine, int startIndex)
        {
            int startEndIndex = inputLine.IndexOf("\"\"", startIndex + 3);
            string rawString = inputLine.Substring(startIndex + 3, startEndIndex - startIndex - 3);
            string[] items = rawString.Split(';');
            return (startEndIndex + 2, items);
        }

        private int ParseAuditors(string inputLine, Company company, int boardMembersIndex)
        {
            int auditorIndex = GetNextSemicolonIndex(inputLine, boardMembersIndex);
            company.Auditor = GetRawString(inputLine, boardMembersIndex, auditorIndex);
            return auditorIndex;
        }

        private int ParseOwnersAndSubsidiaries(string inputLine, Company company, int auditorIndex)
        {
            var parentTuple = ParseCompanyAndOwnershipShares(inputLine, auditorIndex);
            company.ParentCompanies = parentTuple.Item2;
            var subsidiaryTuple = ParseCompanyAndOwnershipShares(inputLine, parentTuple.Item1);
            company.Subsidiaries = subsidiaryTuple.Item2;
            return subsidiaryTuple.Item1;
        }

        private (int, List<CompanyOwnership>) ParseCompanyAndOwnershipShares(string inputLine, int startIndex)
        {
            List<CompanyOwnership> companyOwnerships = new List<CompanyOwnership>();

            if (inputLine[startIndex + 1] == ';')
            {
                if (inputLine[startIndex + 2] == ';')
                {
                    return (startIndex + 2, companyOwnerships);
                }
            }

            if (inputLine[startIndex + 1] == '\"')
            {
                var companyTuple = ParseMultiple(inputLine, startIndex);
                (int, string[]) sharesTuple;

                if (inputLine[companyTuple.Item1 + 1] == ';')
                {
                    var emptyArray = Enumerable.Repeat<string>("-100%", companyTuple.Item2.Length).ToArray();
                    sharesTuple = (companyTuple.Item1 + 2, emptyArray);
                }
                else
                {
                    sharesTuple = ParseMultiple(inputLine, companyTuple.Item1);
                }

                for (int i=0; i<companyTuple.Item2.Length; i++)
                {
                    var parentCompanyOrgNr = companyTuple.Item2[i];
                    var parentCompanyShares = sharesTuple.Item2[i];

                    var companyOwnershipInner = new CompanyOwnership
                    {
                        OrganizationalNumber = parentCompanyOrgNr,
                        Percentage = ParsePercentageString(parentCompanyShares),
                        PercentageString = parentCompanyShares
                    };

                    companyOwnerships.Add(companyOwnershipInner);
                }

                return (sharesTuple.Item1, companyOwnerships);
            }
            
            int companyIndex = GetNextSemicolonIndex(inputLine, startIndex);
            string companyOrgNr = GetRawString(inputLine, startIndex, companyIndex);

            int sharesIndex = GetNextSemicolonIndex(inputLine, companyIndex);
            string shares = GetRawString(inputLine, companyIndex, sharesIndex);

            var companyOwnership = new CompanyOwnership
            {
                OrganizationalNumber = companyOrgNr,
                Percentage = ParsePercentageString(shares),
                PercentageString = shares
            };

            companyOwnerships.Add(companyOwnership);

            return (sharesIndex, companyOwnerships);
        }

        private double ParsePercentageString(string value)
        {
            return double.Parse(value.Trim().Replace("%", "")) / 100;
        }

        private int ParseAdministeredBy(string inputLine, Company company, int subsidiarySharesIndex)
        {
            int administeredByIndex = inputLine.IndexOf('\"', subsidiarySharesIndex);
            company.AdministeredBy = GetRawString(inputLine, subsidiarySharesIndex, administeredByIndex);
            return administeredByIndex;
        }

        private void SetCompanyOwnershipIdsAndParents(List<Company> companies)
        {
            foreach (var company in companies)
            {
                company.Parents = new List<int>();

                if (company.ParentCompanies.Count == 0)
                {
                    company.Parents.Add(1);
                }

                foreach (var parent in company.ParentCompanies)
                {
                    var lookupCompany = companies.FirstOrDefault<Company>(c => c.OrganizationalNumber == parent.OrganizationalNumber);
                    parent.Id = lookupCompany.Id;
                    if (!company.Parents.Contains(lookupCompany.Id))
                    {
                        company.Parents.Add(lookupCompany.Id);
                    }
                }
                foreach (var subsidiary in company.Subsidiaries)
                {
                    var lookupCompany = companies.FirstOrDefault<Company>(c => c.OrganizationalNumber == subsidiary.OrganizationalNumber);
                    subsidiary.Id = lookupCompany.Id;
                }
            }
        }

        private List<Annotation> CreateAnnotations(List<Company> companies)
        {
            List<Annotation> annotations = new List<Annotation>();

            foreach(var company in companies)
            {
                /*foreach(var subsidiary in company.Subsidiaries)
                {
                    var annotation = new Annotation
                    {
                        From = company.Id,
                        To = new List<int>
                        {
                            subsidiary.Id
                        },
                        Percentage = (!subsidiary.PercentageString.Equals("-100%")) ? subsidiary.PercentageString : "n/a"
                    };

                    annotations.Add(annotation);
                }*/

                foreach(var parent in company.ParentCompanies)
                {
                    var result = annotations.Find(a => a.From == parent.Id && a.To[0] == company.Id && a.Percentage == parent.PercentageString);

                    if (result != null)
                    {
                        continue;
                    }

                    var annotation = new Annotation
                    {
                        From = parent.Id,
                        To = new List<int>
                        {
                            company.Id
                        },
                        Percentage = (!parent.PercentageString.Equals("-100%")) ? parent.PercentageString : "n/a"
                    };

                    annotations.Add(annotation);
                }
            }

            return annotations;
        }
    }
}
