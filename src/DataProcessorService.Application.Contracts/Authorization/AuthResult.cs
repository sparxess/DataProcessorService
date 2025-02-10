namespace DataProcessorService.Application.Contracts.Authorization;

public class AuthResult
{
    /// <summary>
    /// Успешно
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Токен
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public string ErrorMessage { get; set; }
}
