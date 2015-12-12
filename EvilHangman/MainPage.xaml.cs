using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Windows.Media.Imaging;
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
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EvilHangman
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    public sealed partial class MainPage : Page
    {

        Dictionary<int, List<string>> words = new Dictionary<int, List<string>>();
        List<string> remaining;
        public string[] result;
        public List<int> loseList;
        public List<int> winList;
        String vastus;
          int image = 0;
        int counter = 0;
        public MainPage()
        {
            this.InitializeComponent();
               Setup();
            loseList=new List<int>();
            winList=new List<int>();
            SizeEntered();
               /*ProposedLetter("a");
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
               }*/
               

        }
          private void buttonClick(object sender, RoutedEventArgs e)
          {
               var button = (Button)sender;
               var buttonValue = button.Content;
               button.IsEnabled = false;
               if (!ProposedLetter(buttonValue.ToString().ToLower())){
                    image++;
                    if (image < 18)
                    {
                           counter++;
                         BitmapImage image2 = new BitmapImage(new Uri(BaseUri, "/Images/" + image + ".jpg"));

                         PlayGround.Source = image2;
                    }else {
                    saveScore("scoreLose.txt", counter);
                }

               }

               Debug.WriteLine(buttonValue);
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
                              try {
                            remaining=occurances[index];

                        }catch(Exception e) { }
                              
                         }
                         else
                         {
                              if (Regex.Matches(item, letter).Count == min)
                              {
                                   if (tempRemaining1.Count != 0)
                                   {
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
                         Debug.WriteLine("1: " + asi[i]);
                         Debug.WriteLine("2: " + letter);
                         if (asi[i].ToString().Equals(letter))
                         {
                              Debug.WriteLine("NO FAKK OFF");
                              this.result[i] = letter;
                         }
                    }
                    vastus = "";
                    foreach (string elem in result)
                    {

                         vastus += elem + " ";
                    }
                    if(vastus.Contains("_")) {
                    saveScore("scoreWin.txt", counter);
                }
                    Debug.WriteLine(vastus);
                    return true;
               }

          }

          private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
          {
              if(e.NewSize.Width > 700)
               {
                    Grid.SetRowSpan(PlayGround, 2);
                    Grid.SetRowSpan(buttons, 2);
                    Grid.SetColumn(PlayGround, 0);
                    Grid.SetColumn(buttons, 1);

                    Grid.SetColumnSpan(PlayGround, 1);
                    Grid.SetColumnSpan(buttons, 1);
                    Grid.SetRow(PlayGround, 0);
                    Grid.SetRow(buttons, 0);
               }
               else
               {
                    Grid.SetColumnSpan(PlayGround, 2);
                    Grid.SetColumnSpan(buttons, 2);
                    Grid.SetRow(PlayGround, 0);
                    Grid.SetRow(buttons, 1);

                    Grid.SetRowSpan(PlayGround, 1);
                    Grid.SetRowSpan(buttons, 1);
                    Grid.SetColumn(PlayGround, 0);
                    Grid.SetColumn(buttons, 0);
               }
          }

        public async Task saveScore(String fileName, int data) {
            await loadScores(fileName);

            //Debug.WriteLine("6:" + loseList.Count);
            foreach(int t in loseList) {
                //Debug.WriteLine("5:" + t);
            }
            String writeResult = "";
            if(fileName.Equals("scoreLose.txt")) {
                loseList.Add(data);
                loseList.Sort();
                loseList.Reverse();
                if(loseList.Count>10) {
                    loseList=loseList.GetRange(0, 10);
                }
                foreach(int t in loseList) {
                    writeResult+=t+",";
                    //Debug.WriteLine(writeResult);
                }
            } else if(fileName.Equals("scoreWin.txt")) {
                winList.Add(data);
                winList.Sort();
                loseList.Reverse();
                if(winList.Count>10) {
                    winList=winList.GetRange(0, 10);
                }
                foreach(int t in winList) {
                    writeResult+=t+",";
                }

            }
            writeResult=writeResult.Substring(0, writeResult.Length-1);
            HttpClient htClient = new HttpClient();
            string webUri = "http://evilhangman.azurewebsites.net/evilhangman/upload.php?table=save/"+fileName+"&data="+writeResult;
            string result = await htClient.GetStringAsync(webUri);
            htClient.Dispose();
            //Debug.WriteLine(result);
        }

        public async Task loadScores(String fileName) {
            HttpClient htClient = new HttpClient();
            string webUri = "http://evilhangman.azurewebsites.net/evilhangman/save/"+fileName;
            string result = await htClient.GetStringAsync(webUri);
            htClient.Dispose();
            if(fileName.Equals("scoreLose.txt")) {
                if(result.Contains(",")) {
                    this.loseList=result.Split(',').Select(Int32.Parse).ToList();
                } else if(!result.Equals("")) {
                    this.loseList.Add(Int32.Parse(result));
                }

                loseList.Sort();
                foreach(int t in this.loseList) {
                    //Debug.WriteLine("4:" + t);
                }
            } else if(fileName.Equals("scoreWin.txt")) {

                if(result.Contains(",")) {
                    this.winList=result.Split(',').Select(Int32.Parse).ToList();
                } else if(!result.Equals("")) {
                    this.winList.Add(Int32.Parse(result));
                }

                this.loseList.Sort();
                foreach(int t in this.winList) {
                    //Debug.WriteLine(t);
                }

            }
        }



    }
      
}
