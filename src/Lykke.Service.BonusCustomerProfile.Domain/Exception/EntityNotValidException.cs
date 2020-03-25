namespace Lykke.Service.BonusCustomerProfile.Domain.Exception
{
    public class EntityNotValidException : System.Exception
    {
        public EntityNotValidException() 
            : base("Entity not valid")
        {
        }

        public EntityNotValidException(string message) 
            : base(message)
        {
        }
    }
}
