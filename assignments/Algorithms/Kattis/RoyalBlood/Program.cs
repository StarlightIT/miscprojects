using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace RoyalBlood
{
    public class Person : IComparable, IComparable<Person>, IComparer, IComparer<Person>
    {
        public string Name { get; set; }
        public double? RoyalBlood { get; set; }
        public List<Person> Parents { get; set; }
        public List<Person> Children { get; set; } 

        public int Compare(object x, object y)
        {
            return Compare((Person)x, (Person)y);
        }

        public int Compare([AllowNull] Person x, [AllowNull] Person y)
        {
            return x.Name.CompareTo(y.Name);
        }

        public int CompareTo(object obj)
        {
            return CompareTo((Person)obj);
        }

        public int CompareTo([AllowNull] Person other)
        {
            return Compare(this, other);
        }

        public override bool Equals(object obj)
        {
            return (Compare(this, (Person)obj) == 0);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            string line;
            List<string> input = new List<string>();

            while ((line = Console.ReadLine()) != null)
            {
                input.Add(line);
            }
            string heir = FindHeir(input);

            Console.WriteLine(heir);
        }

        public static string FindHeir(List<string> input)
        {
            var metadataParts = input[0].Split(' ');
            int lines = int.Parse(metadataParts[0]);
            int claimantsNum = int.Parse(metadataParts[0]);
            HashSet<Person> people = GetPeople(input, lines);
            HashSet<string> claimants = GetClaimants(input, lines, claimantsNum);
            var king = people.FirstOrDefault(p => p.Name == input[1]);
            king.RoyalBlood = 1.0;
            CalculateRoyalBlood(people);
            List<Person> claimantsByRoyalBlood = GetClaimantsByRoyalBlood(people, claimants);
            return claimantsByRoyalBlood[0].Name;
        }

        public static HashSet<Person> GetPeople(List<string> input, int length)
        {
            HashSet<Person> people = new HashSet<Person>();
            for (int i = 2; i < length + 2; i++)
            {
                string peopleRaw = input[i];
                var peopleNames = peopleRaw.Split(' ');
                List<Person> tempPeople = new List<Person>();

                foreach (var name in peopleNames)
                {
                    tempPeople.Add(new Person { Name = name });
                }

                Person child, parent1, parent2;

                var existingChild = people.FirstOrDefault(p => p.Name == peopleNames[0]);
                var p1temp = people.FirstOrDefault(p => p.Name == peopleNames[1]);
                var p2temp = people.FirstOrDefault(p => p.Name == peopleNames[2]);

                child = existingChild != null ? existingChild : tempPeople[0];
                parent1 = p1temp != null ? p1temp : tempPeople[1];
                parent2 = p2temp != null ? p2temp : tempPeople[2];

                if (child.Parents == null) { child.Parents = new List<Person>(); }
                if (parent1.Children == null) { parent1.Children = new List<Person>(); }
                if (parent2.Children == null) { parent2.Children = new List<Person>(); }

                child.Parents.Add(parent1);
                child.Parents.Add(parent2);

                parent1.Children.Add(child);
                parent2.Children.Add(child);

                foreach(var person in tempPeople)
                {
                    people.Add(person);
                }
            }

            return people;
        }

        public static void CalculateRoyalBlood(HashSet<Person> people)
        {
            var lastDesendants = people.ToList().FindAll(p => p.Children == null || !p.Children.Any());
            foreach(var descendant in lastDesendants)
            {
                CalculateRoyalBloodByParents(descendant);
            }
        }

        public static void CalculateRoyalBloodByParents(Person person)
        {
            if (!person.RoyalBlood.HasValue && person.Parents != null && person.Parents.Any())
            {
                Person parent1 = person.Parents[0];
                Person parent2 = person.Parents[1];

                if (!parent1.RoyalBlood.HasValue)
                {
                    CalculateRoyalBloodByParents(parent1);
                }

                if (!parent2.RoyalBlood.HasValue)
                {
                    CalculateRoyalBloodByParents(parent2);
                }

                person.RoyalBlood = parent1.RoyalBlood / 2 + parent2.RoyalBlood / 2;
            }

            if (person.Parents == null && !person.RoyalBlood.HasValue)
            {
                person.RoyalBlood = 0.0;
                return;
            }
        }

        public static HashSet<string> GetClaimants(List<string> input, int lines, int claimantsNum)
        {
            HashSet<string> claimants = new HashSet<string>();

            for (int i=2+lines; i<input.Count; i++)
            {
                claimants.Add(input[i]);
            }

            return claimants;
        }

        public static List<Person> GetClaimantsByRoyalBlood(HashSet<Person> people, HashSet<string> claimants)
        {
            HashSet<Person> claimantsSet = new HashSet<Person>();

            foreach(string claimant in claimants)
            {
                Person person = people.FirstOrDefault(p => p.Name == claimant);
                if (person != null)
                {
                    claimantsSet.Add(person);
                }
            }

            List<Person> claimsByRoyalBlood = claimantsSet.OrderByDescending(p => p.RoyalBlood).ToList();
            return claimsByRoyalBlood;
        }
    }
}
