
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
                try
                {
                    this.words[word.Length].Add(word);
                }
                catch(KeyNotFoundException e)
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
            string letter = "a";
            List<string> tempRemaining = new List<string>(); 
            foreach (string item in this.remaining)
            {
                if (item.Contains(letter) == false)
                {
                    tempRemaining.Add(item);
                }
            }
            if (tempRemaining.Count != 0)
            {
                this.remaining = tempRemaining;
                return false;
            }
            else
            {
                //TODO erandi meetodid

                foreach (string item in this.remaining)
                {

                }
                return true;
            }
            
        }
    }
}
