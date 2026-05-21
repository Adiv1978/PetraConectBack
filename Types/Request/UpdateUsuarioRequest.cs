namespace PetraConectBack.Types.Request
{
    public class UpdateUsuarioRequest
    {
        public string? SessionToken { get; set; }
        public string? Pass { get; set; }
    }
}
