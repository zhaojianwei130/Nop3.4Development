using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.Customer
{
    public partial class CustomerNavigationModel : BaseNopModel
    {
        public bool HideInfo { get; set; }
        public bool HideAddresses { get; set; }
        public bool HideRewardPoints { get; set; }
        public bool HideChangePassword { get; set; }
        public bool HideAvatar { get; set; }

        public CustomerNavigationEnum SelectedTab { get; set; }
    }

    public enum CustomerNavigationEnum
    {
        Info,
        Addresses,
        RewardPoints,
        ChangePassword,
        Avatar
    }
}