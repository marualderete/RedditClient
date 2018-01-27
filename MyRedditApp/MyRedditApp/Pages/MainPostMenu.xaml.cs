using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyRedditApp.Pages
{
    //TODO: CAMBIAR ESTO POR MI MODELO Y EL VIEWMODEL
    public class MainMenuItem
    {
        public string Title { get; set; }
        public Type TargetType { get; set; }
    }

    public partial class MainPostMenu : MasterDetailPage
    {
        public List<MainMenuItem> MainMenuItems { get; set; }

        public MainPostMenu()
        {
            BindingContext = this;

                // Build the Menu
            MainMenuItems = new List<MainMenuItem>()
            {
                new MainMenuItem() { Title = "Page One", TargetType = typeof(PostDetailPage) },
                new MainMenuItem() { Title = "Page Two", TargetType = typeof(PostDetailPage) }
            };
     
            // Set the default page, this is the "home" page.
            Detail = new NavigationPage(new HomePage());
     
            InitializeComponent();
        }

        // When a MenuItem is selected.
        public void MainMenuItem_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMenuItem;
            if (item != null)
            {
                if (item.Title.Equals("Page One"))
                {
                    Detail = new NavigationPage(new PostDetailPage());
                }
                else if (item.Title.Equals("Page Two"))
                {
                    Detail = new NavigationPage(new PostDetailPage());
                }

                MenuListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
