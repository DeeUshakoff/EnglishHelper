using System;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace EnglishHelper
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void mainNV_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

            var selectedItem = (NavigationViewItem)args.SelectedItem;
            string selectedItemTag = ((string)selectedItem.Tag);
            //sender.Header = selectedItemTag.Substring(selectedItemTag.Length - 1);
            string pageName = "EnglishHelper.Pages." + selectedItemTag;
            
            Type pageType = Type.GetType(pageName);
            if(pageType == null) { NV_contentFrame.Content = "Wrong page"; return; }
            //sender.Header = selectedItem.Content;
            
            NV_contentFrame.Navigate(pageType);

        }
    }
}
