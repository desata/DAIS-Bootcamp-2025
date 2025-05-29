using BirthdayGifts.Repository;
using BirthdayGifts.Repository.Implementations.Employee;
using BirthdayGifts.Repository.Implementations.Gift;
using BirthdayGifts.Repository.Implementations.Vote;
using BirthdayGifts.Repository.Implementations.VotingSession;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BirthdayGifts.Repository.Interfaces.Employee;
using BirthdayGifts.Repository.Interfaces.Gift;
using BirthdayGifts.Repository.Interfaces.Vote;
using BirthdayGifts.Repository.Interfaces.VotingSession;
using BirthdayGifts.Services.Interfaces.Authentication;
using BirthdayGifts.Services.Implementations.Authentication;
using BirthdayGifts.Services.Interfaces.Employee;
using BirthdayGifts.Services.Implementations.Employee;
using BirthdayGifts.Services.Interfaces.Gift;
using BirthdayGifts.Services.Implementations.Gift;
using BirthdayGifts.Services.Interfaces.Vote;
using BirthdayGifts.Services.Implementations.Vote;
using BirthdayGifts.Services.Interfaces.VotingSession;
using BirthdayGifts.Services.Implementations.VotingSession;
using BirthdayGifts.Services.DTOs.Authentication;
using BirthdayGifts.Services.DTOs.VotingSession;
using BirthdayGifts.Services.DTOs.Vote;

namespace BirthdayGifts.Program
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            
            ConnectionFactory.Initialize(connectionString);

            var services = new ServiceCollection();
            
            services.AddScoped<IEmployeeRepository2, EmployeeRepository2>();
            services.AddScoped<IGiftRepository2, GiftRepository2>();
            services.AddScoped<IVoteRepository2, VoteRepository2>();
            services.AddScoped<IVotingSessionRepository2, VotingSessionRepository2>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IGiftService, GiftService>();
            services.AddScoped<IVoteService, VoteService>();
            services.AddScoped<IVotingSessionService, VotingSessionService>();

            var serviceProvider = services.BuildServiceProvider();

            try
            {
                var authService = serviceProvider.GetRequiredService<IAuthenticationService>();
                var employeeService = serviceProvider.GetRequiredService<IEmployeeService>();
                var giftService = serviceProvider.GetRequiredService<IGiftService>();
                var votingService = serviceProvider.GetRequiredService<IVotingSessionService>();
                var voteService = serviceProvider.GetRequiredService<IVoteService>();

                Console.WriteLine("Login as first user (session creator):");
                Console.Write("Username: ");
                var username1 = Console.ReadLine();
                Console.Write("Password: ");
                var password1 = Console.ReadLine();

                var loginResult1 = await authService.LoginAsync(new LoginRequest
                {
                    Username = username1,
                    Password = password1
                });

                if (!loginResult1.Success)
                {
                    Console.WriteLine($"Login failed: {loginResult1.ErrorMessage}");
                    return;
                }

                Console.WriteLine($"Logged in as: {loginResult1.FullName}");

                var employees = await employeeService.GetAllAsync();
                Console.WriteLine("\nAvailable employees for birthday session:");
                foreach (var emp in employees)
                {
                    Console.WriteLine($"ID: {emp.EmployeeId}, Name: {emp.FullName}");
                }

                Console.Write("\nEnter birthday person ID: ");
                var birthdayPersonId = int.Parse(Console.ReadLine());

                var createSessionRequest = new CreateVotingSessionRequest
                {
                    BirthdayPersonId = birthdayPersonId,
                    CreatedById = loginResult1.EmployeeId.Value
                };

                var session = await votingService.CreateSessionAsync(createSessionRequest);
                Console.WriteLine($"Created voting session with ID: {session.VotingSessionId}");

                Console.WriteLine("\nLogin as second user (voter):");
                Console.Write("Username: ");
                var username2 = Console.ReadLine();
                Console.Write("Password: ");
                var password2 = Console.ReadLine();

                var loginResult2 = await authService.LoginAsync(new LoginRequest
                {
                    Username = username2,
                    Password = password2
                });

                if (!loginResult2.Success)
                {
                    Console.WriteLine($"Login failed: {loginResult2.ErrorMessage}");
                    return;
                }

                Console.WriteLine($"Logged in as: {loginResult2.FullName}");

                var gifts = await giftService.GetAllAsync();
                Console.WriteLine("\nAvailable gifts:");
                foreach (var gift in gifts)
                {
                    Console.WriteLine($"ID: {gift.GiftId}, Name: {gift.Name}, Price: {gift.Price}");
                }

                Console.Write("\nEnter gift ID to vote: ");
                var giftId = int.Parse(Console.ReadLine());

                var voteRequest = new CreateVoteRequest
                {
                    VotingSessionId = session.VotingSessionId,
                    VoterId = loginResult2.EmployeeId.Value,
                    GiftId = giftId
                };

                var vote = await voteService.CreateVoteAsync(voteRequest);
                Console.WriteLine("Vote registered successfully!");

                var votes = await voteService.GetVotesForSessionAsync(session.VotingSessionId);
                Console.WriteLine("\nCurrent votes in session:");
                foreach (var v in votes)
                {
                    Console.WriteLine($"Voter: {v.VoterName}, Gift: {v.GiftName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError occurred: {ex.Message}");
            }
        }
    }
}