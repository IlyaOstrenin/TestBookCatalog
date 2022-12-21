namespace TestBookCatalog
{
    public static class FastFields
    {
        /// <summary>
        /// Regular expression for the mobile phone of the Russian Federation
        /// </summary>
        public const string RegexPhoneNumber = @"^[7-7]{1}[0-9]{3}[0-9]{7}$";
        /// <summary>
        /// Regular expression for sms code
        /// </summary>
        public const string RegexSMSCode = @"^(\d{4})$";

        public const string SMSCode = "1234";
    }
}
