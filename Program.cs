// See https://aka.ms/new-console-template for more information

using System.Security.Permissions;

// [SecurityPermission(SecurityAction.Demand, Name = "FullTrust")]

string folderToWatch = @"C:\";
string folderToSave = @"D:\tracker\tracked.txt";

using (FileSystemWatcher watcher = new FileSystemWatcher())
{
    watcher.Path = folderToWatch;
    watcher.IncludeSubdirectories = true;
    watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                    | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size;

    watcher.Filter = "*.*";

    watcher.Changed += OnChanged;
    watcher.Created += OnCreated;
    watcher.Deleted += OnDeleted;
    watcher.Renamed += OnRenamed;

    watcher.EnableRaisingEvents = true;
    Console.ReadKey();
}

void WriteLine(string line)
{
    File.AppendAllText(folderToSave, line + Environment.NewLine + Environment.NewLine);
}

void OnRenamed(object sender, RenamedEventArgs e)
{
    string strToWrite =  e.FullPath + Environment.NewLine + e.OldName + " renamed to: " + e.Name +Environment.NewLine+ "Time : " + DateTime.Now.ToString();
    WriteLine(strToWrite);
}

void OnDeleted(object sender, FileSystemEventArgs e)
{
    string strToWrite = e.FullPath + Environment.NewLine +  "Deleted file: " + e.Name + Environment.NewLine + "Time : " + DateTime.Now.ToString();
    WriteLine(strToWrite);
}

void OnCreated(object sender, FileSystemEventArgs e)
{
    string strToWrite = e.FullPath + Environment.NewLine + "Created file: " + e.Name + Environment.NewLine + "Time : " + DateTime.Now.ToString();
    WriteLine(strToWrite);
}

void OnChanged(object sender, FileSystemEventArgs e)
{
    string strToWrite = e.FullPath + Environment.NewLine +  "Changed file: " + e.Name + Environment.NewLine + "Time : " + DateTime.Now.ToString();
    WriteLine(strToWrite);
}