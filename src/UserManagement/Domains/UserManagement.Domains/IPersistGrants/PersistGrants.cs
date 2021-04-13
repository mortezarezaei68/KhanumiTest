using System;
using Framework.Domain.Core;

namespace UserManagement.Domains.IPersistGrants
{
    public class PersistGrants:Entity<int>
    {
        public PersistGrants(string subjectId,string refreshToken, string ipAddress, DateTime expired,UserType userType)
        {
            Expired = expired;
            RefreshToken = refreshToken;
            IpAddress = ipAddress;
            SubjectId = subjectId;
            UserType=userType;
        }
        public string SubjectId { get; private set; }
        public string RefreshToken { get;  private set; }
        public DateTime Expired { get; private set;  }
        public string IpAddress { get;  private set; }
        public UserType UserType { get; private set;  }
        public bool IsRevoke { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public bool IsExpired => DateTime.UtcNow >= Expired;
        public bool IsActive => RevokedAt == null && !IsExpired;
        public User User { get; set; }

        public void Update()
        {
            IsRevoke = true;
            RevokedAt=DateTime.Now;
        }

    }
}