namespace Escalas.Infrastructure.Authentication;
public class JwtOptions
{
    public string Issuer { get; set; } = "Escalas_Issuer";
    public string Audience { get; set; } = "Escalas_Audience";
    public string SecretKey { get; set; } = "berserkeomelhor@amangajafeito#nomundo";
}
