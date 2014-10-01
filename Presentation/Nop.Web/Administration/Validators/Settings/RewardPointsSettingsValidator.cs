using FluentValidation;
using Nop.Admin.Models.Settings;
using Nop.Services.Localization;

namespace Nop.Admin.Validators.Settings
{
    public class RewardPointsSettingsValidator : AbstractValidator<RewardPointsSettingsModel>
    {
        public RewardPointsSettingsValidator(ILocalizationService localizationService)
        {

        }
    }
}