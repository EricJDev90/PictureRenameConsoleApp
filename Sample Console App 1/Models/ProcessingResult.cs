using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureRenameConsoleApp.Models
{
    internal class ProcessingResult
    {
        public int successfullyChanged { get; set; }
        public List<string> successfulPaths { get; set; }
        public int failedToChange { get; set; }
        public List<string> failedPaths { get; set; }

        public ProcessingResult()
        {
            successfullyChanged = 0;
            successfulPaths= new List<string>();
            failedToChange = 0;
            failedPaths= new List<string>();
        }

        public void generateOutputFile()
        {

        }
    }
}
