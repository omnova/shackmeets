﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shackmeets.Dtos
{
  public class MeetDto
  {
    public int MeetId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string OrganizerUsername { get; set; }
    public DateTime EventDate { get; set; }

    public string LocationName { get; set; }
    public string LocationAddress { get; set; }
    public string LocationState { get; set; }
    public string LocationCountry { get; set; }
    public decimal LocationLatitude { get; set; }
    public decimal LocationLongitude { get; set; }

    [JsonIgnore]
    public bool WillPostAnnouncement { get; set; }

    public List<RsvpDto> Rsvps { get; set; }
    
    public int InterestedCount
    {
      get { return this.Rsvps != null ? this.Rsvps.Where(r => r.RsvpTypeId == 2).Sum(r => r.NumAttendees) : 0; }
    }
    
    public int GoingCount
    {
      get { return this.Rsvps != null ? this.Rsvps.Where(r => r.RsvpTypeId == 1).Sum(r => r.NumAttendees) : 0; }
    }
  }
}
