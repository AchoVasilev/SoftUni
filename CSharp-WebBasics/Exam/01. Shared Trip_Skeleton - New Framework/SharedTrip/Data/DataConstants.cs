namespace SharedTrip.Data
{
    public static class DataConstants
    {
        public const int IdMaxLength = 40;
        public const int UserMaxUsernameLength = 20;

        public const int UserMinUsername = 5;
        public const int UserMinPassword = 6;

        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int SeatsMinValue = 2;
        public const int SeatsMaxValue = 6;
        public const int DescriptionMaxLength = 80;
    }
}
