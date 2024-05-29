using EFC_WMP_Asset_Tracking;

Console.Title = "Asset Tracker v2.0EF";
Console.SetWindowSize(140, 30);
Console.ForegroundColor = ConsoleColor.White;

MyDbContext context = new MyDbContext();
ShowMainMenu(context);

static void ShowMainMenu(MyDbContext context)
{
    Console.Clear();
    Console.WriteLine();
    Console.WriteLine("  █████  ███████ ███████ ███████ ████████ ████████ ██████   █████   ██████ ██   ██ ███████ ██████ v.2.0");
    Console.WriteLine(" ██   ██ ██      ██      ██         ██       ██    ██   ██ ██   ██ ██      ██  ██  ██      ██   ██");
    Console.WriteLine(" ███████ ███████ ███████ █████      ██       ██    ██████  ███████ ██      █████   █████   ██████ ");
    Console.WriteLine(" ██   ██      ██      ██ ██         ██       ██    ██   ██ ██   ██ ██      ██  ██  ██      ██   ██");
    Console.WriteLine(" ██   ██ ███████ ███████ ███████    ██       ██    ██   ██ ██   ██  ██████ ██   ██ ███████ ██   ██");
    Console.WriteLine();
    Console.WriteLine();
    CreateMenuInfoText("Pick an option:");
    CreateMenuOptionsText("1", "Add an Asset");
    CreateMenuOptionsText("2", "List Assets");
    CreateMenuOptionsText("3", "Update Asset");
    CreateMenuOptionsText("4", "Delete an Asset");
    CreateMenuOptionsText("0", "Quit");
    Console.Write("\nEnter a Number: ");
    string userInput = Console.ReadLine();
    switch (userInput)
    {
        case "1":
            AddAssetMenu(context);
            break;

        case "2":
            Console.Clear();
            ShowAssets(context, false);
            UserFeedback("\nPress any key to Continue", false);
            Console.ReadLine();
            ShowMainMenu(context);
            break;

        case "3":
            Console.Clear();
            UpdateAssetMenu(context);
            break;

        case "4":
            Console.Clear();
            DeleteAssetMenu(context);
            break;

        case "0":
            Console.Clear();
            CreateMenuOptionsText("\nThank you for using this application.", "", true);
            break;

        default:
            ShowMainMenu(context);
            break;
    }
}
static void ShowAssets(MyDbContext context, bool byTypeID)
{
    var computerAssets = context.Computers.ToList();
    var phoneAssets = context.Phones.ToList();

    var allAssets = computerAssets
        .Cast<Asset>()
        .Concat(phoneAssets.Cast<Asset>())
        .OrderBy(a => a.Office)
        .ThenBy(a => a.PurchaseDate)
        .ToList();

    if (byTypeID)
    {
        allAssets = computerAssets
       .Cast<Asset>()
       .Concat(phoneAssets.Cast<Asset>())
       .OrderBy(a => a.Type)
       .ThenBy(a => a.Id)
       .ToList();
    }
    int padding = 15;
    DateOnly dateToday = DateOnly.FromDateTime(DateTime.Today);
    decimal localPriceToday = 0;
    decimal usdToSek = 10.68m;
    decimal usdToEur = 0.93m;

    var titles = new List<String>() { "Id", "Type", "Brand", "Model", "Office", "Purchase Date", "Price USD", "Currency", "Local Price" };
    var underLine = new List<String>() { "--", "----", "----", "----", "------", "-------------", "---------", "--------", "-----------" };

    foreach (var title in titles)
    {
        if (title == "Id")
        {
            Console.Write(title.PadRight(padding - 10));
        }
        else
        {
            Console.Write(title.PadRight(padding));
        }
    }
    Console.WriteLine();

    foreach (var line in underLine)
    {
        if (line == "--")
        {
            Console.Write(line.PadRight(padding - 10));
        }
        else
        {
            Console.Write(line.PadRight(padding));
        }
    }
    Console.WriteLine();

    foreach (var asset in allAssets)
    {
        var ThreeMonthsBeforeExpired = asset.PurchaseDate.AddYears(2).AddMonths(9);
        var SixMonthsBeforeExpired = asset.PurchaseDate.AddYears(2).AddMonths(6);

        switch (asset.Currency)
        {
            case "SEK":
                localPriceToday = asset.PriceUSD * usdToSek;
                break;

            case "EUR":
                localPriceToday = asset.PriceUSD * usdToEur;
                break;

            case "USD":
                localPriceToday = asset.PriceUSD;
                break;
        }

        if (dateToday > SixMonthsBeforeExpired && dateToday < ThreeMonthsBeforeExpired)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else if (ThreeMonthsBeforeExpired < dateToday)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        Console.WriteLine($" {asset.Id}".PadRight(padding - 10) + $"{asset.Type}".PadRight(padding) + $"{asset.Brand}".PadRight(padding) + $"{asset.Model}".PadRight(padding) + $"{asset.Office}".PadRight(padding) + $"{asset.PurchaseDate}".PadRight(padding) + $"{asset.PriceUSD}".PadRight(padding) + $"{asset.Currency}".PadRight(padding) + $"{localPriceToday}".PadRight(padding));
    }
}
static void AddAssets(MyDbContext context, bool isComputer)
{
    string brand, model, office, currency;
    DateOnly purchaseDate = default;
    decimal priceUSD = 0;

    brand = ReturnUserInput("Enter a Brand: ");
    model = ReturnUserInput("Enter a Model: ");
    office = ReturnUserInput("Enter a Office: ");
    purchaseDate = ReturnUserDateInput("Enter a Date(yyyy - MM - dd): ", purchaseDate);
    priceUSD = ReturnUserPriceInput("Enter a Price in USD: ", priceUSD);
    currency = ReturnUserInput("Enter a Currency (SEK,USD,EUR): ").ToUpper();

    Asset newAsset = isComputer ? new Computer() : new Phone();
    newAsset.Type = isComputer ? "Computer" : "Phone";
    newAsset.Brand = brand;
    newAsset.Model = model;
    newAsset.Office = office;
    newAsset.PurchaseDate = purchaseDate;
    newAsset.PriceUSD = priceUSD;
    newAsset.Currency = currency;

    context.Add(newAsset);
    context.SaveChanges();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Added [" + brand + " " + model + " " + office + " " + purchaseDate + " " + priceUSD + " " + currency + "]");
    Console.ForegroundColor = ConsoleColor.White;
    UserFeedback("\nPress any key to Continue", false);
    Console.ReadLine();
    ShowMainMenu(context);
}
static void AddAssetMenu(MyDbContext context)
{
    Console.Clear();
    Console.WriteLine();
    CreateMenuInfoText("Pick an option to add:");
    CreateMenuOptionsText("1", "Computer");
    CreateMenuOptionsText("2", "Phone");
    CreateMenuOptionsText("0", "Back");
    Console.Write("\n Enter a Number: ");
    String userInput = Console.ReadLine();

    switch (userInput)
    {
        case "1":
            AddAssets(context, true);
            break;

        case "2":
            AddAssets(context, false);
            break;

        case "0":
            break;

        default:
            UserFeedback("Invalid Selection");
            AddAssetMenu(context);
            break;
    }
}
static void UpdateAsset(MyDbContext context, bool isComputer)
{
    Console.Clear();
    ShowAssets(context, true);
    DateOnly purchaseDate = default;
    decimal priceUSD = 0;

    if (isComputer)
    {
        CreateMenuInfoText("Update Computer");
        Console.Write(" Enter Id of the item you want to edit: ");
        string updAssetID = Console.ReadLine();
        int userInputToInt = Convert.ToInt32(updAssetID);

        Computer computerAsset = context.Computers.FirstOrDefault(x => x.Id == userInputToInt);

        if (computerAsset != null)
        {
            Console.Write(" What property do you want to update (Brand, Model...): ");
            string editProp = Console.ReadLine();

            switch (editProp.ToLower().Trim())
            {
                case "brand":
                    string newBrandVal = ReturnUserInput(" Enter new brand value: ");
                    computerAsset.Brand = newBrandVal;
                    context.Update(computerAsset);
                    context.SaveChanges();
                    break;

                case "model":

                    string newModelVal = ReturnUserInput(" Enter new model value: ");
                    computerAsset.Model = newModelVal;
                    context.Update(computerAsset);
                    context.SaveChanges();
                    break;

                case "office":

                    string newOfficeVal = ReturnUserInput(" Enter new office value: ");
                    computerAsset.Office = newOfficeVal;
                    context.Update(computerAsset);
                    context.SaveChanges();
                    break;

                case "purchasedate":
                    DateOnly newDateVal = ReturnUserDateInput(" Enter a new Date(yyyy - MM - dd): ", purchaseDate);
                    computerAsset.PurchaseDate = newDateVal;
                    context.Update(computerAsset);
                    context.SaveChanges();
                    break;

                case "price usd":
                    decimal newPriceVal = ReturnUserPriceInput(" Enter a new Price in USD: ", priceUSD);
                    computerAsset.PriceUSD = newPriceVal;
                    context.Update(computerAsset);
                    context.SaveChanges();
                    break;

                case "currency":

                    string newCurrencyVal = ReturnUserInput(" Enter a Currency (SEK,USD,EUR): ");
                    computerAsset.Currency = newCurrencyVal.ToUpper();
                    context.Update(computerAsset);
                    context.SaveChanges();
                    break;
            }
        }
    }
    else
    {
        CreateMenuInfoText("Update Phone");
        Console.Write(" Enter Id of the item you want to edit: ");
        string updAssetID = Console.ReadLine();
        int userInputToInt = Convert.ToInt32(updAssetID);

        Phone phoneAsset = context.Phones.FirstOrDefault(x => x.Id == userInputToInt);

        if (phoneAsset != null)
        {
            Console.Write(" What property do you want to update (Brand, Model...): ");
            string editProp = Console.ReadLine();

            switch (editProp.ToLower().Trim())
            {
                case "brand":
                    string newBrandVal = ReturnUserInput(" Enter new brand value: ");
                    phoneAsset.Brand = newBrandVal;
                    context.Update(phoneAsset);
                    context.SaveChanges();
                    break;

                case "model":

                    string newModelVal = ReturnUserInput(" Enter new model value: ");
                    phoneAsset.Model = newModelVal;
                    context.Update(phoneAsset);
                    context.SaveChanges();
                    break;

                case "office":

                    string newOfficeVal = ReturnUserInput(" Enter new office value: ");
                    phoneAsset.Office = newOfficeVal;
                    context.Update(phoneAsset);
                    context.SaveChanges();
                    break;

                case "purchasedate":
                    DateOnly newDateVal = ReturnUserDateInput(" Enter a new Date(yyyy - MM - dd): ", purchaseDate);
                    phoneAsset.PurchaseDate = newDateVal;
                    context.Update(phoneAsset);
                    context.SaveChanges();
                    break;

                case "price usd":
                    decimal newPriceVal = ReturnUserPriceInput(" Enter a new Price in USD: ", priceUSD);
                    phoneAsset.PriceUSD = newPriceVal;
                    context.Update(phoneAsset);
                    context.SaveChanges();
                    break;

                case "currency":

                    string newCurrencyVal = ReturnUserInput(" Enter a Currency (SEK,USD,EUR): ");
                    phoneAsset.Currency = newCurrencyVal.ToUpper();
                    context.Update(phoneAsset);
                    context.SaveChanges();
                    break;
            }
        }
    }

    Console.WriteLine("The changes has been saved!");
    UserFeedback("\nPress any key to Continue", false);
    Console.ReadLine();
    ShowMainMenu(context);
}
static void UpdateAssetMenu(MyDbContext context)
{
    while (true)
    {
        ShowAssets(context, true);
        Console.WriteLine();
        CreateMenuInfoText("Pick an option:");
        CreateMenuOptionsText("1", "Update Computer");
        CreateMenuOptionsText("2", "Update Phone");
        CreateMenuOptionsText("0", "Back");
        string userInput = Console.ReadLine();
        Console.Clear();
        switch (userInput)
        {
            case "1":
                UpdateAsset(context, true);
                break;

            case "2":
                Console.Clear();
                UpdateAsset(context, false);
                break;

            case "0":
                ShowMainMenu(context);
                break;

            default:
                UpdateAssetMenu(context);
                break;
        }
    }
}
static void DeleteAssetMenu(MyDbContext context)
{
    while (true)
    {
        Console.Clear();
        ShowAssets(context, true);
        Console.WriteLine();
        CreateMenuInfoText("Pick an option:");
        CreateMenuOptionsText("1", "Delete Computer");
        CreateMenuOptionsText("2", "Delete Phone");
        CreateMenuOptionsText("0", "Back");
        Console.Write("\nEnter a Number: ");
        string userInput = Console.ReadLine();
        Console.Clear();
        switch (userInput)
        {
            case "1":
                DeleteAsset(context, true);
                ShowMainMenu(context);
                break;

            case "2":
                Console.Clear();
                DeleteAsset(context, false);
                ShowMainMenu(context);
                break;

            case "0":
                ShowMainMenu(context);
                break;

            default:
                DeleteAssetMenu(context);
                break;
        }
    }
}
static void DeleteAsset(MyDbContext context, bool isComputer)
{
    ShowAssets(context, true);
    Console.ForegroundColor = ConsoleColor.White;
    if (isComputer)
    {
        CreateMenuInfoText("Delete Computer");
        CreateMenuInfoText("Enter Id of the item you want to delete: ");
        string delAssetID = Console.ReadLine();
        int userInputToInt = Convert.ToInt32(delAssetID);

        Computer computerAsset = context.Computers.FirstOrDefault(x => x.Id == userInputToInt);
        Console.WriteLine();
        Console.WriteLine($"Are You sure you want to Delete computer with the id of: " + delAssetID);
        Console.WriteLine();
        CreateMenuInfoText("Pick an option:");
        CreateMenuOptionsText("1", "Yes");
        CreateMenuOptionsText("2", "Cancel");

        Console.Write("Enter your choice: ");
        // Loop to handle deletion confirmation
        while (true)
        {
            string DeleteCheckInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(DeleteCheckInput))
            {
                if (DeleteCheckInput == "1")
                {
                    context.Remove(computerAsset);
                    context.SaveChanges();
                    UserFeedback("Asset deleted successfully!", false);
                    break; // Exit the loop after valid input
                }
                else if (DeleteCheckInput == "2")
                {
                    UserFeedback("Deletion cancelled.", false);
                    break; // Exit the loop after valid input
                }
                else
                {
                    // User input is not within the valid options
                    UserFeedback("Please enter either 1 or 2");
                    // Continue the loop to prompt the user for valid input
                }
            }
        }
    }
    else
    {
        Console.WriteLine("Delete Phone");
        Console.Write("Enter Id of the item you want to delete: ");
        string delAssetID = Console.ReadLine();
        int userInputToInt = Convert.ToInt32(delAssetID);

        Phone phoneAsset = context.Phones.FirstOrDefault(x => x.Id == userInputToInt);
        Console.WriteLine();
        Console.WriteLine($"Are You sure you want to Delete phone with the id of: " + delAssetID + "?");
        Console.WriteLine();
        CreateMenuInfoText("Pick an option:");
        CreateMenuOptionsText("1", "Yes");
        CreateMenuOptionsText("2", "Cancel");

        Console.Write("Enter your choice: ");
        // Loop to handle deletion confirmation
        while (true)
        {
            string DeleteCheckInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(DeleteCheckInput))
            {
                if (DeleteCheckInput == "1")
                {
                    context.Remove(phoneAsset);
                    context.SaveChanges();
                    UserFeedback("Asset deleted successfully!", false);
                    break; // Exit the loop after valid input
                }
                else if (DeleteCheckInput == "2")
                {
                    UserFeedback("Deletion cancelled.", false);
                    Console.ReadLine();
                    DeleteAssetMenu(context);
                    break; // Exit the loop after valid input
                }
                else
                {
                    // User input is not within the valid options
                    UserFeedback("Please enter either 1 or 2");
                    // Continue the loop to prompt the user for valid input
                }
            }
        }
    }
}
static string ReturnUserInput(string message)
{
    while (true)
    {
        Console.Write(message);
        string prop = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(prop))
        {
            UserFeedback("Please enter a value");
        }
        else
        {
            return prop;
        }
    }
}
static DateOnly ReturnUserDateInput(string message, DateOnly purchaseDate)
{
    while (true)
    {
        Console.Write(message);
        string inputDate = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputDate))
        {
            UserFeedback("Please enter a date, ex: 2020-01-01");
        }
        else if (DateOnly.TryParseExact(inputDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out purchaseDate))
        {
            return purchaseDate;
        }
        else
        {
            UserFeedback("Invalid date format. Please enter a valid date in the format yyyy-MM-dd");
        }
    }
}
static decimal ReturnUserPriceInput(string message, decimal priceUSD)
{
    while (true)
    {
        Console.Write(message);
        if (!decimal.TryParse(Console.ReadLine(), out priceUSD))
        {
            UserFeedback("Invalid price format. Please enter a valid decimal number.");
        }
        else
        {
            return priceUSD;
        }
    }
}
static void CreateMenuOptionsText(string menuNumber, string message, bool isQuit = false)
{
    if (isQuit)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{menuNumber}\n");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{message}");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(">> ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("(");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write($"{menuNumber}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($") {message}\n");
    }
}
static void CreateMenuInfoText(string message, bool isExclamation = false)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write(">> ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write(message);
    if (isExclamation)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("!\n");
    }
    else
    {
        Console.WriteLine();
    }
}
static void UserFeedback(string msg, bool errorMsg = true)
{
    if (errorMsg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(msg);
        Console.ForegroundColor = ConsoleColor.White;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(msg);
        Console.ForegroundColor = ConsoleColor.White;
    }
}