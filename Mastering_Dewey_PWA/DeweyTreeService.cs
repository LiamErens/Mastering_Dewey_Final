using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mastering_Dewey_PWA
{
    public class DeweyTreeService
    {
        private readonly Random random = new Random();


        
        public DeweyTreeNode BuildDeweyTree(string fileContent)
        {

            try
            {

                // Deserialize the JSON content to a JObject
                var jsonObject = JObject.Parse(fileContent);

                // Extract the 'DeweyTree' property from the JObject
                var deweyTreeProperty = jsonObject["DeweyTree"];

                if (deweyTreeProperty != null)
                {
                    // Deserialize the 'DeweyTree' property using the wrapper class
                    var deweyTree = deweyTreeProperty.ToObject<DeweyTreeNode>();

                    if (deweyTree != null)
                    {
                        return deweyTree;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //This code was adapted from How to map JSON to C# Objects
        //https://stackoverflow.com/questions/9988395/how-to-map-json-to-c-sharp-objects
        //Author bio link: https://stackoverflow.com/users/710502/user710502
        //Accessed: 16 Nov 2023

        public class DeweyTreeNode
        {
            [JsonProperty("Category")]
            public string Category { get; set; }

            [JsonProperty("Children")]
            public List<DeweyTreeNode> Children { get; set; }

            public bool ShouldSerializeChildren()
            {
                // Only serialize "Children" if it is not null and not empty
                return Children != null && Children.Any();
            }

            public DeweyTreeNode()
            {
                // Ensure that Children is initialized
                Children = new List<DeweyTreeNode>();
            }
        }

        public class DeweyTreeWrapper
        {
            [JsonProperty("DeweyTree")]
            public DeweyTreeNode DeweyTree { get; set; }
        }

        //This code was adapted from How to map JSON to C# Objects
        //https://stackoverflow.com/questions/9988395/how-to-map-json-to-c-sharp-objects
        //Author bio link: https://stackoverflow.com/users/710502/user710502
        //Accessed: 16 Nov 2023
        public string GetRandomThirdLevelCategory(DeweyTreeNode root)
        {
            // Flatten the tree to a list of all third-level categories
            List<DeweyTreeNode> thirdLevelCategories = FlattenDeweyTree(root)
                .Where(node => node.Children == null || !node.Children.Any())
                .ToList();

            // Check if the list is not empty
            if (thirdLevelCategories.Any())
            {
                // Randomly select a third-level category
                DeweyTreeNode randomThirdLevelCategory = thirdLevelCategories[random.Next(thirdLevelCategories.Count)];

                // Return the name of the random third-level category
                return randomThirdLevelCategory.Category;
            }
            else
            {
                // Handle the case where no third-level categories are found
                Console.WriteLine("No third-level categories found.");
                return string.Empty;
            }
        }

        private List<DeweyTreeNode> FlattenDeweyTree(DeweyTreeNode node)
        {
            List<DeweyTreeNode> flattenedTree = new List<DeweyTreeNode>();

            if (node != null)
            {
                flattenedTree.Add(node);

                if (node.Children != null)
                {
                    foreach (var child in node.Children)
                    {
                        flattenedTree.AddRange(FlattenDeweyTree(child));
                    }
                }
            }

            return flattenedTree;
        }

        private bool HasNonEmptyChildren(DeweyTreeNode node)
        {
            return node.Children != null && node.Children.Any(c => HasNonEmptyChildren(c));
        }


        //This code was adapted from String.Split() Method in C# with Examples
        //https://www.geeksforgeeks.org/string-split-method-in-c-sharp-with-examples/
        //Author: GeeksForGeeks
        //Author link: https://www.geeksforgeeks.org
        //Accessed: 16 Nov 2023
        private void AddNodeToTree(DeweyTreeNode parent, string line)
        {
            // Split the line into parts based on spaces
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // The first part is the category code
            string categoryCode = parts.Length > 1 ? parts[1] : string.Empty;

            // The rest of the parts form the category name
            string categoryName = string.Join(' ', parts.Skip(2));

            // Create a new node
            DeweyTreeNode node = new DeweyTreeNode
            {
                Category = categoryCode + ", " + categoryName
            };

            // Add the node to the parent
            parent.Children.Add(node);

            // Check if there are children indicated by square brackets
            if (line.Contains('['))
            {
                // Extract the content within square brackets
                int startIndex = line.IndexOf('[') + 1;
                int endIndex = line.IndexOf(']');
                string childrenContent = line.Substring(startIndex, endIndex - startIndex);

                // Split the content into child nodes
                string[] childrenLines = childrenContent.Split(',');

                // Recursively add child nodes
                foreach (var childLine in childrenLines)
                {
                    AddNodeToTree(node, childLine.Trim());
                }
            }
        }
    }
}

