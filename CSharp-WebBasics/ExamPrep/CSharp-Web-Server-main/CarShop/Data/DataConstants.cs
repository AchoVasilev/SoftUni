namespace CarShop.Data
{
    public static class DataConstants
    {
        public const int IdMaxLength = 40;

        public const int UsernameMinLength = 4;
        public const int UsernameMaxLength = 20;
        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int PasswordMinLength = 5;
        public const string UserIsMechanic = "Mechanic";
        public const string UserIsClient = "Clinet";

        public const int CarMinLength = 5;
        public const int CarMaxLength = 20;
        public const string CarPlateNumberRegularExpression = @"[A-Z]{2}[0-9]{4}[A-Z]{2}";

        public const int DescriptionMinLength = 5;
    }
}
