﻿using System.Collections.Generic;

namespace Shackmeets.Models
{
  public enum NotificationOption
  {
    Never = 0,
    IfWithinDistance = 1,
    Always = 2
  }

  public class User
  {
    public string Username { get; set; }

    public decimal LocationLatitude { get; set; }
    public decimal LocationLongitude { get; set; }
    public int MaxNotificationDistance { get; set; }

    public NotificationOption NotificationOption { get; set; }
    public bool NotifyByShackmessage { get; set; }
    public bool NotifyByEmail { get; set; }
    public string NotificationEmail { get; set; }

    public bool IsAdmin { get; set; }
    public bool IsBanned { get; set; }

    public List<Meet> Meets { get; set; }
    public List<Rsvp> Rsvps { get; set; }
  }
}
