using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureRenameConsoleApp
{
    internal class MessageStrings
    {
        public string INTRODUCTION_TEXT = "\n\nWelcome to the Picture Date - Rename Tool \n\nThis tool is designed to do batch action re-naming of JPG image files in a folder. \n\nIt will rename the files in the following format: YYYY-MM-DD-HH:MM_Device\n\n";
        public string BEGIN_TEXT = "To begin, please enter the path of the folder you'd like to start:";
        public string PHOTO_FOUND_STRING = "Photo found at: ";
        public string PHOTO_RENAME_STRING = "Attempting to renaming photo to: ";
        public string SUCCESSFULLY_CHANGED = "Successfully renamed ";
        public string IMAGES = " images";
        public string FAILED_CHANGED = "Failed to rename ";
        public string RESULTS_LOCATION = "Results for this batch process can be found here: ";
        public string THANK_YOU = "Thank you for using the Picture Date - Rename Tool!";
        public string FAILED_TO_CHANGE_LABEL = "FAILED TO CONVERT THESE JPGS:";
        public string SUCCESSFUL_CHANGE_LABEL = "SUCCESSFULLY CONVERTED THESE JPGS TO DATE AND TIME CREATED:";
        public string BEGIN_PROCESSING = "BEGIN PROCESSING JPG FILES";
        public string PATH_DOESNT_EXIST = "That path doesn't exist or is invalid.";


        public string ERROR_NULL_PATH = "You must enter a path";
        public string ERROR_PATH_TOO_SHORT = "Path must be at least 3 characters";
        public string ERROR_TOO_MANY_FAILED_ATTEMPTS = "Fine! I didn't want to help you anyway!";
        public string ERROR_FIND_DATE_MISSING_CORRUPT = "Date could not be found in metadata or is corrupt for ";
        public string ERROR_NO_JPG_FOUND = "No JPGs could be found at that location.";
    }
}
