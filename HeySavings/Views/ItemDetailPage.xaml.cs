using System.ComponentModel;
using Xamarin.Forms;
using HeySavings.ViewModels;

namespace HeySavings.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
