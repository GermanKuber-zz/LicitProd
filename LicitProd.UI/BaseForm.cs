using LicitProd.Entities;
using LicitProd.Services;
using System;
using System.Windows.Forms;
using LicitProd.Seguridad;

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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            IdentityServices.Instance.IsLoggued()
                                     .Success(x => ApplyPermissions(x.Rol));
        }
        public virtual void ApplyPermissions(Rol rol) {
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
