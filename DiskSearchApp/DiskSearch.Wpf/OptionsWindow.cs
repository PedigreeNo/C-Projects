using System.Windows;
using System.IO;
using System.Windows.Threading;
using System;
using DiskSearch.Biz;
using DiskSearch.Wpf.Properties;
//Pizza.txt
namespace DiskSearch.Wpf
{
    public partial class MainWindow
    {
        public OptionsWindow optionsWindow { get; set; }

        public DispatcherTimer DispatcherTimer = new DispatcherTimer
        {
            Interval = new TimeSpan(0, 0, 0, 1)
        };


        public MainWindow()
        {          
            InitializeComponent();
            CreateFileTextBox.Text = Settings.Default["iniCreateFileTextBox"].ToString();
            PathTextBox.Text = Settings.Default["IniPathTextBox"].ToString();
            TargetTextBox.Text = Settings.Default["IniTargetTextBox"].ToString();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            WalkAllFolders();
        }

        private void CancelProgramButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void WalkAllFolders()
        {
            //Form Elements Unable during Search Task
            StartButton.IsEnabled = false;
            //Search Task
            var task1 = new Disk(CreateFileTextBox.Text, TargetTextBox.Text)
                .WalkThroughFolders(new DirectoryInfo(PathTextBox.Text));
            //Progressbar
            DispatcherTimer.Start();
            DispatcherTimer.Tick += (sender, e) => DispatcherTimer_Tick(task1);
        }

        private void DispatcherTimer_Tick(IAsyncResult t)
        {
            if (t.IsCompleted)
            {
                ProgressBar1.Value = 100;
                //Sending Email Call
                new Email().Send(AccountTextBox.Text, PasswortTextBox.Password);
                DispatcherTimer.Stop();
            }
            else
            {
               var unused = new Random().Next(1, 3).Equals(1) ? ProgressBar1.Value = 10 : ProgressBar1.Value = 90;              
            }
        }

        public void ShowMessage(string output)
        {
            System.Windows.Forms.MessageBox.Show(output);
        }

        private void CreateFileTextBox_GotFocus(object sender, RoutedEventArgs e)
        {        
            CreateFileTextBox.Text = string.Empty;
            CreateFileTextBox.AppendText(new Explorer().OpenExplorer());       
 
            if (!CreateFileTextBox.Text.Equals(""))
                CreateFileTextBox.AppendText(@"\Abbild.txt");

            else if(CreateFileTextBox.Text.Equals(""))
                CreateFileTextBox.AppendText("Create file path - example" + @"„D:\TEXT\TEXT.txt"+ "\"");          
        }

        private void CreateFileTextBox_Initialized(object sender, EventArgs e)
        {
            CreateFileTextBox.AppendText("Create file path - example" + @"„D:\TEXT\TEXT.txt" + "\"");
        }

        private void CreateFileTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CreateFileTextBox.Text.Equals(""))
                CreateFileTextBox.AppendText("Create file path - example" + @"„D:\TEXT\TEXT.txt" + "\"");
        }      

        private void PathTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PathTextBox.Text = string.Empty;
            PathTextBox.AppendText(new Explorer().OpenExplorer());

            if (PathTextBox.Text.Equals(""))
            PathTextBox.AppendText(@"Begin at search path - example „D:\");
        }

        private void PathTextBox_Initialized(object sender, EventArgs e)
        {
            PathTextBox.AppendText(@"Begin at search path - example „D:\");
        }

        private void PathTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PathTextBox.Text.Equals(""))
                PathTextBox.AppendText(@"Begin at search path - example „D:\");
        }

        private void TargetTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TargetTextBox.Text = string.Empty;
        }

        private void TargetTextBox_Initialized(object sender, EventArgs e)
        {
            TargetTextBox.AppendText("Search file name - example „TEXT.txt" + "\"");
        }

        private void TargetTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TargetTextBox.Text.Equals(""))
                TargetTextBox.AppendText("Search file name - example „TEXT.txt" + "\"");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var optionsWindow = new OptionsWindow
            {
                mainWindow = this
            };

            optionsWindow.Show();
        }

        public void MainWindowFunction()
        {
            Close();
        }

    }
}
