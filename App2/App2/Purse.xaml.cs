using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Purse : ContentPage
    {

        public Purse()
        {
            InitializeComponent();

            Subscribe();
            load_page();
        }
        void load_page()
        {
            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);


            var stockList = db.Query<an_purse>("SELECT * " +
            "FROM [an_purse] WHERE [visible]=0");


            listView.ItemsSource = stockList;
        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            var detailPage = new Purse_add(0);
            await Navigation.PushAsync(detailPage);

        }

        public async void OnMore(object sender, EventArgs e)
        {
            //обновить
            var mi = ((MenuItem)sender);
            int id = Convert.ToInt32(mi.CommandParameter);

            var detailPage = new Purse_add(id);
            await Navigation.PushAsync(detailPage);
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            //удалить
            var mi = ((MenuItem)sender);
            int id = Convert.ToInt32(mi.CommandParameter);


            bool result = await DisplayAlert("Подтвердите действие", "Вы действительно хотите удалить ?" + id, "Ок", "Отмена");
            if (result)
            {
                string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
                var db = new SQLiteConnection(dbPath);
                db.Query<vis_an_dohod>("UPDATE [an_purse] SET [visible]=1 WHERE [id]=" + id);
                load_page();
            }




        }

        private void Subscribe()
        {
            MessagingCenter.Subscribe<ContentPage>(
                this, // кто подписывается на сообщения
                "LabelChange",   // название сообщения
                (sender) => { load_page(); });    // вызываемое действие

        }
    }
}