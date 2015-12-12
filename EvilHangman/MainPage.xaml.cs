
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
using System.Text.RegularExpressions;

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
        public string[] result;
        public MainPage()
        {
            this.InitializeComponent();
            Setup();
            SizeEntered();
            ProposedLetter("a");
            ProposedLetter("e");
            ProposedLetter("i");
            ProposedLetter("p");
            ProposedLetter("k");
            ProposedLetter("l");
            ProposedLetter("t");
            ProposedLetter("m");
            ProposedLetter("u");
            ProposedLetter("n");
            foreach (string eunuhh in remaining)
            {
                Debug.WriteLine(eunuhh);
            }

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
            result = new string[size];
            this.remaining = words[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = "_";
            }
        }
        public bool ProposedLetter(string taht)
        {
            string letter = taht;
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
                Debug.WriteLine(this.remaining.Count);
                return false;
            }
            else
            {
                int min = 100;
                Dictionary<int, List<string>> occurances = new Dictionary<int, List<string>>();
                foreach (string item in this.remaining)
                {
                    int a = Regex.Matches(item, letter).Count;
                    if (a < min)
                    {
                        min = a;
                    }
                }
                List<string> tempRemaining1 = new List<string>();
                foreach (string item in this.remaining)
                {
                    if (min == 1)
                    {
                        if (Regex.Matches(item, letter).Count == min)
                        {
                            if (occurances.ContainsKey(item.IndexOf(letter)))
                            {
                                occurances[item.IndexOf(letter)].Add(item);
                            }
                            else
                            {
                                tempRemaining1.Add(item);
                                occurances[item.IndexOf(letter)] = tempRemaining1;
                            }
                        }
                        int max = 0;
                        int index = 0;
                        foreach (KeyValuePair<int, List<string>> entry in occurances)
                        {
                            if (entry.Value.Count > max)
                            {
                                max = entry.Value.Count;
                                index = entry.Key;
                            }
                        }
                        //TODO siin on erind kinni püüdmata
                        remaining = occurances[index];
                    }
                    else
                    {
                        if (Regex.Matches(item, letter).Count == min)
                        {
                            if (tempRemaining1.Count != 0) {
                                bool error = false;
                                for (int again = 0; again < item.Length; again++)
                                {
                                    if (!tempRemaining1[0][again].Equals(item[again]))
                                    {
                                        error = true;
                                        break;
                                    }
                                }
                                if (error == false)
                                {
                                    tempRemaining1.Add(item);
                                }

                            }
                            else
                            {
                                tempRemaining1.Add(item);
                            }
                        }
                        this.remaining = tempRemaining1;
                    }
                }
                
                Debug.WriteLine(remaining.Count);  
                string asi = remaining[0];
                for (int i = 0; i < asi.Length; i++)
                {
                    if (asi[i].Equals(letter))
                    {
                        this.result[i] = letter;  
                    }
                }
                string vastus = "";
                foreach (string elem in result)
                {
                    vastus += "_ ";
                }
                Debug.WriteLine(vastus); 
                return true; 
            }
            
        }
        private void buttonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var buttonValue = button.Content;
            Debug.WriteLine(buttonValue);
        }

    }
}
