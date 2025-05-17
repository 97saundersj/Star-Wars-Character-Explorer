using System;

namespace StarWarsCharactersWebAPI.Models
{
    public class StarWarsCharacter
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }

    public class StarWarsResponse
    {
        public StarWarsInfo info { get; set; }
        public List<StarWarsCharacter> data { get; set; }
    }

    public class StarWarsInfo
    {
        public int total { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public string prev { get; set; }
    }
} 