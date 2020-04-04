namespace MAVN.Service.BonusCustomerProfile.Client.Models.Enums
{
    /// <summary>
    /// Represents BonusCustomerProfile's error codes
    /// </summary>
    public enum BonusCustomerProfileErrorCodes
    {
        /// <summary>
        /// Empty code
        /// </summary>
        None,

        /// <summary>
        /// Entity with the provided id does not exists
        /// </summary>
        EntityNotFound,

        /// <summary>
        /// Passed values can not be parsed to guid
        /// </summary>
        GuidCanNotBeParsed,

        /// <summary>
        /// Entity not valid error code
        /// </summary>
        EntityNotValid
    }
}
