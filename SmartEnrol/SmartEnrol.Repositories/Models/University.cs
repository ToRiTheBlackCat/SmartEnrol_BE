﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SmartEnrol.Repositories.Models;

public partial class University
{
    public int UniversityId { get; set; }

    public string UniversityName { get; set; }

    public string Code { get; set; }

    public string Location { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Website { get; set; }

    public virtual ICollection<AdmissionMethodOfUni> AdmissionMethodOfUnis { get; set; } = new List<AdmissionMethodOfUni>();

    public virtual ICollection<FieldOfUni> FieldOfUnis { get; set; } = new List<FieldOfUni>();
}