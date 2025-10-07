namespace EduPlatform.API.DTOs.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = default!;
        public string ProfName { get; set; } = default!;
        public string ProfSlug { get; set; } = default!;
        public int ProfId { get; set; }
    }
}
