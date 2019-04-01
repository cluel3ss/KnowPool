using System;
using System.Threading.Tasks;
using KnowPool.model.Okta;
using Refit;

namespace KnowPool.interfaces
{
    public interface IOktaInterface
    {
        [Headers("Authorization: SSWS 00NYEz1hk8-m-Epy3E1y8MkFkx9LzeZznQmi8_hbUZ", "Accept: application/json", "Content-Type: application/json")]
        [Post("/api/v1/users?activate=false")]
        Task<SignUpResponse> CreateUser([Body] SignUpUser data);

        [Headers("Authorization: SSWS 00NYEz1hk8-m-Epy3E1y8MkFkx9LzeZznQmi8_hbUZ", "Accept: application/json", "Content-Type: application/json")]
        [Get("/api/v1/users/{userEmail}")]
        Task<GetUserWithLogin> CheckUser(string userEmail);

        [Headers("Authorization: SSWS 00NYEz1hk8-m-Epy3E1y8MkFkx9LzeZznQmi8_hbUZ", "Accept: application/json", "Content-Type: application/json")]
        [Post("/api/v1/users/{userId}/lifecycle/activate?sendEmail=false")]
        Task<UserActivation> ActivateUser(string userId);

    }
}
