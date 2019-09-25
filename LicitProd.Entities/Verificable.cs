using LicitProd.Infrastructure;

namespace LicitProd.Entities
{
    public abstract class Verificable : Entity
    {
        private string _hash;

        public string Hash
        {
            get { return GenerateHash(); }
            set { _hash = value; }
        }
        public abstract string GenerateHash();
        public bool IsValid => _hash == Hash;

        public string HashValue(string toHash) =>
            VerificableService.GetCheckDigit((toHash));
    }
}
