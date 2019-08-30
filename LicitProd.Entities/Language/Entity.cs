namespace LicitProd.Entities
{
    public interface ILanguage { }
    public interface ILoginForm : ILanguage
    {
        string LabelLoginButton { get; set; }
        string LabelCancelButton { get; set; }
    }
    public interface ILogoutForm : ILanguage
    {
        string LabelLogoutButton { get; set; }
    }
    public interface IPrincipalForm: ILanguage
    {
        string LabelLenguajeIngles { get; set; }
        string LabelLenguajeEspaniol { get; set; }

    }
    public interface IInvitarProveedoresForm : ILanguage
    {
        string InvitarProveedoresFormLabelTituloPrincipal { get; set; }
    }
    public interface IDashboardProveedoresForm : ILanguage
    {
        string DashboardProveedoresLabelTituloPrincipal { get; set; }
    }
    public interface ITranslation : IPrincipalForm,ILogoutForm, ILoginForm, IInvitarProveedoresForm, IDashboardProveedoresForm { }
    public class Translation : ITranslation
    {
        public string LabelLoginButton { get; set; }
        public string LabelCancelButton { get; set; }
        public string LabelLogoutButton { get; set; }
        public string InvitarProveedoresFormLabelTituloPrincipal { get; set; }
        public string DashboardProveedoresLabelTituloPrincipal { get; set; }
        public string LabelLenguajeEspaniol { get; set; }
        public string LabelLenguajeIngles { get; set; }
    }
}
