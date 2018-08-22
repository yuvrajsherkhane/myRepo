using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Working_with_File__Using_CSharp_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
        public string fileNam = "NewFile";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (TextBlock blck in List.Children.OfType<TextBlock>())
            {
                blck.FontWeight = FontWeights.Normal;
            }

            // Get the textblock that was clicked and the Name.
            TextBlock block = sender as TextBlock;
            string text = block.Text;
            block.FontWeight = FontWeights.Bold;

            // Run a switch
            switch (text)
            {
                case "Home":
                    // Show the Home Tab
                    CreateFile.Visibility = Visibility.Collapsed;
                    WriteContent.Visibility = Visibility.Collapsed;
                    Home.Visibility = Visibility.Visible;
                    MoveTo.Visibility = Visibility.Collapsed;
                    DeleteFile.Visibility = Visibility.Collapsed;
                    AboutMe.Visibility = Visibility.Collapsed;
                    break;
                case "Create File":
                    // Show the required tab
                    Home.Visibility = Visibility.Collapsed;
                    WriteContent.Visibility = Visibility.Collapsed;
                    MoveTo.Visibility = Visibility.Collapsed;
                    CreateFile.Visibility = Visibility.Visible;
                    DeleteFile.Visibility = Visibility.Collapsed;
                    AboutMe.Visibility = Visibility.Collapsed;

                    TextBlock blck = CreateFile.Children.OfType<TextBlock>().Last();
                    if (CreateNewFile())
                    {
                        blck.Text = "File has been created. Continue to next module.";
                    }
                    else
                    {
                        blck.Text = "File already exists.";
                    }
                    break;
                case "Write Content":
                    // Show the required tab
                    Home.Visibility = Visibility.Collapsed;
                    CreateFile.Visibility = Visibility.Collapsed;
                    ReadContent.Visibility = Visibility.Collapsed;
                    WriteContent.Visibility = Visibility.Visible;
                    MoveTo.Visibility = Visibility.Collapsed;
                    DeleteFile.Visibility = Visibility.Collapsed;
                    AboutMe.Visibility = Visibility.Collapsed;
                    break;
                case "Read Content":
                    // Show the required tab
                    Home.Visibility = Visibility.Collapsed;
                    CreateFile.Visibility = Visibility.Collapsed;
                    WriteContent.Visibility = Visibility.Collapsed;
                    ReadContent.Visibility = Visibility.Visible;
                    MoveTo.Visibility = Visibility.Collapsed;
                    DeleteFile.Visibility = Visibility.Collapsed;
                    AboutMe.Visibility = Visibility.Collapsed;

                    // Read content and save it, 
                    if (File.Exists(directory + fileNam + ".txt"))
                    {
                        // Write the content to the UI
                        string content = File.ReadAllText(directory + fileNam + ".txt");
                        fileContent.Text = content;
                    }
                    else
                    {
                        // File not found, show the error.
                        MessageBox.Show("We're sorry, but the file is not found. It might have been deleted, please start the process from the beginning.");
                    }
                    break;
                case "Move to":
                    // Show the required tab
                    Home.Visibility = Visibility.Collapsed;
                    CreateFile.Visibility = Visibility.Collapsed;
                    WriteContent.Visibility = Visibility.Collapsed;
                    ReadContent.Visibility = Visibility.Collapsed;
                    MoveTo.Visibility = Visibility.Visible;
                    DeleteFile.Visibility = Visibility.Collapsed;
                    AboutMe.Visibility = Visibility.Collapsed;

                    // make sure the file exists, 
                    if (File.Exists(directory + fileNam + ".txt"))
                    {
                        // create new directory, directory variable has the documents folder.
                        Directory.CreateDirectory(directory + "\\NewFolder");

                        string[] nameArray = fileNam.Split('/');
                        fileNam = nameArray[nameArray.Length - 1];

                        // Move the file to the new location
                        File.Move(directory + fileNam + ".txt", directory + "\\NewFolder\\" + fileNam + ".txt");
                    }
                    break;
                case "Delete File":
                    // Show the required tab
                    Home.Visibility = Visibility.Collapsed;
                    CreateFile.Visibility = Visibility.Collapsed;
                    WriteContent.Visibility = Visibility.Collapsed;
                    ReadContent.Visibility = Visibility.Collapsed;
                    MoveTo.Visibility = Visibility.Collapsed;
                    DeleteFile.Visibility = Visibility.Visible;
                    AboutMe.Visibility = Visibility.Collapsed;

                    if (File.Exists(directory + "\\NewFolder\\" + fileNam + ".txt"))
                    {
                        File.Delete(directory + "\\NewFolder\\" + fileNam + ".txt");
                    }
                    else
                    {
                        MessageBox.Show("Whoops! Someone deleted the file before we could delete it.");
                    }
                    break;
                case "About the Developer":
                    // Show the required tab
                    Home.Visibility = Visibility.Collapsed;
                    CreateFile.Visibility = Visibility.Collapsed;
                    WriteContent.Visibility = Visibility.Collapsed;
                    ReadContent.Visibility = Visibility.Collapsed;
                    MoveTo.Visibility = Visibility.Collapsed;
                    DeleteFile.Visibility = Visibility.Collapsed;
                    AboutMe.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        public bool CreateNewFile()
        {
            if (!File.Exists(directory + fileNam + ".txt"))
            {
                // concat the directory, file name and the format of the file.
                // .Close() method is required to close the Stream.
                File.Create(directory + fileNam + ".txt").Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(directory + fileNam + ".txt"))
            {
                // Get the bytes
                byte[] bytes = new UTF8Encoding(true).GetBytes(fileCon.Text);
                // Write it, 
                File.WriteAllBytes(directory + fileNam + ".txt", bytes);
                MessageBox.Show("Text has been written, you may continue.");
            }
            else
            {
                MessageBox.Show("Sorry, file not found. Make sure that there is a file (file has not been deleted), or please create a new file by following the Software process since the beginning.");
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            // Get the hyperlink
            Hyperlink link = sender as Hyperlink;
            // Navigate to that URI (URL)
            System.Diagnostics.Process.Start(link.NavigateUri.ToString());
        }
    }
}
