using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DAL.Repository;
using System.Configuration;

namespace BL
{
    public class Tracker
    {
        public void OnStart()
        {
            var filePath = ConfigurationManager.AppSettings["FolderPath"];
            var fileExtension = ConfigurationManager.AppSettings["FileExtension"];
            var watcher = new Watcher(filePath, fileExtension);

            watcher.CreatedFile += (sender, info) =>
            {
                CreateTask(info);
            };


            watcher.Run();
            while (Console.Read() != 'q')
            {

            }
        }

        public void CreateTask(FileInfo fileInfo)
        {
            Task task = new Task(() =>
            {
                AddInformationToTheDb(fileInfo.FilePath);
                Task.WaitAll();
                Print();
            });
            task.Start();
        }

        private static readonly object Locker = new object();

        public void AddInformationToTheDb(string fileName)
        {
            AddLog("start add information");
            try
            {
                var records = new Reader(fileName).Read();

                var parser = new Parser();
                var fileInformation = parser.ParseFileName(fileName);

                using (var fileInformationRepository = new FileRepository())
                {
                    fileInformationRepository.Add(fileInformation);
                    fileInformation = fileInformationRepository.FileObjectByName(fileInformation.Name);

                    using (var saleInfoRepository = new SaleInfoRepository())
                    {
                        foreach (var record in records)
                        {
                            var saleInfo = parser.ParseRecord(record);
                            saleInfo.FileInformation = fileInformation;
                            lock (Locker)
                            {
                                saleInfoRepository.Add(saleInfo);
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("exception " + ex.Message);
                //throw;
            }
            AddLog("add infromation OK ");

        }

        public void Print()
        {
            Console.WriteLine("FILES: ");
            int i = 1;
            foreach (var item in new FileRepository())
                Console.WriteLine(i++ + ". " + item);

            Console.WriteLine("MANAGERS:");
            i = 1;
            foreach (var item in new ManagerRepository())
                Console.WriteLine(i++ + ". " + item);

            Console.WriteLine("CLIENTS:");
            i = 1;
            foreach (var item in new ClientRepository())
                Console.WriteLine(i++ + ". " + item);

            Console.WriteLine("PRODUCTS:");
            i = 1;
            foreach (var item in new ProductRepository())
                Console.WriteLine(i++ + ". " + item);

            Console.WriteLine("SALEINFOS:");
            i = 1;
            foreach (var item in new SaleInfoRepository())
                Console.WriteLine(i++ + ". " + item);
        }

        public static void AddLog(string log)
        {
            try
            {
                if (!EventLog.SourceExists("MyService"))
                {
                    EventLog.CreateEventSource("MyService", "MyService");
                }
                EventLog ev = new EventLog { Source = "MyService" };
                ev.WriteEntry(log);
            }
            catch
            {
                // ignored
            }
        }
    }
}
