namespace MAVN.Service.BonusCustomerProfile.Domain.Exception
{
    public class EntityNotFoundException : System.Exception
    {
        public EntityNotFoundException() :
            base("Entity was not found")
        {

        }

        public EntityNotFoundException(string message) 
            : base(message)
        {

        }
    }
}
