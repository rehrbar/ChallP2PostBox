using System;
using System.ComponentModel.DataAnnotations;
using TapBoxCommon.Models;

namespace TapBoxWebApplication.Models
{
    public class DeviceViewModel
    {
        public DeviceViewModel()
        {
        }

        public DeviceViewModel(Device model)
        {
            DeviceName = model.DeviceName;
            Description = model.Description;
            LastContact = model.LastContact;
            OwnerMailAddress = model.OwnerMailAddress;
        }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public string DeviceName { get; set; }

        public DateTime? LastContact { get; set; }

        public string OwnerMailAddress { get; set; }

        public string DeviceKeyPrimary { get; set; }
        public string DeviceKeySecondary { get; set; }


        public void UpdateModel(Device model)
        {
            model.Description = Description;
            model.OwnerMailAddress = OwnerMailAddress;
        }
    }
}