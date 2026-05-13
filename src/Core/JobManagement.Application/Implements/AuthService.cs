using JobManagement.Application.Common;
using JobManagement.Application.DTOs.Auth;
using JobManagement.Application.Interfaces;
using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Domain.Enums.Role;
using Microsoft.AspNetCore.Identity;

namespace JobManagement.Application.Implements;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ICompanyRepository _companyRepository;
    private readonly IJobSeekerRepository _jobSeekerRepository;
    private readonly IJwtService _jwtService;

    public AuthService(
        UserManager<AppUser> userManager,
        ICompanyRepository companyRepository,
        IJobSeekerRepository jobSeekerRepository,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _companyRepository = companyRepository;
        _jobSeekerRepository = jobSeekerRepository;
        _jwtService = jwtService;
    }

    public async Task<ApiResponse<AuthResponseDto>> RegisterCompanyAsync(CompanyRegisterDto dto)
    {
        try
        {
            var validationError = ValidateBaseRegisterFields(dto?.Name, dto?.Surname, dto?.Email, dto?.Password);

            if (validationError != null)
                return validationError;

            if (string.IsNullOrWhiteSpace(dto!.CompanyName))
                return ApiResponse<AuthResponseDto>.Fail("Company name boş ola bilməz.");

            if (string.IsNullOrWhiteSpace(dto.VOEN))
                return ApiResponse<AuthResponseDto>.Fail("VOEN boş ola bilməz.");

            var existingUser = await _userManager.FindByEmailAsync(dto.Email.Trim());

            if (existingUser != null)
                return ApiResponse<AuthResponseDto>.Fail("Bu email artıq istifadə olunub.");

            var voenExists = await _companyRepository.IsExistAsync(x => x.VOEN == dto.VOEN.Trim());

            if (voenExists)
                return ApiResponse<AuthResponseDto>.Fail("Bu VOEN artıq istifadə olunub.");

            var user = CreateUser(dto.Name, dto.Surname, dto.Email, UserType.Company);
            var identityResult = await _userManager.CreateAsync(user, dto.Password);

            if (!identityResult.Succeeded)
                return GetIdentityFailResponse(identityResult);

            var company = new Company
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                CompanyName = dto.CompanyName.Trim(),
                VOEN = dto.VOEN.Trim(),
                Email = dto.Email.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            await _companyRepository.CreateAsync(company);
            await _companyRepository.SaveAsync();

            return ApiResponse<AuthResponseDto>.Ok(CreateAuthResponse(user), "Company register uğurludur.");
        }
        catch (Exception ex)
        {
            return ApiResponse<AuthResponseDto>.Fail(
                "Company register zamanı xəta baş verdi.",
                new List<string> { ex.Message }
            );
        }
    }

    public async Task<ApiResponse<AuthResponseDto>> RegisterJobSeekerAsync(JobSeekerRegisterDto dto)
    {
        try
        {
            var validationError = ValidateBaseRegisterFields(dto?.Name, dto?.Surname, dto?.Email, dto?.Password);

            if (validationError != null)
                return validationError;

            var existingUser = await _userManager.FindByEmailAsync(dto!.Email.Trim());

            if (existingUser != null)
                return ApiResponse<AuthResponseDto>.Fail("Bu email artıq istifadə olunub.");

            var user = CreateUser(dto.Name, dto.Surname, dto.Email, UserType.JobSeeker);
            var identityResult = await _userManager.CreateAsync(user, dto.Password);

            if (!identityResult.Succeeded)
                return GetIdentityFailResponse(identityResult);

            var jobSeeker = new JobSeeker
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Email = dto.Email.Trim(),
                IsPublic = false,
                IsAnonym = false,
                CreatedAt = DateTime.UtcNow
            };

            await _jobSeekerRepository.CreateAsync(jobSeeker);
            await _jobSeekerRepository.SaveAsync();

            return ApiResponse<AuthResponseDto>.Ok(CreateAuthResponse(user), "Job seeker register uğurludur.");
        }
        catch (Exception ex)
        {
            return ApiResponse<AuthResponseDto>.Fail(
                "Job seeker register zamanı xəta baş verdi.",
                new List<string> { ex.Message }
            );
        }
    }

    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto dto)
    {
        try
        {
            if (dto == null)
                return ApiResponse<AuthResponseDto>.Fail("Request boş ola bilməz.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                return ApiResponse<AuthResponseDto>.Fail("Email boş ola bilməz.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                return ApiResponse<AuthResponseDto>.Fail("Password boş ola bilməz.");

            var user = await _userManager.FindByEmailAsync(dto.Email.Trim());

            if (user == null)
                return ApiResponse<AuthResponseDto>.Fail("Email və ya password yanlışdır.");

            var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!passwordValid)
                return ApiResponse<AuthResponseDto>.Fail("Email və ya password yanlışdır.");

            return ApiResponse<AuthResponseDto>.Ok(CreateAuthResponse(user), "Login uğurludur.");
        }
        catch (Exception ex)
        {
            return ApiResponse<AuthResponseDto>.Fail(
                "Login zamanı xəta baş verdi.",
                new List<string> { ex.Message }
            );
        }
    }

    private ApiResponse<AuthResponseDto>? ValidateBaseRegisterFields(string? name, string? surname, string? email, string? password)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ApiResponse<AuthResponseDto>.Fail("Ad boş ola bilməz.");

        if (string.IsNullOrWhiteSpace(surname))
            return ApiResponse<AuthResponseDto>.Fail("Soyad boş ola bilməz.");

        if (string.IsNullOrWhiteSpace(email))
            return ApiResponse<AuthResponseDto>.Fail("Email boş ola bilməz.");

        if (string.IsNullOrWhiteSpace(password))
            return ApiResponse<AuthResponseDto>.Fail("Password boş ola bilməz.");

        return null;
    }

    private AppUser CreateUser(string name, string surname, string email, UserType userType)
    {
        return new AppUser
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Surname = surname.Trim(),
            Email = email.Trim(),
            UserName = email.Trim(),
            UserType = userType,
            IsEmailConfirmed = false,
            CreatedAt = DateTime.UtcNow
        };
    }

    private ApiResponse<AuthResponseDto> GetIdentityFailResponse(IdentityResult identityResult)
    {
        var errors = identityResult.Errors
            .Select(x => x.Description)
            .ToList();

        return ApiResponse<AuthResponseDto>.Fail("Register uğursuz oldu.", errors);
    }

    private AuthResponseDto CreateAuthResponse(AppUser user)
    {
        return new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email!,
            UserType = user.UserType,
            PhotoUrl = user.PhotoUrl,
            Token = _jwtService.GenerateToken(user)
        };
    }
}
