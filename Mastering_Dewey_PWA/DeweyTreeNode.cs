using Newtonsoft.Json;

namespace Mastering_Dewey_PWA
{
    public class DeweyTreeNode
    {
        [JsonProperty("Category")]
        public string Category { get; set; }

        [JsonProperty("Children")]
        public List<DeweyTreeNode> Children { get; set; }

        public bool IsCorrectAnswer { get; set; } 


        public DeweyTreeNode()
        {
            // Ensure that Children is initialized
            Children = new List<DeweyTreeNode>();
        }
    }

}

//This code was adapted from How to map JSON to C# Objects
//https://stackoverflow.com/questions/9988395/how-to-map-json-to-c-sharp-objects
//Author bio link: https://stackoverflow.com/users/710502/user710502
//Accessed: 16 Nov 2023
