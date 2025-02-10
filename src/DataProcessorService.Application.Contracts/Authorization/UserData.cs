namespace DataProcessorService.Application.Contracts.Authorization;

/// <summary>
/// Данные пользователя
/// </summary>
public class UserData
{
    /// <summary>
    /// Имя
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
}