
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EvilHangman
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        Dictionary<int, List<string>> words = new Dictionary<int, List<string>>();
        List<string> remaining;
        public MainPage()
        {
            this.InitializeComponent();
            Setup();

        }

        private void Setup()
        {
            string[] lines = new string[0];
            string path = @"C:\Users\Karl\EvilHangman\words.txt";
            try
            {
                lines = System.IO.File.ReadAllLines("words.txt", System.Text.Encoding.UTF8);
            }
            catch (System.InvalidOperationException e)
            {
                lines = new string[0];
                Debug.WriteLine("Error");

            }
            foreach (string word in lines)
            {
                if (this.words[word.Length] != null)
                {
                    this.words[word.Length].Add(word);
                }
                else
                {
                    List<string> lengthList = new List<string>();
                    lengthList.Add(word);
                    this.words[word.Length] = lengthList;
                }
            }
        }
        public void SizeEntered()
        {
            int size = 8;
            this.remaining = words[size];
        }
        public bool ProposedLetter()
        {

            return false;
        }
    }
}
