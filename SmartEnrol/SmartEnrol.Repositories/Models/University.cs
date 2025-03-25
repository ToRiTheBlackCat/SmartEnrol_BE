﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SmartEnrol.Repositories.Models;

public partial class University
{
    public int UniId { get; set; }

    public string UniName { get; set; }

    public string UniCode { get; set; }

    public string Location { get; set; }

    public int AreaId { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Website { get; set; }

    public virtual ICollection<AdmissionMethodOfUni> AdmissionMethodOfUnis { get; set; } = new List<AdmissionMethodOfUni>();

    public virtual Area Area { get; set; }

    public virtual ICollection<UniMajor> UniMajors { get; set; } = new List<UniMajor>();
}