﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SmartEnrol.Repositories.Models;

public partial class Recommendation
{
    public int RecommendationId { get; set; }

    public int AccountId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Account Account { get; set; }

    public virtual ICollection<RecommendationDetail> RecommendationDetails { get; set; } = new List<RecommendationDetail>();
}