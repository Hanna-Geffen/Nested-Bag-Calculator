<h1 align="center">Nested_Bag_Calculator</h1>
<h2 align="center">Given a set of rules that specify the required contents for different types of bags</h2>
<h3 align="center">How many bag colors can eventually contain at least one shiny gold bag?</h3>
<h3 align="center">How many individual bags are required inside a single shiny gold bag?</h3>
<p>
The program is written in C# and aims to solve the following problem: given a set of rules that specify the required contents for different types of bags, determine how many bag colors can eventually contain at least one shiny gold bag. Additionally, the program determines how many individual bags are required inside a single shiny gold bag.

The program starts by defining a class Pair that represents a pair of quantity and color of a contained bag. Then, the program defines two dictionaries: parentBags and childBags. parentBags stores a list of Pair objects for each parent bag, where the Pair objects represent the contained bags and their quantities. childBags stores a list of Pair objects for each child bag, where the Pair objects represent the containing bags and their quantities.

Next, the program reads in the input lines and builds the parentBags and childBags dictionaries. To do this, the program first extracts the color of the containing bag, called OuterBag, and then extracts the contained bags and their quantities. It then adds this information to the appropriate dictionary.

Once the data model is built, the program uses a recursive function, CountParentBags, to count the number of bag colors that can eventually contain at least one shiny gold bag. This function takes a bag color as input and recursively counts the number of bag colors that can contain that bag color. The function works by checking each containing bag of the input bag color and recursively calling itself on each of those containing bag colors. It also stores the containing bag colors in a HashSet to avoid counting duplicates.

Finally, the program uses another recursive function, CountChildBags, to count the number of individual bags that are required inside a single shiny gold bag. This function works in a similar way to CountParentBags, but it recursively counts the number of bags contained within a given bag color.

The program outputs the number of bag colors that can contain at least one shiny gold bag and the number of individual bags that are required inside a single shiny gold bag.
</p>
<h2 align="center">The output of my Program:</h2>
<h3 align="center"><bold>There are 279 bags containing eventually the shiny gold bag & a
shiny gold bags contains 39645 bags<bold></h3>

