using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ShoppingList
{
    class Program
    {

        //Class for shopping list
        public class ShoppingList
        {
            public string Name { get; set; }
            public List<string> Items { get; set; }
        }

        //Variable for filename
        private const string filename = @"data.json";

        static void Main(string[] args)
        {
            try
            {
                //New list of the shopping list
                var shoppingLists = new List<ShoppingList>();

                //Check if file to store data exists
                if (File.Exists(filename))
                {
                    // Read json data
                    string jsonString = File.ReadAllText(filename);
                    shoppingLists = JsonSerializer.Deserialize<List<ShoppingList>>(jsonString);

                }
                else
                {
                    // Serialize all the objects and save to file
                    Save(shoppingLists);
                }

                //Call the menu method and pass through the shopping lists
                Menu(shoppingLists);

            }
            //Catch all exceptions
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }





        //Menu method
        static void Menu(List<ShoppingList> shoppingLists)
        {
            //Menu
            Console.WriteLine("**************************");
            Console.WriteLine("     Shopping List!");
            Console.WriteLine("**************************\n\n");

            Console.WriteLine(" 1. Add new shopping list");
            Console.WriteLine(" 2. View shopping list");
            Console.WriteLine(" 3. Edit shopping list");
            Console.WriteLine(" 4. Delete shopping list");
            Console.WriteLine(" X. Exit\n");


            //Read users menu choice
            string menuInput = Console.ReadLine();
            
            //Check if valid menu choice
            while (!IsValidInput(menuInput, 5))
            {
                menuInput = Console.ReadLine();
            }

            //Check if X
            if (menuInput.ToUpper() == "X") {
                //Exit program
                Environment.Exit(0);
            }

            var menu = int.Parse(menuInput);

            //Switch with menu options
            switch (menu)
            {
                case 1:

                    //Clear console
                    Console.Clear();

                    Console.WriteLine(" Go back to menu with 'x'");
                    Console.WriteLine(" ------------------------\n\n");

                    //Ask for name
                    Console.WriteLine("\n Name of new shopping list?");
                    string name = Console.ReadLine();


                    //Check if name is empty
                    while (string.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("\n You have to enter a name. Try again!");
                        name = Console.ReadLine();
                    }

                    if (name.ToUpper() == "X")
                    {
                        //Clear console
                        Console.Clear();

                        Menu(shoppingLists);
                    }

                    //Clear console
                    Console.Clear();

                    Console.WriteLine(" Save and go back to menu with 'x'");
                    Console.WriteLine(" ---------------------------------\n\n");

                    Console.WriteLine(" Shopping list: " + name);
                    Console.WriteLine(" ************************\n");

                    var items = new List<string>();

                    //Add item
                    Console.WriteLine("\n Add item:");
                    string item = Console.ReadLine();

                    //Check if message is empty
                    while (string.IsNullOrEmpty(item))
                    {
                        Console.WriteLine("\n You have to add an item or go to menu with 'x'. Try again!");
                        item = Console.ReadLine();
                    }

                    while (item.ToUpper() != "X" && !string.IsNullOrEmpty(item))
                    {
                        items.Add(item);

                        Console.WriteLine("\n Add item:");
                        item = Console.ReadLine();
                    }

                    //New instance of the shopping list
                    ShoppingList shoppingList = new ShoppingList();
                    shoppingList.Name = name;
                    shoppingList.Items = items;

                    //Add to shopping list
                    shoppingLists.Add(shoppingList);

                    //Save the shopping list
                    Save(shoppingLists);

                    //Clear console
                    Console.Clear();

                    //Call menu and pass through the shopping list
                    Menu(shoppingLists);

                    break;


                case 2:

                    //Clear console
                    Console.Clear();

                    Console.WriteLine(" Go back to menu with 'x'");
                    Console.WriteLine(" ------------------------\n\n");

                    Console.WriteLine(" Shopping lists:");
                    Console.WriteLine(" ************************\n");

                    int id = 0;
                    //Loop through the shopping lists
                    foreach (var l in shoppingLists)
                    {
                        Console.WriteLine(" " + id + ". " + l.Name);
                        id++;
                    }

                    //Ask for index number
                    Console.WriteLine("\n\n Which shopping list do you want to view?");

                    //Read users choice
                    string view = Console.ReadLine();

                    //Check if valid input
                    while (!IsValidInput(view, shoppingLists.Count))
                    {                       
                        view = Console.ReadLine();
                    }

                 
                    //If x is chosen go to menu
                    if (view.ToUpper() == "X")
                    {
                        Console.Clear();
                        Menu(shoppingLists);
                    }

                    //Clear console
                    Console.Clear();

                    Console.WriteLine(" Go back to menu with 'x'");
                    Console.WriteLine(" ------------------------\n\n");

                    var index = int.Parse(view);
                    var viewList = shoppingLists[index];

                    Console.WriteLine("\n Shopping list: " + viewList.Name);
                    Console.WriteLine(" ************************\n\n");

                    foreach (var i in viewList.Items) {
                        Console.WriteLine(" " + i);
                    }

                    var back = Console.ReadLine();

                    //If x is chosen go to menu
                    while (back.ToUpper() != "X")
                    {
                        back = Console.ReadLine();
                    }

                    Console.Clear();
                    Menu(shoppingLists);

                    break;

                case 3:

                    //Clear console
                    Console.Clear();

                    Console.WriteLine(" Go back to menu with 'x'");
                    Console.WriteLine(" ------------------------\n\n");

                    Console.WriteLine(" Shopping lists:");
                    Console.WriteLine(" ************************\n");

                    int editId = 0;
                    //Loop through the shopping lists
                    foreach (var l in shoppingLists)
                    {
                        Console.WriteLine(" " + editId + ". " + l.Name);
                        editId++;
                    }

                    //Ask for index number
                    Console.WriteLine("\n\n Which shopping list do you want to edit?");

                    //Read users choice
                    string edit = Console.ReadLine();

                    //Check if valid input
                    while (!IsValidInput(edit, shoppingLists.Count))
                    {
                        edit = Console.ReadLine();
                    }

                    //If x is chosen go to menu
                    if (edit.ToUpper() == "X")
                    {
                        Console.Clear();
                        Menu(shoppingLists);
                    }

                    //Clear console
                    Console.Clear();

                    Console.WriteLine(" Go back to menu with 'x'");
                    Console.WriteLine(" ------------------------\n\n");

                    var indexE = int.Parse(edit);
                    var editList = shoppingLists[indexE];

                    Console.WriteLine("\n Shopping list: " + editList.Name);
                    Console.WriteLine(" ************************\n");

                    Console.WriteLine(" 0. Edit shopping list name");
                    Console.WriteLine(" 1. Edit shopping items");

                    string option = Console.ReadLine();

                    //Check if valid input
                    while (!IsValidInput(option, 2))
                    {
                        option = Console.ReadLine();
                    }

                    //If x is chosen go to menu
                    if (option.ToUpper() == "X")
                    {
                        Console.Clear();
                        Menu(shoppingLists);
                    }

                    //Clear console
                    Console.Clear();

                    int indexO = int.Parse(option);
                    if (indexO == 0)
                    {
                        Console.WriteLine(" Go back to menu with 'x'");
                        Console.WriteLine(" ------------------------\n\n");

                        Console.WriteLine(" New shopping list name:\n");
                        var newName = Console.ReadLine();

                        //Check if empty
                        while (string.IsNullOrEmpty(newName))
                        {
                            Console.WriteLine("\n You have to enter a new name. Try again!");
                            newName = Console.ReadLine();
                        }

                        if (newName.ToUpper() != "X")
                        {
                            shoppingLists[indexE].Name = newName;

                            //Save shopping list
                            Save(shoppingLists);
                        }

                        //Clear console
                        Console.Clear();

                        //Call menu and pass through shopping list
                        Menu(shoppingLists);
                    }

                    if (indexO == 1)
                    {
                        Console.WriteLine(" Go back to menu with 'x'");
                        Console.WriteLine(" ------------------------\n\n");

                        Console.WriteLine("\n Shopping list: " + editList.Name);
                        Console.WriteLine(" ************************\n");

                        int itemId = 0;
                        //Loop through the shopping lists
                        foreach (var i in editList.Items)
                        {
                            Console.WriteLine(" " + itemId + ". " + i);
                            itemId++;
                        }

                        Console.WriteLine("\n\n Choose item to edit:\n");
                        var editItem = Console.ReadLine();

                        //Check if valid input
                        while (!IsValidInput(editItem, editList.Items.Count))
                        {
                            editItem = Console.ReadLine();
                        }

                        //If x is chosen go to menu
                        if (editItem.ToUpper() == "X")
                        {
                            Console.Clear();
                            Menu(shoppingLists);
                        }

                        //Clear console
                        Console.Clear();

                        var indexItem = int.Parse(editItem);

                        Console.WriteLine(" Go back to menu with 'x'");
                        Console.WriteLine(" ------------------------\n\n");

                        Console.WriteLine(" New name of item:\n");
                        var newItem = Console.ReadLine();

                        //Check if empty
                        while (string.IsNullOrEmpty(newItem))
                        {
                            Console.WriteLine("\n You have to enter a new item name. Try again!");
                            newItem = Console.ReadLine();
                        }

                        if (newItem.ToUpper() != "X")
                        {
                            shoppingLists[indexE].Items[indexItem] = newItem;

                            //Save shopping list
                            Save(shoppingLists);
                        }


                        //Clear console
                        Console.Clear();

                        //Call menu and pass through shopping list
                        Menu(shoppingLists);
                    }



                    break;

                case 4:

                    //Clear console
                    Console.Clear();

                    Console.WriteLine(" Go back to menu with 'x'");
                    Console.WriteLine(" ------------------------\n\n");

                    int deleteId = 0;
                    //Loop through the shopping list
                    foreach (var l in shoppingLists)
                    {
                        Console.WriteLine(" " + deleteId + ". " + l.Name);
                        deleteId++;
                    }

                    //Ask for index number
                    Console.WriteLine("\n\n Which shopping list do you want to delete?");

                    //Read users choice
                    string delete = Console.ReadLine();

                    //Check if valid input
                    while (!IsValidInput(delete, shoppingLists.Count))
                    {
                      delete = Console.ReadLine();
                    }

                    //If x is chosen go to menu
                    if (delete.ToUpper() == "X")
                    {
                        Console.Clear();
                        Menu(shoppingLists);
                    }

                    var indexD = int.Parse(delete);

                    //Remove shopping list
                    shoppingLists.RemoveAt(indexD);

                    //Save shopping list
                    Save(shoppingLists);

                    //Clear console
                    Console.Clear();

                    //Call menu and pass through shopping list
                    Menu(shoppingLists);

                    break;

                default:
                    break;
            }

        }


        //Method to save to file
        private static void Save(List<ShoppingList> shoppingLists)
        {
            var jsonString = JsonSerializer.Serialize(shoppingLists);

            File.WriteAllText(filename, jsonString);
        }



        //Function to check valid input
        private static bool IsValidInput(string input, int maxValue)
        {          
            //Check if empty
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine(" You have to chose a valid index number. Try again!");
                return false;
            }

            //Check if "X" is chosen
            if (input.ToUpper() == "X")
            {
                return true;
            }

            //Try parse input to int
            try
            {
                var parsedValue = int.Parse(input);

                //Check if valid index choice
                if (parsedValue > maxValue -1)
                {
                    Console.WriteLine(" You have to chose a valid index number. Try again!");
                    return false;

                }
                return true;

            }
            //Catch exception and return false
            catch (Exception)
            {
                Console.WriteLine(" You have to chose a valid index number. Try again!");
                return false;
              
            }          

        }

    }
}
