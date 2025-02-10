namespace DataProcessorService.Application.Contracts.Authorization;

public interface IAuthService
{
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="username">имя пользователя</param>
    /// <param name="password">пароль пользователя</param>
    /// <returns></returns>
    Task<string> RegisterAsync(string username, string password);

    /// <summary>
    /// Аутентификация пользователя и получение токена доступа
    /// </summary>
    /// <param name="username">имя пользователя</param>
    /// <param name="password">пароль пользователя</param>
    /// <returns></returns>
    Task<AuthResult> LoginAsync(string username, string password);
}