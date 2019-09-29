using System.Collections.ObjectModel;
using LicitProd.Entities;

namespace LicitProd.UI.Uwp.Pages.Settings
{
    public class IdiomaViewModel
    {
        private  int _count;
        public Idioma Idioma { get; }
        public ObservableCollection<TraduccionValue> Traducciones { get; set; } = new ObservableCollection<TraduccionValue>();

        public int Count
        {
            get { return Traducciones.Count; }
            set { _count = value;}

        }

        public IdiomaViewModel(Idioma idioma)
        {
            Idioma = idioma;
            idioma.Traducciones?.ForEach(x => Traducciones.Add(x));
        }

        public IdiomaViewModel()
        {
            
        }
    }
}