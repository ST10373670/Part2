using System;
using System.Collections.Concurrent;

internal class Program
{
    private static void Main(string[] args)
    {
       Dictionary<String, Recipe> dictionaryRecipe= new Dictionary<String, Recipe>();
        appmenu menu = new appmenu(dictionaryRecipe);
        menu.appMenu();

    }
}
 class RecipeLogger
{
    private Dictionary<String, Recipe> dictionaryRecipe;
    public RecipeLogger(Dictionary<string, Recipe> dictionaryRecipe)
    {
        this.dictionaryRecipe = dictionaryRecipe;
    }

    public void LogDetails()
    {
        Console.WriteLine("Enter the number of recipes:");
        int NumOfRecipes;
        if(int.TryParse(Console.ReadLine(),out NumOfRecipes))
        {
            for (int i = 0; i < NumOfRecipes; i++)
            {
                Console.WriteLine("Enter recipe Name:");
                string NameofRecipe = Console.ReadLine();
                Recipe recipe = new Recipe();
                recipe.EnterData();
                dictionaryRecipe.Add(NameofRecipe, recipe);
            }
            String ans;
            do
            {
                Console.WriteLine("Display Ingredients and steps?(Y/N");
                ans = Console.ReadLine();
                switch (ans)
                {
                    case "Y":
                        foreach (var recipeEntry in dictionaryRecipe)
                        {
                            Console.WriteLine($"recipe Name:{recipeEntry.Key}");
                            recipeEntry.Value.RecipeDisplay();
                        }
                        break;
                    case "N":
                        appmenu menu = new appmenu(dictionaryRecipe);
                        menu.appMenu();
                        break;
                    default:
                        Console.WriteLine("Please a valid input");
                        break;

                }

            } while (ans != "N");
        }
        else
        {
            Console.WriteLine("Please enter a number");
        }
     
    }
    public void recipeList()
    {
        foreach(var recipeEntry in dictionaryRecipe)
        {
            Console.WriteLine($"Recipe Name:{ recipeEntry.Key}");

        }
    }
    public void recipeSearcher()
    {
        Console.WriteLine("Please enter the name of the recipe:");
        string recipeName = Console.ReadLine();
        if (dictionaryRecipe.ContainsKey(recipeName))
        {
            Console.WriteLine($"Recipe Name:{recipeName}");
            dictionaryRecipe[recipeName].RecipeDisplay();
        }
        else
        { 
    Console.WriteLine("No recipe found");


        }
    }
}
class ingredientMenu
{

    private Dictionary<String, Recipe> dictionaryRecipe;
    public ingredientMenu(Dictionary<string, Recipe> dictionaryRecipe)
    {
        this.dictionaryRecipe = dictionaryRecipe;
        Recipe recipe = new Recipe();
        while (true)
        {
            Console.WriteLine("================================================");
            Console.WriteLine("Enter 1 to enter recipe details");
            Console.WriteLine("Enter 2 to display recipe");
            Console.WriteLine("Enter 3 to scale recipe");
            Console.WriteLine("Enter 4 to reset quantities");
            Console.WriteLine("Enter 5  to clear recipe");
            Console.WriteLine("Enter 6 to go back to the main menu");
            Console.WriteLine("=================================================");
            string ans = Console.ReadLine();
            switch (ans)
            {
                case "1":
                    recipe.EnterData();
                    break;
                case "2":
                    recipe.RecipeDisplay();
                    break;
                case "3":
                    Console.WriteLine("Enter a scale of 0.5, 2, or 3");
                    double scale1 = Convert.ToDouble(Console.ReadLine());
                    recipe.RecipeScale(scale1);
                    break;
                case "4":
                    recipe.ResetRecipe();
                    break;
                case "5":
                    recipe.ClearRecipe();
                    break;
                case "6":
                    appmenu appmenu = new appmenu(dictionaryRecipe);
                    appmenu.appMenu();
                    break;
                default:
                    Console.WriteLine("Wrong value.Please try again");
                    break;

            }
        }

    }
}

class Recipe
{

    private string[] ingredients;
    private double[] amount;
    private string[] units;
    private string[] steps;
    private double[] calories;
    private string[] foodGroup;

    public Recipe()
    {
        ingredients = new string[0];
        amount = new double[0];
        units = new string[0];
        steps = new string[0];
        calories = new double[0];
        foodGroup = new string[0];


    }
    public void EnterData()
    {
        Console.WriteLine("Enter number of ingredients:");
        int numIngredients = Convert.ToInt32(Console.ReadLine());

        ingredients = new string[numIngredients];
        amount = new double[numIngredients];
        units = new string[numIngredients];
        calories = new double[numIngredients];
        foodGroup= new string[numIngredients];


        for (int i = 0; i < numIngredients; i++)
        {
            Console.WriteLine($"Enter ingredient details#{i + 1}:");
            Console.Write("Name: ");
            ingredients[i] = Console.ReadLine();
            do 
            { 
                Console.Write("Quantity:");
            } while(! double.TryParse(Console.ReadLine(),out amount[i]));
            Console.Write("Unit of measurement:");
            units[i] = Console.ReadLine();
            do 
            {
                Console.Write("Number of calories:");
            } while (!double.TryParse(Console.ReadLine(), out calories[i]));

            Console.Write("Enter Food Group:");
            foodGroup[i] = Console.ReadLine();
        }
        //Delegation
        double caloriesExceed = calculateTotal(calories);
        Console.WriteLine("calculateTotal CALORIES: "+ caloriesExceed);
        if (caloriesExceed > 300) 
        {
            Console.WriteLine("!!TOTAL CALORIES EXCEED 300!!");
        }
        int numSteps;
        do
        {
            Console.WriteLine("Enter the Number of steps:");
        } while (!int.TryParse(Console.ReadLine(), out numSteps));
        
        steps = new string[numSteps];

        for (int a = 0; a < numSteps; a++)
        {
            Console.Write($"Steps#{a + 1}:");
            steps[a] = Console.ReadLine();

        }


    }
    public double calculateTotal(double[]calories)
    {
        double result = 0;
        for(int i=0; i<calories.Length; i++) 
        {
          result+= calories[i];
        }
        return result;
    }
    public void RecipeDisplay()
    {
        Console.WriteLine("Ingredients:");
        for (int i = 0; i < ingredients.Length; i++)
        {
            Console.WriteLine($"- {amount[i]}{units[i]} of {ingredients[i]}");
        }
        Console.WriteLine("Steps:");
        for (int a = 0; a < steps.Length; a++)
        {
            Console.WriteLine($"-{steps[a]}");
        }
        double result = 0;
        for (int i = 0; i < calories.Length; i++)
        { 
            result+= calories[i];
        }
        if (result > 300)
        {
            Console.WriteLine("!! TOTAL CALORIES EXCEED 300!!");
        }

    }

    public void RecipeScale(double Scale)
    {
        for (int i = 0; i < amount.Length; i++)
        {
            amount[i] *= Scale;
        }

    }
    public void ResetRecipe()
    {
        for (int i = 0; i < amount.Length; i++)
        {
            amount[i] /= 2;
        }
    }
    public void ClearRecipe()
    {

        ingredients = new string[0];
        amount = new double[0];
        units = new string[0];
        steps = new string[0];

    }
}
class appmenu
{
    private Dictionary<string, Recipe> dictionaryRecipe;
    private RecipeLogger repL;
    public appmenu(Dictionary<string, Recipe> dictionaryRecipe)
    {
        this.dictionaryRecipe = dictionaryRecipe;
        repL = new RecipeLogger(dictionaryRecipe);
    }
    public void appMenu()
    {
         while(true)
        {
            Console.WriteLine("=====================================");
            Console.WriteLine("RECIPE APPLICATION");
            Console.WriteLine("======================================");
            Console.WriteLine("1) Create Recipe");
            Console.WriteLine("2) Search for a recipe");
            Console.WriteLine("3) Display all the recipes");
            Console.WriteLine("4)Exit the App");
            Console.WriteLine("======================================");
            Console.WriteLine("Please select an option");
            string ans = Console.ReadLine();
            switch (ans)
            {
                case "1":
                    repL.LogDetails();
                    break;
                case "2":
                    repL.recipeSearcher();
                    break;
                    case "3":
                    repL.recipeList();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please enter  a valid input");
                    break;


            }
        }
    }

}