using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TapBoxCommon.Models;

namespace TapBoxWebApplication.Models {
    public class AccessKeyViewModel {
        public AccessKeyViewModel() { }

        public AccessKeyViewModel(AccessKey model)
        {
            Id = model.Id;
            CardUID = model.CardUID;
            LastAccessed = model.LastAccessed;
            OwnerEmail = model.OwnerEmail;
            Devices = model.Devices.Select(device => device.DeviceName);
        }

        public Guid Id { get; set; }
        [Required]
        public string CardUID { get; set; }
        public DateTime? LastAccessed { get; set; }
        [Required]
        public string OwnerEmail { get; set; }

        public IEnumerable<string> Devices { get; set; }

        public void UpdateModel(AccessKey model)
        {
            model.CardUID = CardUID;
            model.OwnerEmail = OwnerEmail;
        }
    }
}