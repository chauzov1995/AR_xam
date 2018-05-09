using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class report : ContentPage
    {
        public report()
        {
            InitializeComponent();

            Subscribe();
            load_page();
        }

        void load_page()
        {

            string dbPath = DependencyService.Get<ISQLite>().GetDatabasePath("friends.db");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<an_dohod>();
            db.CreateTable<an_dkr_hist>();
            db.CreateTable<an_purse>();
            //     db.Insert(new an_dohod { komment = "Работа", id = "2", summa_dohod = "25000" }); // after creating the newStock object


            var stockList = db.Query<an_dkr_hist>("SELECT * " +
                " FROM [an_dkr_hist] WHERE [visible]=0 ORDER BY [data_fakt] DESC");

        
            listView.ItemsSource = stockList;






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