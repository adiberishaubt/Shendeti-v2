using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Shendeti.Domain.Configurations;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Services;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task<bool> SendMailAsync(MailData mailData)
    {
        try
        {
            using var emailMessage = new MimeMessage();
            
            var emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
            emailMessage.From.Add(emailFrom);
                
            var emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
            emailMessage.To.Add(emailTo);
                
            emailMessage.Subject = mailData.EmailSubject;
                
            var emailBodyBuilder = new BodyBuilder
            {
                TextBody = mailData.EmailBody
            };

            emailMessage.Body = emailBodyBuilder.ToMessageBody();
                
            using (var mailClient = new SmtpClient())
            {
                await mailClient.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await mailClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                await mailClient.SendAsync(emailMessage);
                await mailClient.DisconnectAsync(true);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task SendRequestMailAsync(PotentialDonor donor)
    {
        var urgent = "";
        var emailBody = "";
                
        if (donor.Urgent)
        {
            urgent = "URGJENT ";
            emailBody = $"I nderuar {donor.Name},\n\n" +
                        $"Ju drejtohemi për një KËRKESË URGJENTE për dhurim gjaku.\n\n" +
                        $"Lloji i Gjakut: {donor.BloodType}\n" +
                        $"Qyteti: {donor.CityName}\n" +
                        $"Spitali: {donor.Spitali}\n" +
                        $"Ju lutemi, kontaktoni ne menjëherë në numrin e mëposhtëm: [Numri i Telefonit]\n" +
                        $"Konfirmo dhurimin e gjakut ne linkun : http://localhost:5095/BloodRequest/{donor.RequestId}\n" +
                        $"Faleminderit për përkushtimin tuaj.\n" +
                        $"Me respekt,\n" +
                        $"Shendeti Org";
        }
        else
        {
            emailBody = $"I nderuar {donor.Name},\n\n" +
                        $"Ju drejtohemi për një kërkesë për dhurim gjaku.\n" +
                        $"Lloji i Gjakut: {donor.BloodType}\n" +
                        $"Qyteti: {donor.CityName}\n" +
                        $"Spitali: {donor.Spitali}\n" +
                        $"Konfirmo dhurimin e gjakut ne linkun : http://localhost:5095/BloodRequest/{donor.RequestId}\n" +
                        $"Ju falënderojmë për gatishmërinë tuaj për të dhënë gjak. Kontributi juaj mund të ndryshojë gjërat.\n" +
                        $"Me respekt,\n" +
                        $"Shendeti Org";
        }
                
        await SendMailAsync(new MailData
        {
            EmailToId = donor.Email,
            EmailToName = donor.Name,
            EmailSubject = urgent+$"Kerkese per dhurim gjaku : {donor.BloodType}",
            EmailBody = emailBody
        });
    }

    public async Task SendCompletedRequestMailAsync(BloodRequest request, User donor)
    {
        var emailBodyForNeeder = $"I nderuar {request.User.UserName},\n\n" +
                                  $"Na vjen mir te njoftojm se kemi gjetur nje dhurues gjaku qe ka pranuar\n" +
                                  $"Qyteti se nga vjen dhuruesi eshte : {donor.City.Name}\n" +
                                  $"Shpresojme se dhurimi i gjakut do te kryhet me sukses.\n" +
                                  $"Ju lutem pas dhurimi te gjakut verifikone ne kete link : http://localhost:5095/BloodRequest/{request.Id}/{donor.Id}\n" +
                                  $"Me respekt,\n" +
                                  $"Shendeti Org";
        
        var emailBodyForDonor = "";
        
        if (request.Urgent)
        {
            emailBodyForDonor = $"I nderuar {donor.UserName},\n\n" +
                                $"Ju jeni zgjedhur per dhurimin e gjakut per kerkesen me te dhenat :\n\n" +
                                $"Lloji i Gjakut: {request.BloodType}\n" +
                                $"Qyteti: {request.City.Name}\n" +
                                $"Spitali: {request.Spitali}\n" +
                                $"Ju lutemi, kontaktoni ne menjëherë në numrin e mëposhtëm: [Numri i Telefonit]\n\n" +
                                $"Faleminderit për përkushtimin tuaj.\n" +
                                $"VEMENDJE: NESE NUK JEPNI GJAKUN BRENDA 30 MINUTAVE DO VAZHDOJM ME DHURUESIT E GJAKUT TE TJERE" +
                                $"Me respekt,\n" +
                                $"Shendeti Org";
        }
        else
        {
            emailBodyForDonor = $"I nderuar {donor.UserName},\n\n" +
                                $"Ju jeni zgjedhur per dhurimin e gjakut per kerkesen me te dhenat :\n\n" +
                                $"Lloji i Gjakut: {request.BloodType}\n" +
                                $"Qyteti: {request.City.Name}\n" +
                                $"Spitali: {request.Spitali}\n" +
                                $"Ju lutemi, kontaktoni ne menjëherë në numrin e mëposhtëm: [Numri i Telefonit]\n\n" +
                                $"Faleminderit për përkushtimin tuaj.\n" +
                                $"Me respekt,\n" +
                                $"Shendeti Org";
        }
                
        await SendMailAsync(new MailData
        {
            EmailToId = donor.Email,
            EmailToName = donor.UserName,
            EmailSubject = $"Jeni Zgjedhur Per Dhurim Gjaku",
            EmailBody = emailBodyForDonor
        });
                
        await SendMailAsync(new MailData
        {
            EmailToId = request.User.Email,
            EmailToName = request.User.UserName,
            EmailSubject = $"Kemi Gjetur Nje Dhurues Gjaku Per Ju",
            EmailBody = emailBodyForNeeder
        });
    }

    public async Task SendNoDonorFoundMailAsync(BloodRequest request)
    {
        await SendMailAsync(new MailData
        {
            EmailToId = request.User.Email,
            EmailToName = request.User.UserName,
            EmailSubject = $"Na vjen keq te ju lajmerojm qe nuk ka dhurues gjaku ne kete moment",
            EmailBody = $"I nderuar {request.User.UserName}\n" +
                        $"Per fat te keq nuk ka dhurues gjaku ne kete moment me llojin e gjakut {request.BloodType} ne asnje qytet.\n" +
                        $"Nese nuk deshironi te vazhdojm kerkimin cdo 10 minuta dhe te ju njoftojm vetem ne rast se ka dhurues gjaku klikoni ne linkun : http://localhost:5095/BloodRequest/{request.Id}\n" +
                        $"Me keqardhje,\n" +
                        $"Shendeti Org"
        });
    }
}