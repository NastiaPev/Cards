using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cards
{
    /// <summary>
    /// Outputs the information regarding user interaction event into a text file 
    /// </summary>
    class EventFileWriter
    {
                private readonly string _mFileLocation;
        private EventFileWriter(string mFileLocation)
        {
            this._mFileLocation = mFileLocation;
        }

		// CODEREVIEW: In practice, we'd want some way to specify the location of the files, and perhaps include the current date in the 
		// CODEREVIEW: name of the file. We're not going to be persuing this method of event recording though, so it doesn't really matter.
        public static EventFileWriter TheWriter = new EventFileWriter("@MyReport.txt");
        
        public void WriteIntoFile(EventInformation userEvent)
        {
                
                using (FileStream stream = new FileStream(_mFileLocation, FileMode.Append))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(userEvent.Serialize());
                }
           
        }
      
    }


}

