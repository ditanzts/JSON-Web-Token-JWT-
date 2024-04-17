namespace PercobaanApi1.Entities
{
    public class User
    {
        public int id_person { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? token { get; set; }

    }
}