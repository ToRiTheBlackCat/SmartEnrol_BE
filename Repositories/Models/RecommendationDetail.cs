﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class RecommendationDetail
{
    public int RecommendationDetailId { get; set; }

    public int RecommendationId { get; set; }

    public int UniMajorId { get; set; }

    public string Recommendation { get; set; }

    public virtual Recommendation RecommendationNavigation { get; set; }

    public virtual UniMajor UniMajor { get; set; }
}