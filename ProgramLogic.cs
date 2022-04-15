using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LINQ {
    public class ProgramLogic {
        static void Main(string[] args) {
            PeopleData[] people;
            var fileContent = File.ReadAllText("people.json");
            people = JsonSerializer.Deserialize<PeopleData[]>(fileContent);
            //foreach (PeopleData item in people) {
            //    Console.WriteLine(item.Last_name);
            //}

            var orderedByLastName = people.OrderBy(pers => pers.Last_name).ToList();        //co vchazi a podle ceho radit
            foreach (var item in orderedByLastName) {
                Console.WriteLine($"{item.Last_name}");
            }
            //people.OrderBy(pers => pers.First_name).ToList().ForEach(per => Console.WriteLine(per.First_name));  //BEZ PROMENE VAR!!!

            var orderedByLanguage = people.OrderByDescending(per => per.Language).ToList();
            foreach (var item in orderedByLastName) {
                Console.WriteLine($"{item.Language}");
            }
            //people.OrderBy(person => person.First_name).ToList().ForEach(person => Console.WriteLine(person.First_name));

            //var ages = people
            //	.Select(person => new {
            //		person.Last_name,
            //		person.Date_of_birth
            //	})
            //	.Select(person => $"{person.Last_name} {(DateTime.Today - Convert.ToDateTime(person.Date_of_birth)).Days / 365}")
            //	.ToList();
            //foreach (var person in ages) {
            //	Console.WriteLine(person);
            //}

            //var ages2 = people
            //	.Select(person => new {
            //		person.Last_name,
            //		Age = (DateTime.Today - Convert.ToDateTime(person.Date_of_birth)).Days / 365
            //	})
            //	.Select(personWithDate => $"{personWithDate.Last_name} {personWithDate.Age}")
            //		.ToList();
            //foreach (var item in ages2) {
            //	Console.WriteLine(item);
            //}

            //GetPeopleOfLang(people);            //nejede .foreach proc???

            var startsWithBe = people.Where(person => person.First_name.StartsWith("Be")).ToList();

            //BEZ VAR JE MOZNY HNED VYPIS!!! JINAK TO NEJEDE, JE TO VOID FUNKCE FOREACH
            people.Where(person => person.First_name.StartsWith("A")).ToList().ForEach(person => Console.WriteLine(person.First_name));


        }

        public static List<PeopleData> GetPeopleOfLang(PeopleData[] people, string language) {
            return people.Where(person => person.Language == language).ToList();
        }

        public static object[] GetLAnguages(PeopleData[] people) {
            return people.Select(person => person.Language).Distinct().OrderBy(lang=>lang).ToArray();
            //po select jdou dale jen jazyky, order tedy lang=>lang - podle abecedy
        }

        public static List<PeopleData> LastNameContains(PeopleData[] people, string myString) {
            return people.Where(person => person.Last_name.Contains(myString)).ToList();
        }
    }
}
