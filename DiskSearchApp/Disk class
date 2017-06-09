using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace DiskSearch.Biz
{
    public class Disk
    {
        public string CreateTxtFilePath { get; set; }
        public string SearchFile { get; set; }
        public bool Stop { get; set; }
        public CancellationToken Token { get; set; }
        public CancellationTokenSource CancelTokenSource { get; set; }

        public Disk(string createTxtFilePath, string searchFile = "")
        {
            CreateTxtFilePath = createTxtFilePath;
            SearchFile = searchFile;
            CancelTokenSource = new CancellationTokenSource();
            Token = CancelTokenSource.Token;
        }

        public Task WalkThroughFolders(DirectoryInfo root)
        {           
            return Task.Run(async () =>
            {       
                    try
                    {
                        foreach (var folder in root.GetDirectories())
                        {
                                if (WriteTxt(CreateTxtFilePath, SearchFile, folder.FullName))
                                    Console.WriteLine(@"Gefunden Folder: ");

                                await WalkThroughFolders(folder);                        
                        }
                        foreach (var file in root.GetFiles())
                        {
                            if (WriteTxt(CreateTxtFilePath, SearchFile, file.FullName))
                                Console.WriteLine(@"Gefunden File: ");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }                
            },Token);
        }
 
        public bool WriteTxt(string txtPath, string searchTxt, string path)
        {
            if (Token.IsCancellationRequested) return false;
            var fileName = Path.GetFileName(path);
            if (fileName != null && fileName.Equals(searchTxt))
            {
                using (var tw = new StreamWriter(txtPath, true))
                {
                    tw.WriteLine("Gefunden: " + path);
                    CancelTokenSource.Cancel();
                    return true;
                }
            }
            using (var tw = new StreamWriter(txtPath, true))
            {
                tw.WriteLine(path);
                return false;
            }
        }
    }
}
