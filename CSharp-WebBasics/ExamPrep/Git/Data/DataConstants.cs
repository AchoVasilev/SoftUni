namespace Git.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;
        public const int DefaultMaxLength = 20;

        public const int UserMinUsername = 5;
        public const int UserMinPassword = 6;
        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const string PublicRepositoryType = "Public";
        public const string PrivateRepositoryType = "Private";

        public const int RepositoryMinName = 3;
        public const int RepositoryMaxName = 10;

        public const int CommitDescriptionMinLength = 5;
    }
}
