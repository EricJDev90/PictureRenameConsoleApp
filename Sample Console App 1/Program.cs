using PictureRenameConsoleApp;
using PictureRenameConsoleApp.Models;
using System.Text.RegularExpressions;


MessageStrings feedback = new MessageStrings();
Console.WriteLine(value: feedback.INTRODUCTION_TEXT);
Folder folder = new Folder();
int tries = 0;
Regex regex = new Regex(":|\\0");
ProcessingResult results = new ProcessingResult();

//Get path we need to work with
while (!folder.isValidPathAndExists || tries >= 10)
{
    Console.WriteLine(value: feedback.BEGIN_TEXT);
    var enteredPath = Console.ReadLine();

    if (enteredPath != null && enteredPath.Length > 3)
    {
        folder.CheckIsValidPathAndExists(enteredPath);
    }
    if (enteredPath == null || enteredPath.Length == 0)
    {
        Console.WriteLine(value: feedback.ERROR_NULL_PATH);
    }
    if (enteredPath.Length is > 0 and < 3)
    {
        Console.WriteLine(value: feedback.ERROR_PATH_TOO_SHORT);
    }
    tries++;
    if (tries >= 10)
    {
        Console.WriteLine(value: feedback.ERROR_TOO_MANY_FAILED_ATTEMPTS);
        Console.ReadLine();
        Environment.Exit(0);
    }
}

//Get all the files in the top directory
string[] filesJPG = Directory.GetFiles(folder.pathUri, "*.jpg", SearchOption.TopDirectoryOnly);
string[] filesPNG = Directory.GetFiles(folder.pathUri, "*.png", SearchOption.TopDirectoryOnly);

if (filesJPG.Length > 0) {
     folder.ProcessFiles(filesJPG, ".jpg", regex, results);
}

if (filesPNG.Length > 0)
{
    folder.ProcessFiles(filesPNG, ".png", regex, results); //Commenting this out for now untl I can figure out how pngs work
}

Console.WriteLine(value: feedback.SUCCESSFULLY_CHANGED + results.successfullyChanged + feedback.IMAGES);
Console.WriteLine(value: feedback.FAILED_CHANGED + results.failedToChange + feedback.IMAGES);
Console.WriteLine(value: feedback.RESULTS_LOCATION + folder.pathUri);
Console.WriteLine(value: feedback.THANK_YOU);
Console.ReadLine();


//Still need to create output file and push successful/unsuccessful strings
//Fix PNG editing