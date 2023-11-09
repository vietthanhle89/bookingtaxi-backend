namespace bookingtaxi_backend.Model
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public bool Result { get; set; }
        public List<string> Errors { get; set; }
        public string Role { get; set; }
        public string AccountID { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
    }
}
