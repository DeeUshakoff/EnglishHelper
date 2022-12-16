using EnglishHelper.RuleCheckers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace EnglishHelper.Pages.Rules
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class PassiveVoicePage : Page
    {
        public PassiveVoicePage()
        {
            this.InitializeComponent();

            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            Content_ScrollViewer.Height = bounds.Height  -25;
        }

        private void RichTextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }

      

        private void CheckPassiveVoice_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var buttonContext = button.DataContext.ToString().Split('.');
            bool concrete = buttonContext[0] == "True";
            string expectedTime = buttonContext[1];
            string expectedTimeForm = buttonContext[2];


            var stackpanel = button.Parent as StackPanel;
            var textbox = stackpanel.Children.Where(x => x.GetType() == typeof(TextBox)).First() as TextBox;
            if(string.IsNullOrWhiteSpace(textbox.Text))
            {
                App.DisplayNotification("Empty string");
                return;
            }
            var sentence = new Sentence(textbox.Text);
            var time = sentence.Time.ToString();
            var timeform = sentence.TimeForm.ToString();
            if (concrete)
            {
                if(expectedTime == time && expectedTimeForm == timeform && sentence.IsPassive)
                    textbox.BorderBrush = new SolidColorBrush(Colors.Green);
                else
                {
                    textbox.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                App.DisplayNotification(sentence.IsPassive ? "Passive" : "Not Passive", time + " " + timeform);
                
            }
            else
            {
                if (sentence.IsPassive)
                    textbox.BorderBrush = new SolidColorBrush(Colors.Green);
                else
                {
                    textbox.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                App.DisplayNotification(sentence.IsPassive ? "Passive" : "Not Passive", time + " " + timeform);

            }

        }

        private void TextBox_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            textbox.BorderBrush = new SolidColorBrush(Colors.Gray);
        }
    }
}
