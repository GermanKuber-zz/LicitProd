using LicitProd.Data.Repositories;
using LicitProd.Entities;
using LicitProd.UI.Uwp.Services;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicitProd.UI.Uwp.Pages.Concursos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListConcursos : Page
    {
        public List<Concurso> Concursos { get; set; }
        public ListConcursos()
        {
            this.InitializeComponent();
            LoadingService.LoadingStart();
            LoadData();
        }
        private void LoadData()
        {
            new ConcursosRepository().Get()
           .Success(concursos =>
           {
               Concursos = concursos;
               LoadingService.LoadingStop();
           })
           .Error(async x =>
           {
               MessageDialogService.Create("No hay concursos", c =>
               {
                   LoadingService.LoadingStop();
                   NavigationService.NavigatePop<Dashboard>();
               }, null);
           });
        }
        public void Group()
        {

        }

    }
}
