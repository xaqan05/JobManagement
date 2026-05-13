using JobManagement.Application.Common;
using JobManagement.Application.DTOs.JobSeeker;
using JobManagement.Application.Interfaces;
using JobManagement.Application.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Domain.Enums.Role;
using Microsoft.AspNetCore.Identity;

namespace JobManagement.Application.Implements;
public class JobSeekerService : IJobSeekerService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJobSeekerRepository _jobSeekerRepository;
    private readonly IJobSeekerPhoneRepository _phoneRepository;
    private readonly IJobSeekerEducationRepository _educationRepository;
    private readonly IJobSeekerExperienceRepository _experienceRepository;
    private readonly IJobSeekerLanguageRepository _jobSeekerLanguageRepository;
    private readonly IJobSeekerSkillRepository _jobSeekerSkillRepository;
    private readonly IJobSeekerLinkRepository _linkRepository;
    private readonly IJobSeekerCertificateRepository _certificateRepository;
    private readonly IEducationInstitutionRepository _educationInstitutionRepository;
    private readonly IJobSeekerJobCategoryRepository _jobCategoryRepository;
    private readonly IJobSeekerJobPositionRepository _jobPositionRepository;
    private readonly ILanguageRepository _languageRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly ISocialPlatformRepository _socialPlatformRepository;

    public JobSeekerService(
        UserManager<AppUser> userManager,
        IJobSeekerRepository jobSeekerRepository,
        IJobSeekerPhoneRepository phoneRepository,
        IJobSeekerEducationRepository educationRepository,
        IJobSeekerExperienceRepository experienceRepository,
        IJobSeekerLanguageRepository jobSeekerLanguageRepository,
        IJobSeekerSkillRepository jobSeekerSkillRepository,
        IJobSeekerLinkRepository linkRepository,
        IJobSeekerCertificateRepository certificateRepository,
        IEducationInstitutionRepository educationInstitutionRepository,
        IJobSeekerJobCategoryRepository jobCategoryRepository,
        IJobSeekerJobPositionRepository jobPositionRepository,
        ILanguageRepository languageRepository,
        ISkillRepository skillRepository,
        ISocialPlatformRepository socialPlatformRepository)
    {
        _userManager = userManager;
        _jobSeekerRepository = jobSeekerRepository;
        _phoneRepository = phoneRepository;
        _educationRepository = educationRepository;
        _experienceRepository = experienceRepository;
        _jobSeekerLanguageRepository = jobSeekerLanguageRepository;
        _jobSeekerSkillRepository = jobSeekerSkillRepository;
        _linkRepository = linkRepository;
        _certificateRepository = certificateRepository;
        _educationInstitutionRepository = educationInstitutionRepository;
        _jobCategoryRepository = jobCategoryRepository;
        _jobPositionRepository = jobPositionRepository;
        _languageRepository = languageRepository;
        _skillRepository = skillRepository;
        _socialPlatformRepository = socialPlatformRepository;
    }

    public async Task<ApiResponse<JobSeekerCvGetDto>> CreateCvAsync(Guid userId, CreateCvDto dto)
    {
        try
        {
            if (dto == null)
                return ApiResponse<JobSeekerCvGetDto>.Fail("Request boş ola bilməz.");

            var baseValidation = await ValidateUserFieldsAsync(userId, dto);

            if (baseValidation != null)
                return baseValidation;

            var jobSeeker = await _jobSeekerRepository.GetByUserIdWithCvAsync(userId);

            if (jobSeeker == null)
                return ApiResponse<JobSeekerCvGetDto>.Fail("Job seeker tapılmadı.");

            if (jobSeeker.User.UserType != UserType.JobSeeker)
                return ApiResponse<JobSeekerCvGetDto>.Fail("Bu əməliyyatı yalnız JobSeeker edə bilər.");

            var referenceValidation = await ValidateReferenceDataAsync(dto);

            if (referenceValidation != null)
                return referenceValidation;

            var detailValidation = ValidateDetails(dto);

            if (detailValidation != null)
                return detailValidation;

            await UpdateUserAsync(jobSeeker.User, dto);

            jobSeeker.Email = dto.Email.Trim();
            jobSeeker.About = dto.About?.Trim();
            jobSeeker.Address = dto.Address?.Trim();
            jobSeeker.BirthDate = dto.BirthDate;
            jobSeeker.JobCategoryId = dto.JobCategoryId;
            jobSeeker.JobPositionId = dto.JobPositionId;
            jobSeeker.Gender = dto.Gender;
            jobSeeker.FamilyStatus = dto.FamilyStatus;
            jobSeeker.Citizenship = dto.Citizenship;
            jobSeeker.MilitaryStatus = dto.MilitaryStatus;
            jobSeeker.DriverLicense = dto.DriverLicense;
            jobSeeker.HasEducation = dto.Educations != null && dto.Educations.Any();
            jobSeeker.HasExperience = dto.Experiences != null && dto.Experiences.Any();
            jobSeeker.IsPublic = false;
            jobSeeker.IsAnonym = false;
            jobSeeker.UpdatedAt = DateTime.UtcNow;

            ClearOldCvDetails(jobSeeker);
            await CreateCvDetailsAsync(jobSeeker.Id, dto);
            await _jobSeekerRepository.SaveAsync();

            var updatedCv = await _jobSeekerRepository.GetByUserIdWithCvAsync(userId);

            return ApiResponse<JobSeekerCvGetDto>.Ok(MapCv(updatedCv!), "CV uğurla yaradıldı.");
        }
        catch (Exception ex)
        {
            return ApiResponse<JobSeekerCvGetDto>.Fail(
                "CV yaradılarkən xəta baş verdi.",
                new List<string> { ex.Message }
            );
        }
    }

    public async Task<ApiResponse<JobSeekerCvGetDto>> GetOwnCvAsync(Guid userId)
    {
        var jobSeeker = await _jobSeekerRepository.GetByUserIdWithCvAsync(userId);

        if (jobSeeker == null)
            return ApiResponse<JobSeekerCvGetDto>.Fail("Job seeker CV tapılmadı.");

        if (jobSeeker.User.UserType != UserType.JobSeeker)
            return ApiResponse<JobSeekerCvGetDto>.Fail("Bu endpoint yalnız JobSeeker üçündür.");

        return ApiResponse<JobSeekerCvGetDto>.Ok(MapCv(jobSeeker), "CV gətirildi.");
    }

    private async Task<ApiResponse<JobSeekerCvGetDto>?> ValidateUserFieldsAsync(Guid userId, CreateCvDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return ApiResponse<JobSeekerCvGetDto>.Fail("Ad boş ola bilməz.");

        if (string.IsNullOrWhiteSpace(dto.Surname))
            return ApiResponse<JobSeekerCvGetDto>.Fail("Soyad boş ola bilməz.");

        if (string.IsNullOrWhiteSpace(dto.Email))
            return ApiResponse<JobSeekerCvGetDto>.Fail("Email boş ola bilməz.");

        var existingUser = await _userManager.FindByEmailAsync(dto.Email.Trim());

        if (existingUser != null && existingUser.Id != userId)
            return ApiResponse<JobSeekerCvGetDto>.Fail("Bu email artıq istifadə olunub.");

        return null;
    }

    private async Task<ApiResponse<JobSeekerCvGetDto>?> ValidateReferenceDataAsync(CreateCvDto dto)
    {
        if (dto.JobCategoryId.HasValue && !await _jobCategoryRepository.IsExistAsync(dto.JobCategoryId.Value))
            return ApiResponse<JobSeekerCvGetDto>.Fail("Job category tapılmadı.");

        if (dto.JobPositionId.HasValue && !await _jobPositionRepository.IsExistAsync(dto.JobPositionId.Value))
            return ApiResponse<JobSeekerCvGetDto>.Fail("Job position tapılmadı.");

        foreach (var education in dto.Educations ?? new List<JobSeekerEducationCreateDto>())
        {
            if (!await _educationInstitutionRepository.IsExistAsync(education.InstitutionId))
                return ApiResponse<JobSeekerCvGetDto>.Fail("Education institution tapılmadı.");
        }

        foreach (var language in dto.Languages ?? new List<JobSeekerLanguageCreateDto>())
        {
            if (!await _languageRepository.IsExistAsync(language.LanguageId))
                return ApiResponse<JobSeekerCvGetDto>.Fail("Language tapılmadı.");
        }

        foreach (var skill in dto.Skills ?? new List<JobSeekerSkillCreateDto>())
        {
            if (!await _skillRepository.IsExistAsync(skill.SkillId))
                return ApiResponse<JobSeekerCvGetDto>.Fail("Skill tapılmadı.");
        }

        foreach (var link in dto.Links ?? new List<JobSeekerLinkCreateDto>())
        {
            if (!await _socialPlatformRepository.IsExistAsync(link.SocialPlatformId))
                return ApiResponse<JobSeekerCvGetDto>.Fail("Social platform tapılmadı.");
        }

        return null;
    }

    private ApiResponse<JobSeekerCvGetDto>? ValidateDetails(CreateCvDto dto)
    {
        foreach (var phone in dto.Phones ?? new List<JobSeekerPhoneCreateDto>())
        {
            if (string.IsNullOrWhiteSpace(phone.PhoneNumber))
                return ApiResponse<JobSeekerCvGetDto>.Fail("Telefon nömrəsi boş ola bilməz.");

            if (string.IsNullOrWhiteSpace(phone.CountryCode))
                return ApiResponse<JobSeekerCvGetDto>.Fail("Telefon country code boş ola bilməz.");
        }

        foreach (var education in dto.Educations ?? new List<JobSeekerEducationCreateDto>())
        {
            if (string.IsNullOrWhiteSpace(education.SpecialtyName))
                return ApiResponse<JobSeekerCvGetDto>.Fail("İxtisas adı boş ola bilməz.");

            if (!education.IsCurrentlyStudying && education.EndDate == null)
                return ApiResponse<JobSeekerCvGetDto>.Fail("Təhsil bitmə tarixi boş ola bilməz.");
        }

        foreach (var experience in dto.Experiences ?? new List<JobSeekerExperienceCreateDto>())
        {
            if (string.IsNullOrWhiteSpace(experience.CompanyName))
                return ApiResponse<JobSeekerCvGetDto>.Fail("İş təcrübəsində şirkət adı boş ola bilməz.");

            if (string.IsNullOrWhiteSpace(experience.PositionName))
                return ApiResponse<JobSeekerCvGetDto>.Fail("İş təcrübəsində vəzifə boş ola bilməz.");

            if (!experience.IsCurrentlyWorking && experience.EndDate == null)
                return ApiResponse<JobSeekerCvGetDto>.Fail("İş bitmə tarixi boş ola bilməz.");
        }

        foreach (var link in dto.Links ?? new List<JobSeekerLinkCreateDto>())
        {
            if (string.IsNullOrWhiteSpace(link.Url))
                return ApiResponse<JobSeekerCvGetDto>.Fail("Link URL boş ola bilməz.");
        }

        foreach (var certificate in dto.Certificates ?? new List<JobSeekerCertificateCreateDto>())
        {
            if (string.IsNullOrWhiteSpace(certificate.CertificateName))
                return ApiResponse<JobSeekerCvGetDto>.Fail("Sertifikat adı boş ola bilməz.");

            if (string.IsNullOrWhiteSpace(certificate.IssuingOrganization))
                return ApiResponse<JobSeekerCvGetDto>.Fail("Sertifikatı verən təşkilat boş ola bilməz.");
        }

        return null;
    }

    private async Task UpdateUserAsync(AppUser user, CreateCvDto dto)
    {
        user.Name = dto.Name.Trim();
        user.Surname = dto.Surname.Trim();
        user.Email = dto.Email.Trim();
        user.UserName = dto.Email.Trim();

        await _userManager.UpdateAsync(user);
    }

    private void ClearOldCvDetails(JobSeeker jobSeeker)
    {
        foreach (var phone in jobSeeker.Phones.ToList())
            _phoneRepository.Remove(phone);

        foreach (var education in jobSeeker.Educations.ToList())
            _educationRepository.Remove(education);

        foreach (var experience in jobSeeker.Experiences.ToList())
            _experienceRepository.Remove(experience);

        foreach (var language in jobSeeker.Languages.ToList())
            _jobSeekerLanguageRepository.Remove(language);

        foreach (var skill in jobSeeker.Skills.ToList())
            _jobSeekerSkillRepository.Remove(skill);

        foreach (var link in jobSeeker.Links.ToList())
            _linkRepository.Remove(link);

        foreach (var certificate in jobSeeker.Certificates.ToList())
            _certificateRepository.Remove(certificate);
    }

    private async Task CreateCvDetailsAsync(Guid jobSeekerId, CreateCvDto dto)
    {
        foreach (var phone in dto.Phones ?? new List<JobSeekerPhoneCreateDto>())
        {
            await _phoneRepository.CreateAsync(new JobSeekerPhone
            {
                Id = Guid.NewGuid(),
                JobSeekerId = jobSeekerId,
                PhoneNumber = phone.PhoneNumber.Trim(),
                CountryCode = phone.CountryCode.Trim(),
                CreatedAt = DateTime.UtcNow
            });
        }

        foreach (var education in dto.Educations ?? new List<JobSeekerEducationCreateDto>())
        {
            await _educationRepository.CreateAsync(new JobSeekerEducation
            {
                Id = Guid.NewGuid(),
                JobSeekerId = jobSeekerId,
                InstitutionId = education.InstitutionId,
                SpecialtyName = education.SpecialtyName.Trim(),
                EducationLevel = education.EducationLevel,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                IsCurrentlyStudying = education.IsCurrentlyStudying,
                CreatedAt = DateTime.UtcNow
            });
        }

        foreach (var experience in dto.Experiences ?? new List<JobSeekerExperienceCreateDto>())
        {
            await _experienceRepository.CreateAsync(new JobSeekerExperience
            {
                Id = Guid.NewGuid(),
                JobSeekerId = jobSeekerId,
                CompanyName = experience.CompanyName.Trim(),
                PositionName = experience.PositionName.Trim(),
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                IsCurrentlyWorking = experience.IsCurrentlyWorking,
                Responsibilities = experience.Responsibilities?.Trim(),
                CreatedAt = DateTime.UtcNow
            });
        }

        foreach (var language in dto.Languages ?? new List<JobSeekerLanguageCreateDto>())
        {
            await _jobSeekerLanguageRepository.CreateAsync(new JobSeekerLanguage
            {
                Id = Guid.NewGuid(),
                JobSeekerId = jobSeekerId,
                LanguageId = language.LanguageId,
                Level = language.Level,
                CreatedAt = DateTime.UtcNow
            });
        }

        foreach (var skill in dto.Skills ?? new List<JobSeekerSkillCreateDto>())
        {
            await _jobSeekerSkillRepository.CreateAsync(new JobSeekerSkill
            {
                Id = Guid.NewGuid(),
                JobSeekerId = jobSeekerId,
                SkillId = skill.SkillId,
                CreatedAt = DateTime.UtcNow
            });
        }

        foreach (var link in dto.Links ?? new List<JobSeekerLinkCreateDto>())
        {
            await _linkRepository.CreateAsync(new JobSeekerLink
            {
                Id = Guid.NewGuid(),
                JobSeekerId = jobSeekerId,
                SocialPlatformId = link.SocialPlatformId,
                Url = link.Url.Trim(),
                CreatedAt = DateTime.UtcNow
            });
        }

        foreach (var certificate in dto.Certificates ?? new List<JobSeekerCertificateCreateDto>())
        {
            await _certificateRepository.CreateAsync(new JobSeekerCertificate
            {
                Id = Guid.NewGuid(),
                JobSeekerId = jobSeekerId,
                CertificateName = certificate.CertificateName.Trim(),
                IssuingOrganization = certificate.IssuingOrganization.Trim(),
                CertificateImageUrl = certificate.CertificateImageUrl?.Trim(),
                CreatedAt = DateTime.UtcNow
            });
        }
    }

    private JobSeekerCvGetDto MapCv(JobSeeker jobSeeker)
    {
        return new JobSeekerCvGetDto
        {
            Id = jobSeeker.Id,
            UserId = jobSeeker.UserId,
            Name = jobSeeker.User.Name,
            Surname = jobSeeker.User.Surname,
            Email = jobSeeker.User.Email ?? jobSeeker.Email ?? string.Empty,
            PhotoUrl = jobSeeker.User.PhotoUrl,
            About = jobSeeker.About,
            Address = jobSeeker.Address,
            BirthDate = jobSeeker.BirthDate,
            JobCategoryId = jobSeeker.JobCategoryId,
            JobCategoryName = jobSeeker.JobCategory?.Name,
            JobPositionId = jobSeeker.JobPositionId,
            JobPositionName = jobSeeker.JobPosition?.Name,
            Gender = jobSeeker.Gender,
            FamilyStatus = jobSeeker.FamilyStatus,
            Citizenship = jobSeeker.Citizenship,
            MilitaryStatus = jobSeeker.MilitaryStatus,
            DriverLicense = jobSeeker.DriverLicense,
            HasEducation = jobSeeker.HasEducation,
            HasExperience = jobSeeker.HasExperience,
            IsPublic = jobSeeker.IsPublic,
            IsAnonym = jobSeeker.IsAnonym,
            Phones = jobSeeker.Phones.Select(x => new JobSeekerPhoneGetDto
            {
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                CountryCode = x.CountryCode
            }).ToList(),
            Educations = jobSeeker.Educations.Select(x => new JobSeekerEducationGetDto
            {
                Id = x.Id,
                InstitutionId = x.InstitutionId,
                InstitutionName = x.Institution.Name,
                SpecialtyName = x.SpecialtyName,
                EducationLevel = x.EducationLevel,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                IsCurrentlyStudying = x.IsCurrentlyStudying
            }).ToList(),
            Experiences = jobSeeker.Experiences.Select(x => new JobSeekerExperienceGetDto
            {
                Id = x.Id,
                CompanyName = x.CompanyName,
                PositionName = x.PositionName,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                IsCurrentlyWorking = x.IsCurrentlyWorking,
                Responsibilities = x.Responsibilities
            }).ToList(),
            Languages = jobSeeker.Languages.Select(x => new JobSeekerLanguageGetDto
            {
                Id = x.Id,
                LanguageId = x.LanguageId,
                LanguageName = x.Language.Name,
                Level = x.Level
            }).ToList(),
            Skills = jobSeeker.Skills.Select(x => new JobSeekerSkillGetDto
            {
                Id = x.Id,
                SkillId = x.SkillId,
                SkillName = x.Skill.Name,
                IsSoft = x.Skill.IsSoft
            }).ToList(),
            Links = jobSeeker.Links.Select(x => new JobSeekerLinkGetDto
            {
                Id = x.Id,
                SocialPlatformId = x.SocialPlatformId,
                SocialPlatformName = x.SocialPlatform.Name,
                Url = x.Url
            }).ToList(),
            Certificates = jobSeeker.Certificates.Select(x => new JobSeekerCertificateGetDto
            {
                Id = x.Id,
                CertificateName = x.CertificateName,
                IssuingOrganization = x.IssuingOrganization,
                CertificateImageUrl = x.CertificateImageUrl,
                CreatedAt = x.CreatedAt
            }).ToList()
        };
    }
}
