using FuelInApi.Models.Dtos;

namespace FuelInApi.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequestDto mailRequest);
    }
}
