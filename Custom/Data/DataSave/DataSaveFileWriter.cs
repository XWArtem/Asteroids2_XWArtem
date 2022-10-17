using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


class DataSaveFileWriter
{
    private string _folder;
    private string _filePath;
    private Thread _workingThread;

    public DataSaveFileWriter(string folder)
    {
        _folder = folder;
        ManagePath();
        //_workingThread = new Thread(StoreData)
        //{

        //}
    }

    private void ManagePath()
    {
        _filePath = $"{_folder}/GameData.log";
    }

    private void StoreData()
    {

    }
}

