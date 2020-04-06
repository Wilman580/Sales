using GalaSoft.MvvmLight.Command;
using Sales.Common.Models;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.ViewModel
{
    public class ProductsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private ObservableCollection<Product> products;
        private bool isRefreshing;

        public ProductsViewModel()
        {
            this.apiService = new ApiService(); //Inicia el sercicio Api para la comunicacion de datos
            this.LoadProducts();
        }

        public ObservableCollection<Product> Products {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); } //Establece los valores si hay algun cambio co la clase BaseViewModel heredada
        }
        public bool IsRefreshing
        {
            get{ return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public async void LoadProducts()
        {
            this.IsRefreshing = true; //Refrezca la lista con los datos
            //verifica la conexión a internet
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                return;
            }
            var urlApi = Application.Current.Resources["UrlApi"].ToString(); //Etractor de Key de Diccionario de recursos App
            var response = await this.apiService.GetList<Product>(urlApi, "/api", "/Products");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Aceptar");
                return;
            }
            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list); //Conversion de tipo List a Observable Collections
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand //Comando para refrezcar con gesto de actualiación en la lista
        {
            get { return new RelayCommand(LoadProducts); } //Para usar RelayCommand se instala nuget mvvm...Ligth
        } 
    }
}
