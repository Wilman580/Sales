using Sales.Common.Models;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Sales.ViewModel
{
    public class ProductsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private ObservableCollection<Product> productsList;

        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        public ObservableCollection<Product> ProductsList {
            get { return this.productsList; }
            set { this.SetValue(ref this.productsList, value); }
        }

        public async void LoadProducts()
        {
            var response = await this.apiService.GetList<Product>("http://localhost:64790", "/api", "/Products");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Aceptar");
                return;
            }
            var list = (List<Product>)response.Result;
            this.ProductsList = new ObservableCollection<Product>(list);
        }
    }
}
