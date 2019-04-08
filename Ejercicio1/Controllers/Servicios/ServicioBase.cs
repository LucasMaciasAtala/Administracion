namespace Controllers
{
    public abstract class ServicioBase
    {
        protected bool ValidarExtension(string aValidar, int charLimite)
        {
            aValidar.Trim();

            if (string.IsNullOrWhiteSpace(aValidar))
            {
                return false;
            }

            if (aValidar.Length >= charLimite)
            {
                return false;
            }

            return true;
        }

        public bool ValidarId(int id)
        {
            return id > 0;
        }
    }
}
