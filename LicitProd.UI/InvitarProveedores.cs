using LicitProd.Entities;
using LicitProd.Services;
using System.Windows.Forms;

namespace LicitProd.UI
{
    public abstract class BaseForm<TLanguage> : Form where TLanguage : ILanguage
    {
        public BaseForm()
        {
            TranslationService.Subscribe(x =>
            {
                ChangeLanguage((TLanguage)x);
            });
        }
        public abstract void ChangeLanguage(TLanguage translation);

    }
    public partial class InvitarProveedores : BaseForm<IInvitarProveedoresForm>
    {
        public InvitarProveedores()
        {
            InitializeComponent();
        }
        public override void ChangeLanguage(IInvitarProveedoresForm translation)
        {
        }

    }
}
