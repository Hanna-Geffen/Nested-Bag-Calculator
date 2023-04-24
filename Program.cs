using System;
using System.IO;
using System.Collections.Generic;

namespace Nested_Bag_Calculator
{
    class Program
    {
        //class pair represent a pair of quantity and color of a contained bag.
        class Pair
        {
            public int quantity;
            public string color;

            public Pair(int _quantity, string _color)
            {
                quantity = _quantity;
                color = _color;
            }
        }

        //a dictionary where: key- is a string color, value- is a list of pairs that defines the contained bags(and their quantities) in the key color bag.
        static Dictionary<string, List<Pair>> parentBags = new Dictionary<string, List<Pair>>();
        //a dictionary where: key- is a string color, value- is a list of pairs that defines the containing bags of the key color.
        static Dictionary<string, List<Pair>> childBags = new Dictionary<string, List<Pair>>();

        //parse the input lines and build the parent & child dictionaries
        static void BuildDataModel(string[] lines)
        {
            foreach (string line in lines)
            {
                int currIndex = 0, space = 2;
                string OuterBag = "";

                //extract the containing bag color called 'OuterBag'.
                while (space > 0)
                {
                    while (line[currIndex] != ' ')
                    {
                        OuterBag += line[currIndex];
                        currIndex++;
                    }
                    if (space > 1)//add only the first space
                    {
                        OuterBag += ' ';
                        currIndex++;
                    }
                    space--;
                }
                if (parentBags.ContainsKey(OuterBag))
                {
                    throw new Exception("Dictionary already contains this color of bag: " + OuterBag);
                }

                List<Pair> list = new List<Pair>();
                int quantity = 0;
                string InnerBag = "";
                currIndex += " bags contain ".Length;//skip " bags contain "

                //extract the contained bags
                while (line[currIndex] != '.')
                {
                    //extract the contained bag quantity
                    string quantityString = "";
                    while (line[currIndex] != ' ')
                    {
                        quantityString += line[currIndex];
                        currIndex++;
                    }
                    quantity = quantityString == "no" ? 0 : Convert.ToInt32(quantityString);
                    if (quantity == 0) { break; }//this bag contains 'no other bags' - break from the while loop

                    //extract the contained bag color called 'InnerBag'
                    space = 2;
                    InnerBag = "";
                    currIndex++;//read the space character
                    while (space > 0)
                    {
                        while (line[currIndex] != ' ')
                        {
                            InnerBag += line[currIndex];
                            currIndex++;
                        }
                        if (space > 1)//add only the first space
                        {
                            InnerBag += ' ';
                            currIndex++;
                        }
                        space--;
                    }

                    //fill child dictionary
                    if (!childBags.ContainsKey(InnerBag))
                    {
                        //this list will define all the bags containing the key color and the quantity of the key color contained.
                        childBags.Add(InnerBag, new List<Pair>());
                    }
                    childBags[InnerBag].Add(new Pair(quantity, OuterBag));

                    //add the pair to the list (for parent dictionary)
                    list.Add(new Pair(quantity, InnerBag));

                    currIndex += 4;//skip ' bag'
                    if (quantity > 1)
                    {
                        //skip 's' in case it is 'bags'
                        currIndex++;
                    }
                    //if the next character is ',' skip and go up to extract the next pair
                    //else go up and since it must be a '.' - we will exit the while loop
                    if (line[currIndex] == ',')
                    {
                        currIndex += 2;//skip ', '
                    }
                }
                //fill the parent dictionary
                //this list will define all the bags contained in the key color and their quantity.
                parentBags.Add(OuterBag, list);
            }
        }

        //calculate all the color bags containing the color parameter
        static long CalculateBagsContaining(string color)
        {
            long result = 0;

            if (childBags.ContainsKey(color))
            {
                foreach (var parent in childBags[color])
                {
                    //adding the color bag containing the inner bag directly
                    result++;
                    //adding in recursion all the bags containing the outer bag called 'parent'
                    result += CalculateBagsContaining(parent.color);
                }
            }

            return result;
        }

        //calculate all the color bags containing - for each color
        static void CalculateBagsContainingForAllColors()
        {
            foreach (var item in parentBags)
            {
                long result = CalculateBagsContaining(item.Key);
                Console.WriteLine(result + " bags contains " + item.Key + " bag");
            }
        }

        //calculate how many bags the color bag contains
        static long CalculateBagsContained(string color)
        {
            long result = 0;

            if (parentBags.ContainsKey(color))
            {
                foreach (var child in parentBags[color])
                {
                    //adding the quantity of the inner bag contained in the outer bag directly
                    result += child.quantity;
                    //adding in recursion all the bags contained in the inner bag * the quantity of the inner bag
                    result += child.quantity * CalculateBagsContained(child.color);
                }
            }

            return result;
        }

        //calculate how many bags are contained - for each color
        static void CalculateBagsContainedForAllColors()
        {
            foreach (var item in parentBags)
            {
                long result = CalculateBagsContained(item.Key);
                Console.WriteLine(item.Key + " bags contains " + result + " bags");
            }
        }

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\hanna\Desktop\Hanna-Geffen\Nested_Bag_Calculator\Input.txt");
            BuildDataModel(lines);

            string color = "shiny gold";

            long result = CalculateBagsContaining(color);
            Console.WriteLine("there are {0} bags containing eventually the {1} bag", result, color);

            result = CalculateBagsContained(color);
            Console.WriteLine("{0} bags contains {1} bags", color, result);

            CalculateBagsContainingForAllColors();
            //CalculateBagsContainedForAllColors();
        }
    }
}
