using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeboDoGomesV2.Models
{
    internal class Writer
    {
        [JsonPropertyName("FullName")]
        public string? FullName { get; private set; }

        [JsonPropertyName("Nacionality")]
        public string? Nacionality { get; private set; }

        [JsonPropertyName("BirthDay")]
        public DateTime BirthDay { get; private set; }



        [JsonConstructor]
        public Writer(string FullName, string Nacionality, DateTime BirthDay)
        {
            this.FullName = FullName;
            this.Nacionality = Nacionality;
            this.BirthDay = BirthDay;
        }


        public Writer()
        {

        }

        public override string ToString()
        {
            return $"{FullName} ({Nacionality}) - Born on {BirthDay:dd/MM/yyyy}";
        }
    }
}
