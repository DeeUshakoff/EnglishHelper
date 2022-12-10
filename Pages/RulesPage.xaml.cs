using System;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace EnglishHelper.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class RulesPage : Page
    {
        public RulesPage()
        {
            this.InitializeComponent();
        }

        private void RulesListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as TextBlock;
            string pageName = "EnglishHelper.Pages.Rules." + clickedItem.Tag;

            Type pageType = Type.GetType(pageName);
            if (pageType == null) { return; }


            ruleContent.Navigate(pageType);
        }
    }
}
