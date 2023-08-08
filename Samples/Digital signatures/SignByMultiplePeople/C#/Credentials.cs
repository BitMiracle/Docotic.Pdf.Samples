namespace SignByMultiplePeople
{
    internal readonly record struct Credentials
    {
        public readonly string Name;
        public readonly string Keystore;
        public readonly string Password;

        public Credentials(string name, string keystore, string password)
        {
            Name = name;
            Keystore = keystore;
            Password = password;
        }
    }
}
