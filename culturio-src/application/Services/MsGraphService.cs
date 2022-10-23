using Azure.Identity;
using Culturio.Application.Users.Models;
using Culturio.Domain;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Culturio.Application.Services;

public class MsGraphService
{
    private readonly GraphServiceClient _graphServiceClient;
    private readonly string _aadIssuerDomain;
    private readonly string _aadB2CIssuerDomain;

    

    public MsGraphService(IConfiguration configuration)
    {
        string[]? scopes = configuration.GetValue<string>("GraphApi:Scopes")?.Split(' ');
        var tenantId = configuration.GetValue<string>("GraphApi:TenantId");

        // Values from app registration
        var clientId = configuration.GetValue<string>("GraphApi:ClientId");
        var clientSecret = configuration.GetValue<string>("GraphApi:ClientSecret");

        _aadIssuerDomain = configuration.GetValue<string>("AadIssuerDomain");
        _aadB2CIssuerDomain = configuration.GetValue<string>("AzureAdB2C:Domain");

        var options = new TokenCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
        };

        // https://docs.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
        var clientSecretCredential = new ClientSecretCredential(
            tenantId, clientId, clientSecret, options);

        _graphServiceClient = new GraphServiceClient(clientSecretCredential, scopes);
    }

  
    public async Task<(string Upn, string Password, string Id)> CreateAzureB2CSameDomainUserAsync(CreateUserDto userModel)
    {

        StringBuilder emailPart = new StringBuilder();
        string privateMail = userModel.Email;
        for (int i = 0; i < privateMail.Length; i++)
        {
            if (privateMail[i] == '@')
                break;
            emailPart.Append(privateMail[i]);
        }

        var user = new Microsoft.Graph.User
        {
            AccountEnabled = true,
            UserPrincipalName = emailPart.Append("@sdalonghorn.onmicrosoft.com").ToString(),
            DisplayName = userModel.FirstName,
            Surname = userModel.LastName,
            GivenName = userModel.FirstName,
            PreferredLanguage = "en-US",
            MailNickname = userModel.FirstName,
            PasswordProfile = new PasswordProfile
            {
                ForceChangePasswordNextSignIn = false, //VRATI SE NA OVO,TREBA BITI TRUE
                //Password="Admin123!",
                Password = GetEncodedRandomString(),
            },
            
        };
       

        var userCreated = await _graphServiceClient.Users
            .Request()
            .AddAsync(user);

        InviteUser(privateMail, "http://localhost:4200/#/", userCreated.UserPrincipalName, user.PasswordProfile.Password);

        //var userID = await _graphServiceClient.Users["41b5a6a8-3ad6-4c7e-aa67-a7030cd539a7"]   //RUCNO DOHVACANJE HARDCODE
        //    .Request()
        //    .Select("displayName,givenName,postalCode,identities,id")
        //    .GetAsync();
        


        return (user.UserPrincipalName, user.PasswordProfile.Password, userCreated.Id);
    }
    public async Task<Invitation> InviteUser(string email, string redirectUrl, string principalName, string password)
    {
        var invitation = new Invitation
        {
            InvitedUserEmailAddress = email,
            InviteRedirectUrl = redirectUrl,
            InvitedUserType = "Member",// default is guest,
            SendInvitationMessage = true,
            InvitedUserMessageInfo = new InvitedUserMessageInfo { CustomizedMessageBody= "UserName: " + principalName + "\nPassword: " + password + "\nContinue to Culturio website : http://localhost:4200/#" }
        };

        var invite = await _graphServiceClient.Invitations
            .Request()
            .AddAsync(invitation);

        return invite;
    }

    private static string GetEncodedRandomString()
    {
        var base64 = Convert.ToBase64String(GenerateRandomBytes(8));
        return HtmlEncoder.Default.Encode(base64);
    }

    private static byte[] GenerateRandomBytes(int length)
    {
        var item = RandomNumberGenerator.Create();
        var byteArray = new byte[length];
        item.GetBytes(byteArray);
        return byteArray;
    }



}