 namespace UserService.Configuration;
    public class JwtModel
    {
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double ExpiryInMinutes { get; set; }
    public int ExpiryInMonth {  get; set; }

}
