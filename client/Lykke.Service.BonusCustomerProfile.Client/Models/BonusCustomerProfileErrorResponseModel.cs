using Lykke.Service.BonusCustomerProfile.Client.Models.Enums;

namespace Lykke.Service.BonusCustomerProfile.Client.Models
{
    /// <summary>
    /// Represents BonusEngineErrorResponse model 
    /// </summary>
    public class BonusEngineErrorResponseModel
    {
        /// <summary>
        /// Represents error code from the operation.
        /// </summary>
        public BonusCustomerProfileErrorCodes ErrorCode { get; set; }

        /// <summary>
        /// Represents error message from the operation.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
