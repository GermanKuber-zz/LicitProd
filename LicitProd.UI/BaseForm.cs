using LicitProd.Services;
using System.Windows.Forms;

namespace LicitProd.UI
{
    public class BaseForm : Form
    {
        public BaseForm()
        {

            TranslationService.Subscribe(trans => ChangeLanguage(trans));

        }
        protected override void InitLayout()
        {
            base.InitLayout();
            TranslationService.GetTranslation()
                            .Success(x => ChangeLanguage(x));
        }
        public void ChangeLanguage(Translations translation)
        {
            translation._translations.ForEach(x =>
            {
                var controls = Controls.Find(x.Key, true);
                foreach (var control in controls)
                {
                    if (control is Label)
                        control.Text = x.Value;
                    if (control is Button)
                        control.Text = x.Value;
                }
            });
        }

    }
}
